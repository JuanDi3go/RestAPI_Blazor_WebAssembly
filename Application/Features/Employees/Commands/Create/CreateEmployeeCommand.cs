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

namespace Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeCommand:IRequest<GenericResponse<int>>
    {
        public string Name { get; set; }
        public int IdDepartment { get; set; }
        public int Salary { get; set; }
        public DateTime ContractDate { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, GenericResponse<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Employee> _repository;

        public CreateEmployeeCommandHandler(IRepositoryAsync<Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
           var employee =  await _repository.Create(_mapper.Map<Employee>(request));

            return new GenericResponse<int>(employee.Id,"The employee was created successfully");
        }
    }
}
