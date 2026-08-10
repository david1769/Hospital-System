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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : EntityController<Patient, PatientResponse, PatientRequest,FilterableRequest>
    {
        IPatientService service;

        public PatientController(IPatientService service) : base(service)
        {

            this.service = service;


        }


        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {

            var response = await service.GetTotalPatientsAsync();
            return StatusCode(response.StatusCode, response);
        }






        }








    }

    

