using AutoMapper;
using hospital_api.Models;
using hospital_api.DTO;
using hospital_api.DTO.Request;

namespace hospital_api.Mappers
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapRequests();
            MapResponse();

        }
    }
}
