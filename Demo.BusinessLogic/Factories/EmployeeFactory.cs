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
                IsActived = employee.IsActived,
                Email = employee.Email,
                Salary = employee.Salary,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType
            };
        }

        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDto()
            {
                Id= employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                IsActived= employee.IsActived,
                Salary= employee.Salary,
                Email= employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender= employee.Gender,
                EmployeeType = employee.EmployeeType
            };
        }

        public static Employee ToEntity(this CreatedEmployeeDto createdEmployeeDto)
        {
            return new Employee()
            {
                Name = createdEmployeeDto.Name,
                Age = createdEmployeeDto.Age,
                Address = createdEmployeeDto.Address,
                IsActived = createdEmployeeDto.IsActived,
                Salary = createdEmployeeDto.Salary,
                Email = createdEmployeeDto.Email,
                PhoneNumber = createdEmployeeDto.PhoneNumber,
                HiringDate = createdEmployeeDto.HiringDate,
                Gender = createdEmployeeDto.Gender,
                EmployeeType = createdEmployeeDto.EmployeeType,
                CreatedBy = createdEmployeeDto.CreatedBy,
                LastModifiedBy = createdEmployeeDto.LastModifiedBy
            };
        }

        public static Employee ToEntity(this UpdatedEmployeeDto updatedEmployeeDto)
        {
            return new Employee()
            {
                Id = updatedEmployeeDto.Id,
                Name = updatedEmployeeDto.Name,
                Age = updatedEmployeeDto.Age,
                Address = updatedEmployeeDto.Address,
                IsActived = updatedEmployeeDto.IsActived,
                Salary = updatedEmployeeDto.Salary,
                Email = updatedEmployeeDto.Email,
                PhoneNumber = updatedEmployeeDto.PhoneNumber,
                HiringDate = updatedEmployeeDto.HiringDate,
                Gender = updatedEmployeeDto.Gender,
                EmployeeType = updatedEmployeeDto.EmployeeType,
                CreatedBy = updatedEmployeeDto.CreatedBy,
                LastModifiedBy = updatedEmployeeDto.LastModifiedBy
            };
        }
    }
}
