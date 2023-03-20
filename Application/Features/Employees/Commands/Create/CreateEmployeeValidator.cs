using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeValidator:AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} cant be empty").MaximumLength(100).WithMessage("{PropertyName} cannot exceed {MaximumLenght} ");
            RuleFor(p => p.Salary).NotEmpty().WithMessage("{PropertyName} cant be empty");
            RuleFor(p => p.ContractDate).NotEmpty().WithMessage("{PropertyName} cant be empty");
            RuleFor(p => p.IdDepartment).NotEmpty().WithMessage("{PropertyName} cant be empty");

        }
    }
}
