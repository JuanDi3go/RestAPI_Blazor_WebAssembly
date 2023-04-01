using Application.DTOs;
using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Queries.GetById;
using Application.Features.Employees.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            #region DTOs

            CreateMap<Department, DepartmentDTO>();
            CreateMap<Employee, EmployeeDTO>();
            #endregion


            #region Commands

            CreateMap<CreateDepartmentCommand, Department>();

            CreateMap<CreateEmployeeCommand, Employee>();
            
            #endregion
        }
    }
}
