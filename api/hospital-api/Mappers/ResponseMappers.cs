using System.Globalization;
using System.Linq;
using AutoMapper;
using hospital_api.Models;
using hospital_api.DTO.Response;
namespace hospital_api.Mappers
{
    public partial class MappingProfile
    {
        public void MapResponse()
        {
            CreateMap<Appointment, AppointmentResponse>()
           .ForMember(dest => dest.DoctorId, src => src.MapFrom(s => s.DoctorId))
           .ForMember(dest => dest.Id, src => src.MapFrom(s => s.Id))
           .ForMember(dest => dest.DoctorName, src => src.MapFrom(s => s.Doctor!.FirstName + " " + s.Doctor.LastName))
           .ForMember(dest => dest.PatientId, src => src.MapFrom(s => s.PatientId))
           .ForMember(dest => dest.PatientName, src => src.MapFrom(s => s.Patient!.FirstName + " " + s.Patient.LastName))
           .ForMember(dest => dest.AppointmentDate, src => src.MapFrom(s => s.AppointmentDate))
           .ForMember(dest => dest.StatusId, src => src.MapFrom(s => s.StatusId))
           .ForMember(dest => dest.StatusName, src => src.MapFrom(s => s.Status!.Name))
           .ForMember(dest => dest.DepartmentName, src => src.MapFrom(s => s.Department!.Name))
           .ForMember(dest => dest.Notes, src => src.MapFrom(s => s.Notes));


            CreateMap<Doctor, DoctorResponse>()
           .ForMember(dest => dest.Email, src => src.MapFrom(s => s.Email))
           .ForMember(dest => dest.Id, src => src.MapFrom(s => s.Id))
           .ForMember(dest => dest.DepartmentId, src => src.MapFrom(s => s.DepartmentId))
           .ForMember(dest => dest.DepartmentName, src => src.MapFrom(s => s.Department!.Name))
           .ForMember(dest => dest.FullName, src => src.MapFrom(s => s.FirstName + " " + s.LastName))
           .ForMember(dest => dest.LicenseNumber, src => src.MapFrom(s => s.LicenseNumber))
           .ForMember(dest => dest.OfficeAddress, src => src.MapFrom(s => s.OfficeAddress))
           .ForMember(dest => dest.ProfileDescription, src => src.MapFrom(s => s.ProfileDescription))
           .ForMember(dest => dest.QualificationId, src => src.MapFrom(s => s.QualificationId))
           .ForMember(dest => dest.QualificationName, src => src.MapFrom(s => s.Qualification!.Name))
           .ForMember(dest => dest.ScheduleId, src => src.MapFrom(s => s.ScheduleId))
           .ForMember(dest => dest.ScheduleName, src => src.MapFrom(s => s.Schedule!.Name))
           .ForMember(dest => dest.SpecialtyId, src => src.MapFrom(s => s.SpecialtyId))
           .ForMember(dest => dest.SpecialtyName, src => src.MapFrom(s => s.Specialty!.Name))
           ;


            CreateMap<Patient, PatientResponse>()
              .ForMember(dest => dest.Id, src => src.MapFrom(s => s.Id))
              .ForMember(dest => dest.NationalIdNumber, src => src.MapFrom(s => s.NationalIdNumber))
              .ForMember(dest => dest.InsuranceProviderId, src => src.MapFrom(s => s.InsuranceProviderId))
              .ForMember(dest => dest.InsuranceProviderName, src => src.MapFrom(s => s.InsuranceProvider!.Name))
              .ForMember(dest => dest.FullName, src => src.MapFrom(s => s.FirstName + " " + s.LastName))
              .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(s => s.DateOfBirth))
              .ForMember(dest => dest.MedicalHistory, src => src.MapFrom(s => s.MedicalHistory))
              .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(s => s.PhoneNumber))
              .ForMember(dest => dest.Gender, src => src.MapFrom(s => s.Gender))
              ;

            CreateMap<ReferenceData, ReferenceDataResponse>()
              .ForMember(dest => dest.Id, src => src.MapFrom(s => s.Id))
              .ForMember(dest => dest.ReferenceDataCategoryId, src => src.MapFrom(s => s.ReferenceDataCategoryId))
              .ForMember(dest => dest.ReferenceDataCatName, src => src.MapFrom(s => s.ReferenceDataCategory!.Name))
              .ForMember(dest => dest.Name, src => src.MapFrom(s => s.Name))
              .ForMember(dest => dest.Value, src => src.MapFrom(s => s.Value))

              ;

            CreateMap<ReferenceDataCategory, ReferenceDataCategoryResponse>()
            .ForMember(dest => dest.Id, src => src.MapFrom(s => s.Id))            
            .ForMember(dest => dest.Name, src => src.MapFrom(s => s.Name))

            ;


        }

    }
}