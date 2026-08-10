using System.Globalization;
using System.Linq;
using AutoMapper;
using hospital_api.DTO.Request;
using hospital_api.Models;


namespace hospital_api.Mappers
{
    public partial class MappingProfile
    {
        public void MapRequests()
        {
            CreateMap<PatientRequest, Patient>(); 
            CreateMap<DoctorRequest, Doctor>();
            CreateMap<AppointmentRequest, Appointment>();
            CreateMap<ReferenceDataRequest, ReferenceData>();
            CreateMap<ReferenceDataCategoryRequest, ReferenceDataCategory>();
        }



    }
}
