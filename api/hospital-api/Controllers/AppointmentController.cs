using hospital_api.DTO.Request;
using hospital_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using hospital_api.Data;
using Microsoft.AspNetCore.Authorization;
using hospital_api.Interface.Services;
using hospital_api.Interface.Repository;
using hospital_api.DTO.Response;
namespace hospital_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : EntityController<Appointment, AppointmentResponse, AppointmentRequest, FilterableRequest>
    {
        IAppointmentService service;
        private readonly ApplicationDbContext _context;


        public AppointmentController(IAppointmentService service, ApplicationDbContext context) : base(service)
        {

            this.service = service;
            this._context = context;


        }
        [HttpGet]
        public async Task<IActionResult> GetAppointment([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] int? doctorId = null, [FromQuery] int? departmentId = null)
        {
            var query = _context.Appointment.Where(a => a.AppointmentDate >= start && a.AppointmentDate <= end);

            if (doctorId.HasValue)
            {
                query = query.Where(a => a.DoctorId == doctorId.Value);
            }

            if (departmentId.HasValue)
            {
                query = query.Where(a => a.DepartmentId == departmentId.Value);
            }


            var appointments = await query
                .Include(a => a.Patient)
        .Include(a => a.Doctor)
        .Include(a => a.Status)
        .Include(a => a.Department)
                .ToListAsync();



            var events = appointments.Select(a => new
            {
                id = a.Id,
                title = $"{a.Patient?.FirstName   ?? "Unknown"} - {a.Doctor.FirstName ?? ""}".Trim(),
                start = a.AppointmentDate?.ToString("o"),
                backgroundColor =  "#3788d8", // your status color logic
                extendedProps = new
                {
                    patientName = a.Patient?.FirstName +" "+ a.Patient?.LastName,

                    doctorName = a.Doctor?.FirstName +" "+ a.Doctor?.LastName,
                    notes = a.Notes,
                    followUpRequired = a.FollowUpRequired
                }
            }).ToList();


            return Ok(new { 
            data = events,
            total = events.Count,
            sucess = true
            
            });
        }


        [HttpGet("get-total-appointments")]
        public async Task<IActionResult> GetTotalAppointments()
        {

            var response = await service.GetTotalAppointmentsCountAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-todays-count")]
        public async Task<IActionResult> GetTodayCount()
        {

            var response = await service.GetTodaysAppointmentsCountAsync();
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("get-pending")]
        public async Task<IActionResult> GetPending()
        {

            var response = await service.GetPendingAppointmentsCountAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-todays-list")]
        public async Task<IActionResult> GetTodayListAppointment()
        {

            var response = await service.GetTodaysAppointmentsList();
            return StatusCode(response.StatusCode, response);
        }

    }

}
