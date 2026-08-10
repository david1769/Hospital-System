using AutoMapper;
using hospital_api.Constants;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Repository;
using hospital_api.Interface.Services;
using hospital_api.Models;
using hospital_api.Repositories;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using hospital_api.Extension;
namespace hospital_api.Services
{
    public abstract class BaseService<T, Response, Request,Filter> : IBaseService<T, Response, Request,Filter>
        where T : Entity where Response : class where Request : BaseRequest where Filter : FilterableRequest
    {
        protected ICommandRepository<T> commandRepository;
        protected IQueryRepository<T> queryRepository;
        protected readonly IMapper _mapper;
        protected readonly ILogger logger;

        protected BaseService(ICommandRepository<T> commandRepository, IQueryRepository<T> queryRepository, IMapper mapper, ILogger logger)
        {
            this.commandRepository = commandRepository;
            this.queryRepository = queryRepository;
            this._mapper = mapper;
            this.logger = logger;

        }



        public async Task<DataResponse<List<Response>>> All()
        {
            var response = new DataResponse<List<Response>>();
            try
            {
                var list = await queryRepository.GetAll();
                var lookUpList = new List<Response>();
                if (list != null && list.Any())
                {

                    foreach (var item in list)
                        lookUpList.Add(_mapper.Map<Response>(item));


                    response.Data = lookUpList;
                    response.StatusCode = ResponseStatus.Ok;


                }
                else
                    response.StatusCode = ResponseStatus.NotFound;


            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.ServerError;
                var errorMessage = $"Error occured when retrieving all records for {typeof(T)}";
                response.Messages.Add(new MessageResponse { Message = errorMessage, Type = MessageType.Technical });
                logger.LogError(ex, errorMessage);
            }
            return response;

        }

        public async Task<DataResponse<Response>> Create(Request request)
        {
            var response = new DataResponse<Response>();

            try
            {
                var newEntity = _mapper.Map<T>(request);
                var existingEntity = await queryRepository.GetByPlainId(newEntity.Id);

                await commandRepository.Create(newEntity);
               

                if (existingEntity != null)
                {
                    response.StatusCode = ResponseStatus.BadRequest;
                    response.Messages.Add(new MessageResponse { Message = "An entity with this ID already exists.", Type = MessageType.Validation });
   
                   return response;
                }
                response.Data = await PostCreate(newEntity);
                response.StatusCode = ResponseStatus.Ok;
            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.ServerError;
                var errorMessage = $"Error occured when creating {typeof(T)}.";
                response.Messages.Add(new MessageResponse { Message = errorMessage, Type = MessageType.Technical });
                logger.LogError(ex, errorMessage);
            }
            

            return response;

        }
        protected async virtual Task<Response> PostCreate(T entity) => _mapper.Map<Response>(entity);


        public async Task<BaseResponse> Delete(int id)
        {
            var response = new BaseResponse();
            try
            {
                var itemToDelete = await queryRepository.GetById(id)
                    ;
                if (itemToDelete != null)
                {
                    await commandRepository.Delete(itemToDelete);
                    response.StatusCode = ResponseStatus.Ok;
                }
                else
                    response.StatusCode = ResponseStatus.NotFound;
            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.ServerError;
                var errorMessage = $"Error occured when deleting {typeof(T).Name}, with Id {id}";
                response.Messages.Add(new MessageResponse { Message = errorMessage, Type = MessageType.Technical });
                logger.LogError(ex, errorMessage);
            }
            return response;
        }

        public async Task<DataResponse<Response>> Get(int id)
        {
            var response = new DataResponse<Response>();
            try
            {
                var itm = await queryRepository.GetById(id);
                if (itm != null)
                {
                    response.Data = _mapper.Map<Response>(itm);
                    response.StatusCode = ResponseStatus.Ok;
                }
                else
                    response.StatusCode = ResponseStatus.NotFound;
            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.ServerError;
                var errorMessage = $"Error occured when reading from {typeof(T)},id={id}.";
                response.Messages.Add(new MessageResponse { Message = errorMessage, Type = MessageType.Technical });
                logger.LogError(ex, errorMessage);
            }

            return response;

        }

        public async Task<DataResponse<Response>> Update(Request request)
        {


            var response = new DataResponse<Response>();

            try
            {
                var itemToUpdate = await queryRepository.GetById(request.Id);
                if (itemToUpdate != null)
                {
                    var update = _mapper.Map<T>(request);
                    update.IsActive = itemToUpdate.IsActive;
                    update.CreatedAt = itemToUpdate.CreatedAt;
                    update.CreatedBy = itemToUpdate.CreatedBy;

                    var updateId = update.GetType().GetProperty("Id");
                    var idValue = itemToUpdate.GetType().GetProperty("Id");

                    if (idValue != null)
                        updateId.SetValue(update, idValue.GetValue(itemToUpdate));

                    await commandRepository.Update(itemToUpdate, update);
                    response.Data = await PostUpdate(update);
                    response.StatusCode = ResponseStatus.Ok;
                }
                else
                    response.StatusCode = ResponseStatus.BadRequest;
            
           
        }

            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.ServerError;
                var errorMessage = $"Error occured when updating {typeof(T).Name}.";
                response.Messages.Add(new MessageResponse { Message = errorMessage, Type = MessageType.Technical });
                logger.LogError(ex, errorMessage);
            }

            return response;

        }

        protected async virtual Task<Response> PostUpdate(T entity) => _mapper.Map<Response>(entity);

        public PagedResponse<Response> Find(Filter request)
        {
            var response = new PagedResponse<Response>();
            try
            {
                var list = FindLogic(request);
                //var getlist = list.ToList();
                var paged = list.Paginate(request.Page, request.PageSize);
                response.PageSize = paged.PageSize;
                response.CurrentPage = paged.CurrentPage;
                response.NextPage = paged.NextPage;
                response.PreviousPage = paged.PreviousPage;
                response.DisplayingText = paged.DisplayingText;
                response.TotalItems = paged.TotalItems;
                response.TotalPages = paged.TotalPages;
                response.Items = new List<Response>();

                foreach (var entity in paged!.Items)
                {
                    response.Items.Add(_mapper.Map<Response>(entity));
                }
            }
            catch(Exception ex)
            {
                response.StatusCode = ResponseStatus.ServerError;
                var errorMessage = $"Error occured when searching fro {typeof(T)}, search criteria{request}.";
                response.Messages.Add(new MessageResponse { Message = errorMessage, Type = MessageType.Technical });
                logger.LogError(ex, errorMessage);
            }
            return response
                
                ; 
        }

        protected abstract IQueryable<T> FindLogic(Filter request);

    }
}

