using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService,
                                       ILogger<DepartmentsController> _logger,
                                       IWebHostEnvironment _environment) : Controller
    {
        //GET : BaseURL/Departments/Index
        [HttpGet] //Default
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto createdDepartmentDto)
        {
            if (ModelState.IsValid) //Server-Side Validation
            {
                try
                {
                    int returnedRows = _departmentService.AddDepartment(createdDepartmentDto);
                    //Usually Not Done in the Create Action (Mostly in Update Action)
                    if (returnedRows > 0)
                        //return View(nameof(Index) , _departmentService.GetAllDepartments());
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Department Can't Be Created");
                }
                catch (Exception ex)
                {
                    //Log Exception In:
                    //Develpment => Log Error in Console (Allready Done By Default) and return the same View with Error Message.
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    //Deployment => Log Error in a File Or Table in Databse and return Error View Based on the error type.
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(createdDepartmentDto);
        }

        #endregion
    }
}
