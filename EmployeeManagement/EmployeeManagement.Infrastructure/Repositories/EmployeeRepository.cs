using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Core.Repositories;
using Employee.Infrastructure.Data;
using Employee.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.Repositories
{
    public class EmployeeRepository: Repository<Employee.Core.Entities.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext context): base(context){}

        public async Task<IEnumerable<Core.Entities.Employee>> GetEmployeeByLastName(string lastName)
        {
            return await _context.Employees.Where(m => m.LastName == lastName).ToListAsync();
        }
    }
}

