using AutoMapper;
using PracticalTest.Common.Dtos;
using PracticalTest.Entities.Entities;

namespace PracticalTest.Api.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeResponseDto, Employee>().ReverseMap();
        CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
        CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
    }
}
