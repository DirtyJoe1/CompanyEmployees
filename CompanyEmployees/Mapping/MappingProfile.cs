using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace CompanyEmployees.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Grade, GradeDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<GradeForCreationDto, Grade>();
            CreateMap<StudentForCreationDto, Student>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<StudentForUpdateDto, Student>().ReverseMap();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<GradeForUpdateDto, Grade>();
        }
    }
}
