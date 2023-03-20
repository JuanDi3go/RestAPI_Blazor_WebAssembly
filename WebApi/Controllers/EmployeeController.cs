using Application.Features.Employees.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.v1;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class EmployeeController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
