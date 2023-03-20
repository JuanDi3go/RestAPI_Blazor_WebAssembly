using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.Update
{
    public class UpdateEmployeeCommand: IRequest<GenericResponse<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdDepartment { get; set; }
        public int Salary { get; set; }
        public DateTime ContractDate { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, GenericResponse<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Employee> _repository;

        public UpdateEmployeeCommandHandler(IRepositoryAsync<Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employeeExist = await _repository.Get(p => p.Id == request.Id);
            if (employeeExist == null)
                throw new Exceptions.ApiException($"The employee with the id {request.Id} doesnt exist");

            employeeExist.Name = request.Name;
            employeeExist.IdDepartment = request.IdDepartment;
            employeeExist.Salary = request.Salary;
            employeeExist.ContractDate = request.ContractDate;

            bool updateResult = await _repository.Update(employeeExist);

            return new GenericResponse<int>(employeeExist.Id, "The information of the employee has been updated");
        }
    }
}
