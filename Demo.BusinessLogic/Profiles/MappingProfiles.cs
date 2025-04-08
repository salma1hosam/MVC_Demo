using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.DataAccess.Models.EmployeeModel;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Source , Destination>()
            //ForMember(Destionation , Options)
            //MapFrom(Source)

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.EmpGender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmpType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Department , options => options.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.Department , options => options.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
