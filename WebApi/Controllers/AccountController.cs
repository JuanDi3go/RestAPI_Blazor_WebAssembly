using Application.DTOs.User;
using Application.Features.Users.Commands.AuthenticateUser;
using Application.Features.Users.Commands.RegisterCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.v1;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok( await Mediator.Send(new AuthenticateUserCommand
            {
                Email = request.Email,
                IpAddress = GenerateIpAddress(),
                Password = request.Password
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok( await Mediator.Send(new RegisterUserCommand
            {
                Email = request.Email,
                Origin = GenerateIpAddress(),
                Password = request.Password,
                UserName = request.UserName,
                Name = request.UserName,
                LastName = request.UserName,
                ConfirmPassword = request.ConfirmPassword   
                
            }));
        }

        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            
        }
    }
}
