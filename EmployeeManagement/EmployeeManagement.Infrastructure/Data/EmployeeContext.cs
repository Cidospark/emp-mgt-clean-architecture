using System;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {
        }

        public DbSet<EmployeeManagement.Core.Entities.Employee> Employees { get; set; }
    }
}

