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

namespace Application.Features.Departments.Commands.Update
{
    public class UpdateDepartmentCommand:IRequest<GenericResponse<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, GenericResponse<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Department> _repository;

        public UpdateDepartmentCommandHandler(IRepositoryAsync<Department> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<int>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department departmentExist = await _repository.Get(p => p.Id == request.Id);
            if (departmentExist == null)
                throw new Exceptions.ApiException($"The requested department {request.Id} doesnt exist");


            departmentExist.Name = request.Name;

            var updateResult = await _repository.Update(departmentExist);

            return new GenericResponse<int>(departmentExist.Id, "Department update succesufull");
        }
    }
}
