using System;
using System.Collections.Generic;
using EmployeeManagement.Core.Caching;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Application.Querys
{
	public class AllEmployeeQuery
	{
        private readonly ICacheProvider<Employee> cacheProvider;

        public AllEmployeeQuery(ICacheProvider<Employee> cacheProvider)
		{
            this.cacheProvider = cacheProvider;
        }

		public async Task<IEnumerable<Employee>> GetEmployees()
		{
            var users = await cacheProvider.GetCachedResponse("Employees");
            return users;
        }
	}
}

