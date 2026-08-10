using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Models;

namespace hospital_api.Interface.Services
{
    public interface IBaseService<T,Response,Request,Filter>
        where T : Entity where Response : class where Request : BaseRequest where Filter : FilterableRequest 
    {
        Task<DataResponse<Response>> Create(Request request);
        PagedResponse<Response> Find(Filter request);

        Task<DataResponse<Response>> Get(int id);
        Task<DataResponse<List<Response>>> All();

        Task<DataResponse<Response>> Update(Request request);

        Task<BaseResponse> Delete(int id);



    }
}
