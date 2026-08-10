using hospital_api.DTO.Request;
using hospital_api.Interface.Services;
using hospital_api.Models;
using hospital_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
namespace hospital_api.Controllers
{
   
    public abstract class EntityController<T,Response,Request,Filter> : ControllerBase
        where T : Entity where Response : class where Request : BaseRequest where Filter : FilterableRequest
    {
        private readonly IBaseService<T, Response, Request,Filter> service;


        public EntityController(IBaseService<T, Response, Request, Filter> service)
        {
            this.service = service;

        }
        [HttpPost]
        [Route("create")]
        public async virtual Task<IActionResult> Post([FromBody] Request data)
        {


            var response = await service.Create(data);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> Get([FromQuery]int id)
        {
            var response = await service.Get(id);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> All()
        {
            var response = await this.service.All();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put([FromBody] Request data)
        {
            var response = await service.Update(data);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.Delete(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("find")]
        public IActionResult Criteria([FromQuery] Filter filterCriteria)
        {
            var response = service.Find(filterCriteria);
            return StatusCode(response.StatusCode, response);
        }

    }
    }

