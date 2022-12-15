using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.Commands;
using EmployeeManagement.Application.Mappers;
using EmployeeManagement.Application.Response;
using EmployeeManagement.Core.Repositories;
using MediatR;

namespace EmployeeManagement.Application.Handlers
{
    public class CreateEmployeeHandler: IRequestHandler<CreateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _empRepo;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _empRepo = employeeRepository;
        }

        public async Task<EmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var empEntity = EmployeeMapper.Mapper.Map<EmployeeManagement.Core.Entities.Employee>(request);
            if(empEntity is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newEmployee = await _empRepo.AddAsync(empEntity);
            var empRes = EmployeeMapper.Mapper.Map<EmployeeResponse>(newEmployee);
            return empRes;
        }
    }
}

