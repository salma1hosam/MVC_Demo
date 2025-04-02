using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public int CreateEmployee(CreatedEmployeeDto createdEmployeeDto)
        {
            var employee = createdEmployeeDto.ToEntity();
            return _employeeRepository.Add(employee);
        }


        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false)
        {
            var employees = _employeeRepository.GetAll(withTracking);
            return employees.Select(E => E.ToEmployeeDto());
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is not null ? employee.ToEmployeeDetailsDto() : null;
        }

        public int UpdateEmployee(UpdatedEmployeeDto updatedEmployeeDto)
        {
            var employee = updatedEmployeeDto.ToEntity();
            return _employeeRepository.Update(employee);
        }
    }
}
