using Application.Exceptions;
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

namespace Application.Features.Departments.Commands.Delete
{
    public class DeleteDepartmentCommand:IRequest<GenericResponse<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, GenericResponse<int>>
    {
        private readonly IRepositoryAsync<Department> _repository;
        private readonly IMapper _mapper;

        public DeleteDepartmentCommandHandler(IRepositoryAsync<Department> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<int>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department departmentExist = await _repository.Get(d => d.Id == request.Id);

            if (departmentExist == null)
                throw new ApiException($"The department with the id {request.Id} doesnt exist");

            bool deleteResult = await _repository.Delete(departmentExist);

            return new GenericResponse<int>(departmentExist.Id, "The department was succesfull deleted");
        }
    }
}
