using hospital_api.Data;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Interface.Services;
using hospital_api.Models;
using hospital_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hospital_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceDataCategoryController : EntityController<ReferenceDataCategory, ReferenceDataCategoryResponse, ReferenceDataCategoryRequest,FilterableRequest>
    {
        IReferenceDataCategory service;
        public ReferenceDataCategoryController(IReferenceDataCategory service) : base(service)
        {

            this.service = service;


        }
    }
}

