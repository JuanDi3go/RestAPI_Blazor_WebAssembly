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

namespace Application.Features.Departments.Queries.GetById
{
    public class GetDepartmentByIdQuery:IRequest<GenericResponse<DepartmentDTO>>
    {
        public int Id { get; set; }
    }

    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, GenericResponse<DepartmentDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Department> _repositoryAsync;

        public GetDepartmentByIdQueryHandler(IRepositoryAsync<Department> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<GenericResponse<DepartmentDTO>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Department foundDepartment = await _repositoryAsync.Get(p => p.Id == request.Id);
            if (foundDepartment == null)
                throw new Exceptions.ApiException($"The element with the id {request.Id} doesnt exist");

            DepartmentDTO mapDepartment = _mapper.Map<DepartmentDTO>(foundDepartment);

            return new GenericResponse<DepartmentDTO>(mapDepartment, "Department found");
        }
    }
}
