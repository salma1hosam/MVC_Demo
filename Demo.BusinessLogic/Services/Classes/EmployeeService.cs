using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;


namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService : IEmployeeService
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }

        public int CreateEmployee(CreatedEmployeeDto createdEmployeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(createdEmployeeDto);
            _unitOfWork.EmployeeRepository.Add(employee);  //Add Locally
            return _unitOfWork.SaveChanges();
        }


        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
			}
        }

		public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
		{

			#region Using 3rd Overload of GetAll() [selector]
			//var employeesDto = _employeeRepository.GetAll(E => new EmployeeDto()
			//{
			//    Id = E.Id,
			//    Name = E.Name,
			//    Age = E.Age,
			//    Salary = E.Salary
			//}).Where(E => E.Age > 25); // IEnumerable Where() => Filteration on the returned Result in the Memory 
			#endregion

			IEnumerable<Employee> employees;
			if (string.IsNullOrWhiteSpace(EmployeeSearchName))
				employees = _unitOfWork.EmployeeRepository.GetAll();
			else
				employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

			//Source => Employee
			//Destination => EmployeeDto
			var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

			return employeesDto;
		}

		public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is not null ? _mapper.Map<Employee, EmployeeDetailsDto>(employee) : null;
        }

        public int UpdateEmployee(UpdatedEmployeeDto updatedEmployeeDto)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(updatedEmployeeDto));
            return _unitOfWork.SaveChanges();
        }
    }
}
