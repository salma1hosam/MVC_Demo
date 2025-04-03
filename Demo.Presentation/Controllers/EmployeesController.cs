using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.BusinessLogic.Services.Interfaces;
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
    }
}
