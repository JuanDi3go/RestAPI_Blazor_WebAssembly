using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterCommand
{
    public class RegisterUserCommandValidator:AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("The User Name cant be empty");
            RuleFor(p => p.Email).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            RuleFor(p => p.Name).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            RuleFor(p => p.Password).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("The {PropertyName} cant be empty").Equal(p => p.Password).WithMessage("The {PropertyName}  has to be equal to password");
        }
    }
}
