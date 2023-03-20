using Domain.AuditableBaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employe:BaseEntity
    {
        public string Name { get; set; }
        public int IdDepartment { get; set; }
        public int Salary { get; set; }
        public DateTime ContractDate { get; set; }
        public virtual Department? IdDepartmentNavigation { get; set; }
    }
}
