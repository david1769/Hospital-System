using AutoMapper;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Repository;
using hospital_api.Interface.Services;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;

namespace hospital_api.Services
{
    public class ReferenceDataCategoryService : BaseService<ReferenceDataCategory, ReferenceDataCategoryResponse, ReferenceDataCategoryRequest,FilterableRequest>, IReferenceDataCategory
    {
        public ReferenceDataCategoryService(ICommandRepository<ReferenceDataCategory> commandRepository, IQueryRepository<ReferenceDataCategory> queryRepository, IMapper mapper, ILogger<BaseService<ReferenceDataCategory, ReferenceDataCategoryResponse, ReferenceDataCategoryRequest,FilterableRequest>> logger) : base(commandRepository, queryRepository, mapper, logger)
        {


        }

        protected override IQueryable<ReferenceDataCategory> FindLogic(FilterableRequest request)
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
