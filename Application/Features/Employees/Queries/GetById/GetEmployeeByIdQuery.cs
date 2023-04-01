using Application.DTOs;
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

namespace Application.Features.Employees.Queries.GetById
{
    public class GetEmployeeByIdQuery: IRequest<GenericResponse<EmployeeDTO>>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GenericResponse<EmployeeDTO>>
{
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Employee> _repository;

        public GetEmployeeByIdQueryHandler(IRepositoryAsync<Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<EmployeeDTO>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _repository.Get(p => p.Id == request.Id);

            if (employee == null)
                throw new Exceptions.ApiException("Employee not fuounded");

            EmployeeDTO dtoResponse = _mapper.Map<EmployeeDTO>(employee);
            return new GenericResponse<EmployeeDTO>(dtoResponse,"Employee founded");
        }
    }
}
