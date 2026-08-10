using hospital_api.Data;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Repository;
using hospital_api.Interface.Services;
using hospital_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace hospital_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : EntityController<Doctor, DoctorResponse, DoctorRequest,FilterableRequest>
    {
        IDoctorService service;
        public DoctorController(IDoctorService service) : base(service)
        {

            this.service = service;


        }


        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {

            var response = await service.GetTotalDoctorsAsync();
            return StatusCode(response.StatusCode, response);
        }






    }

}
