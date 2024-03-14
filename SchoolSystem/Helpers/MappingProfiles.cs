using AutoMapper;
using SchoolSystem.DTOs;
using SchoolSystem.School.DAL.Data.Models.Career;
using SchoolSystem.School.DAL.Data.Models.Students;
using SchoolSystem.School.DAL.Data.Models.TeachersAdmission;

namespace SchoolSystem.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<PersonalAddmissionDto, PersonalInformation>();
            CreateMap<JobDto, JobData>();

            CreateMap<StudentAdmissionDto, StudentAdmission>();
            
            // CreateMap<DAjobDto, DAJob>();
            CreateMap<TeacherAdmissionDto, TeacherInfoAdmission>();

            CreateMap<DAjobDto, TeacherAdmissionDto>();


        }
    }
}
