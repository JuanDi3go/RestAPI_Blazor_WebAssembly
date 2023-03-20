using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departments.Commands.Delete
{
    public class DeleteDepartmentCommandValidator:AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("The {PropertyName} cant be empty");
        }
    }
}
