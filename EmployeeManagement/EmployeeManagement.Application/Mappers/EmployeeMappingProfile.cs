using System;
using AutoMapper;
using EmployeeManagement.Application.Commands;
using EmployeeManagement.Application.Response;

namespace EmployeeManagement.Application.Mappers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeManagement.Core.Entities.Employee, EmployeeResponse>().ReverseMap();
            CreateMap<EmployeeManagement.Core.Entities.Employee, CreateEmployeeCommand>().ReverseMap();
        }
    }
}

