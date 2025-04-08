using Demo.BusinessLogic.DataTransferObjects.Employee;


namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto createdEmployeeDto);
        int UpdateEmployee(UpdatedEmployeeDto updatedEmployeeDto);
        bool DeleteEmployee(int id);
    }
}
