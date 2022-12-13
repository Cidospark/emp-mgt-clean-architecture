using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Core.Repositories.Base;

namespace EmployeeManagement.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<EmployeeManagement.Core.Entities.Employee>
    {
        Task<IEnumerable<EmployeeManagement.Core.Entities.Employee>> GetEmployeeByLastName(string lastName);
    }
}

