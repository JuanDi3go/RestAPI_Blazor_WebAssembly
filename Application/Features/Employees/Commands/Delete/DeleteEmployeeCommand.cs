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

namespace Application.Features.Employees.Commands.Delete
{
    public class DeleteEmployeeCommand:IRequest<GenericResponse<int>>
    {
        public int Id { get; set; }
    }

    public class DelteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, GenericResponse<int>>
    {
        private readonly IRepositoryAsync<Employee> _repository;
        private readonly IMapper _mapper;

        public DelteEmployeeCommandHandler(IMapper mapper, IRepositoryAsync<Employee> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GenericResponse<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employeeExist = await _repository.Get(p => p.Id == request.Id);
            if (employeeExist == null)
                throw new Exceptions.ApiException($"The employee with the id {request.Id} doesnt exist");

            bool deleteResponse = await _repository.Delete(employeeExist);

            return new GenericResponse<int>(employeeExist.Id, $"The employee with the id {request.Id} has been deleted");
        }
    }
}
