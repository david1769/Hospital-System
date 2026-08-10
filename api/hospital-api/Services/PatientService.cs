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
    public class PatientService : BaseService<Patient, PatientResponse, PatientRequest, FilterableRequest>, IPatientService
    {
        public PatientService(ICommandRepository<Patient> commandRepository, IQueryRepository<Patient> queryRepository, IMapper mapper, ILogger<BaseService<Patient, PatientResponse, PatientRequest, FilterableRequest>> logger) : base(commandRepository, queryRepository, mapper, logger)
        {


        }

        protected override IQueryable<Patient> FindLogic(FilterableRequest request)
        {
            var list = queryRepository.Filter(d => d.IsActive == true);
            if (!string.IsNullOrEmpty(request.Search))
            {
                var term = request.Search.ToLower();
                list = list.Where(d => EF.Functions.Like((d.FirstName ?? "").ToLower(), $"%{term}%") || EF.Functions.Like((d.LastName ?? "").ToLower(), $"%{term}%"));
            }

            return list;
        }

        public async Task<int> GetActivePatientsAsync()
        {
            var query = queryRepository.Filter(p => p.IsActive == true);
            return await query.CountAsync();
        }

        public async Task<CountResponse> GetTotalPatientsAsync()
        {
            var count = await queryRepository.CountAsync(p => p.IsActive == true);
            return new CountResponse(count);
        }

    }
    }
