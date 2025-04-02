using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.DataAccess.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    public static class EmployeeFactory
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActived,
                Email = employee.Email,
                Salary = employee.Salary,
                EmpGender = employee.Gender.ToString(),
                EmpType = employee.EmployeeType.ToString()
            };
        }

        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                IsActive = employee.IsActived,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                Gender= employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                CreatedBy = 1,
                CreatedOn = employee.CreatedOn,
                LastModifiedBy = 1,
                LastModifiedOn = employee.LastModifiedOn
            };
        }

        
    }
}
