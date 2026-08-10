using hospital_api.Data;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Services;
using hospital_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hospital_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceDataController : EntityController<ReferenceData, ReferenceDataResponse, ReferenceDataRequest,FilterableRequest>
    {
        IReferenceData service;
        public ReferenceDataController(IReferenceData service) : base(service)
        {

            this.service = service;


        }
        [HttpGet]
        [Route("get-by-referencecategory")]
        public async Task<IActionResult> GetByReferenceData([FromQuery] int referencecategoryid)
        {
            var response =  service.GetByReferenceData(referencecategoryid);
            return StatusCode(response.StatusCode, response);
        }
    }

}

