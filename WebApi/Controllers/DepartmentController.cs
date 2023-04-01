using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Delete;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Queries.GetAll;
using Application.Features.Departments.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WebApi.Controllers.v1;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class DepartmentController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateDepartments(CreateDepartmentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(DeleteDepartmentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            return Ok(await Mediator.Send(new GetDepartmentByIdQuery { Id = id }));
        }

        [HttpGet]

        public async Task<IActionResult> GetAllDepartments([FromQuery] GetAllDepartmentsParameters filter )
        {
            return Ok(await Mediator.Send(new GetAllDepartmentsQuery {PageNumber = filter.PageNumber, PageSize = filter.PageSize, Name = filter.Name }));
        }

        
    }
}
