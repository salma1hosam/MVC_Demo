using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService , 
                                     IWebHostEnvironment _environment , 
                                     ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }

        #region Create Employee

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] //Action Filter (check for the token that submits the form in the request)
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var createdEmployeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate = employeeViewModel.HiringDate,
                        Salary = employeeViewModel.Salary,
                        IsActived = employeeViewModel.IsActived,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image
                    };
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
            return View(employeeViewModel);
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
           
            var employeeViewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                IsActived = employee.IsActived,
                HiringDate = employee.HiringDate,
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                Gender = Enum.Parse<Gender>(employee.Gender),
                DepartmentId = employee.DepartmentId,
                ImageName = employee.ImageName
            };

            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id , EmployeeViewModel employeeViewModel)
        {
            if(!id.HasValue) return BadRequest();
            if(!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var updatedEmployeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    HiringDate= employeeViewModel.HiringDate,
                    Salary= employeeViewModel.Salary,
                    IsActived = employeeViewModel.IsActived,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    DepartmentId = employeeViewModel.DepartmentId,
                    Image = employeeViewModel.Image
                };
                int returnedRows = _employeeService.UpdateEmployee(updatedEmployeeDto);
                if (returnedRows > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Updated");
                    return View(employeeViewModel);
                }    
            }
            catch (Exception ex)
            {
                if(_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty , ex.Message);
                    return View(employeeViewModel);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView" , ex);
                }
            }
        }

        #endregion

        #region Delete Employee

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try
            {
                var isDeleted = _employeeService.DeleteEmployee(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Deleted");
                    return RedirectToAction(nameof(Delete), new { Id = id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return RedirectToAction(nameof(Index));
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);   
                }
            }
        }

        #endregion
    }
}
