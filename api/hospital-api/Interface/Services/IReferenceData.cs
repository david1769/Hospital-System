using Azure;
using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Models;

namespace hospital_api.Interface.Services
{
    public interface IReferenceData : IBaseService<ReferenceData, ReferenceDataResponse, ReferenceDataRequest, FilterableRequest>
    {
        DataResponse<List<ReferenceDataResponse>> GetByReferenceData(int id);

    }
}
