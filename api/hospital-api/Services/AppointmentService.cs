
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Repository;
using hospital_api.Interface.Services;
using hospital_api.Models;
using hospital_api.Repositories;

namespace hospital_api.Services
{
    public class AppointmentService : BaseService<Appointment, AppointmentResponse, AppointmentRequest,FilterableRequest>, IAppointmentService
    {
        public AppointmentService(ICommandRepository<Appointment> commandRepository, IQueryRepository<Appointment> queryRepository, IMapper mapper, ILogger<BaseService<Appointment, AppointmentResponse, AppointmentRequest,FilterableRequest>> logger) : base(commandRepository,queryRepository,mapper,logger)
        {
       

        }

        protected override IQueryable<Appointment> FindLogic(FilterableRequest request)
        {
            var list = queryRepository.Filter(d => d.IsActive == true);
            if (!string.IsNullOrEmpty(request.Search))
            {
                var term = request.Search.ToLower();
                list = list.Where(d => EF.Functions.Like((d.Notes ?? "").ToLower(), $"%{term}%"));
            }

            return list;
        }


        public async Task<CountResponse> GetTodaysAppointmentsCountAsync()
        {
            var today = DateTime.Today;               
            var tomorrow = today.AddDays(1);          

            var count = await queryRepository.CountAsync(a =>
                a.AppointmentDate >= today &&
                a.AppointmentDate < tomorrow &&
                a.IsActive == true
            );

            return new CountResponse(count);
        }

        public async Task<CountResponse> GetPendingAppointmentsCountAsync()
        {
            var query = queryRepository.Filter(a => a.StatusId == 10);
            var count = await query.CountAsync();
            return new CountResponse(count);
        }

        public async Task<DataResponse<List<AppointmentResponse>>> GetTodaysAppointmentsList()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var appointments = await queryRepository.Filter(a =>
         a.AppointmentDate >= today &&
         a.AppointmentDate < tomorrow &&
         a.IsActive == true
     )
     .Include(a => a.Patient)
     .Include(a => a.Doctor)
     .Include(a => a.Status)
     .Include(a => a.Department)
     .OrderBy(a => a.AppointmentDate)
     .ToListAsync();


            var result = appointments.Select(a => new AppointmentResponse

            {
                Id = a.Id,
                PatientName = $"{a.Patient?.FirstName} {a.Patient?.LastName}",
                DoctorName = $"{a.Doctor?.FirstName} {a.Doctor?.LastName}",
                AppointmentDate = a.AppointmentDate,
                StatusName = a.Status?.Name,
                Notes = a.Notes






            }



            ).ToList();


            return new DataResponse<List<AppointmentResponse>>
            {
                Data = result
            };
        }

        public Task<CountResponse> GetTotalAppointmentsCountAsync()
        {
            var count = queryRepository.Filter(a => a.IsActive == true).Count();
            return Task.FromResult(new CountResponse(count));   
        }
    }

}
