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

namespace Application.Features.Departments.Commands.Create
{
    public class CreateDepartmentCommand : IRequest<GenericResponse<int>>
    {
        public string Name { get; set; }
    }

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, GenericResponse<int>>
    {
        private readonly IRepositoryAsync<Department> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreateDepartmentCommandHandler(IRepositoryAsync<Department> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<GenericResponse<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department newRegister = _mapper.Map<Department>(request);
            var data = await _repositoryAsync.Create(newRegister);

            return new GenericResponse<int>(data.Id);
        }
    }
}
