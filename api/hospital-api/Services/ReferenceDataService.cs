using AutoMapper;
using Azure;
using hospital_api.Constants;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Repository;
using hospital_api.Interface.Services;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;

namespace hospital_api.Services
{
    public class ReferenceDataService : BaseService<ReferenceData, ReferenceDataResponse, ReferenceDataRequest, FilterableRequest>, IReferenceData
    {
        public ReferenceDataService(ICommandRepository<ReferenceData> commandRepository, IQueryRepository<ReferenceData> queryRepository, IMapper mapper, ILogger<BaseService<ReferenceData, ReferenceDataResponse, ReferenceDataRequest, FilterableRequest>> logger) : base(commandRepository, queryRepository, mapper, logger)
        {


        }

        public  DataResponse<List<ReferenceDataResponse>> GetByReferenceData(int id)
        {
            var response = new DataResponse<List<ReferenceDataResponse>>();
            try
            {
                var list = this.queryRepository.Filter(x => x.ReferenceDataCategoryId == id);
                var lookUpList = new List<ReferenceDataResponse>();
                if (list != null && list.Any())
                {
                    foreach (var item in list)
                        lookUpList.Add(_mapper.Map<ReferenceDataResponse>(item));

                    response.Data = lookUpList;
                    response.StatusCode = ResponseStatus.Ok;
                }
                else
                    response.StatusCode = ResponseStatus.NotFound;
            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.ServerError;
                var errorMessage = $"Error occured when retrieving records by category records for ReferenceData  entity";
                response.Messages.Add(new MessageResponse { Message = errorMessage, Type = MessageType.Technical });
                logger.LogError(ex, errorMessage);
            }

            return response;

        }

        protected override IQueryable<ReferenceData> FindLogic(FilterableRequest request)
        {
            var list = queryRepository.Filter(d => d.IsActive == true);
            if (!string.IsNullOrEmpty(request.Search))
            {
                var term = request.Search.ToLower();
                list = list.Where(d => EF.Functions.Like((d.Name ?? "").ToLower(), $"%{term}%"));
            }

            return list;
        }

    }
}
