using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAll
{
    public class GetAllEmployeesParameters:RequestParameters
    {

        public string? Name { get; set; }
    }
}
