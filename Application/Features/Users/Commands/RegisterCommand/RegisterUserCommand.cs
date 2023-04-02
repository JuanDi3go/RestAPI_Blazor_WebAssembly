using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterCommand
{
    public class RegisterUserCommand:IRequest<GenericResponse<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, GenericResponse<string>>
    {
        private readonly IAccountService _accountService;

        public RegisterUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<GenericResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterAsync(new DTOs.User.RegisterRequest
            {
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
            }, request.Origin);
        }
    }
}
