using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.HasOne(f => f.IdDepartmentNavigation).WithMany(f => f.Employes).HasForeignKey(f => f.IdDepartment)
                .HasConstraintName("FK_Employe_Departments");
            builder.Property(p => p.Salary).IsRequired();
            builder.Property(p => p.ContractDate).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        }
    }
}
