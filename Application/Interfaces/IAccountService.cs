using Application.DTOs.User;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<GenericResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request,string ipAddress);

        Task<GenericResponse<string>> RegisterAsync(RegisterRequest request, string origin);
    }
}
