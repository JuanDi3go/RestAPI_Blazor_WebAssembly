using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Queries.GetAll;
using Application.Features.Employees.Queries.GetById;
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
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(DeleteEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        //<<Employee/{1}>>
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery { Id = id}));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] GetAllEmployeesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllEmployeesQuery { 
                Name = filter.Name, 
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize }));
        }
    }
}
