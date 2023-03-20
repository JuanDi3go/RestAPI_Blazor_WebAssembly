using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departments.Commands.Create
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} can't be empty").MaximumLength(80).WithMessage("{PropertyName} cant be more than {MaxLenght} words")
                .NotNull().WithMessage("{PropertyName} cant be null");
        }
    }
}
