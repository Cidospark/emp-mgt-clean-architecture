using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Core.Repositories;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeRepository: Repository<EmployeeManagement.Core.Entities.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext context): base(context){}

        public async Task<IEnumerable<Core.Entities.Employee>> GetEmployeeByLastName(string lastName)
        {
            return await _context.Employees.Where(m => m.LastName == lastName).ToListAsync();
        }
    }
}

