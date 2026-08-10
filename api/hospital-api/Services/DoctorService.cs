using AutoMapper;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Repository;
using hospital_api.Interface.Services;
using hospital_api.Models;
using hospital_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace hospital_api.Services
{
    public class DoctorService: BaseService<Doctor, DoctorResponse, DoctorRequest,FilterableRequest>, IDoctorService
    {
        public DoctorService(ICommandRepository<Doctor> commandRepository, IQueryRepository<Doctor> queryRepository, IMapper mapper, ILogger<BaseService<Doctor, DoctorResponse, DoctorRequest,FilterableRequest>> logger) : base(commandRepository, queryRepository, mapper, logger)
    {


    }

        protected override IQueryable<Doctor> FindLogic(FilterableRequest request)
        {
            var list = queryRepository.Filter(d => d.IsActive == true);
            if (!string.IsNullOrEmpty(request.Search))
            {
                var term = request.Search.ToLower();
                list = list.Where(d => EF.Functions.Like((d.FirstName ?? "").ToLower(), $"%{term}%") || EF.Functions.Like((d.LastName ?? "").ToLower(), $"%{term}%"));
            }

            return list;
        }


        public async Task<CountResponse> GetTotalDoctorsAsync()
        {
            var count = await queryRepository.CountAsync(p => p.IsActive == true);
            return new CountResponse(count);
        }






    }
    

}
