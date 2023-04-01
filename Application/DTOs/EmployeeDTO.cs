using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdDepartment { get; set; }
        public int Salary { get; set; }
        public DateTime ContractDate { get; set; }
    }
}
