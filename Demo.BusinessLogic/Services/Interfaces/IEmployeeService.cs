using Demo.BusinessLogic.DataTransferObjects.Employee;


namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetEmployeeById(int id);
        int AddEmployee(CreatedEmployeeDto createdEmployeeDto);
        int UpdateEmployee(UpdatedEmployeeDto updatedEmployeeDto);
    }
}
