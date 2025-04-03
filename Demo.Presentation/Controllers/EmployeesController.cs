using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService , 
                                     IWebHostEnvironment _environment , 
                                     ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        #region Create Employee

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto createdEmployeeDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    int returnedRows = _employeeService.CreateEmployee(createdEmployeeDto);
                    if (returnedRows > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Can't Create Employee");
                }
                catch (Exception ex)
                {
                    if(_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty , ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(createdEmployeeDto);
        }
        #endregion

        #region Employee Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ? NotFound() : View(employee);
        }
        #endregion

        #region Edit Employee

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeDto = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                IsActived = employee.IsActived,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };

            return View(employeeDto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id , UpdatedEmployeeDto updatedEmployeeDto)
        {
            if(!id.HasValue || id != updatedEmployeeDto.Id) return BadRequest();
            if(!ModelState.IsValid) return View(updatedEmployeeDto);
            try
            {
                int returnedRows = _employeeService.UpdateEmployee(updatedEmployeeDto);
                if (returnedRows > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Updated");
                    return View(updatedEmployeeDto);
                }    
            }
            catch (Exception ex)
            {
                if(_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty , ex.Message);
                    return View(updatedEmployeeDto);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView" , ex);
                }
            }
        }

        #endregion
    }
}
