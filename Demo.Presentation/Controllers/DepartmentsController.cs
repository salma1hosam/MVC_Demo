using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentViewModel;
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
            //Because the Key and Property are the Same , the Second will override the first [Because Both are stored in the Same Dictionary]
            ViewData["Message"] = new DepartmentDto() { Name = "TestViewData"};
            ViewBag.Message = new DepartmentDto() { Name = "TestViewBag"};

            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid) //Server-Side Validation
            {
                try
                {
                    var createdDepartmentDto = new CreatedDepartmentDto()
                    {
                        Code = departmentViewModel.Code,
                        Name = departmentViewModel.Name,
                        Description = departmentViewModel.Description,
                        CreatedOn = departmentViewModel.DateOfCreation
                    };
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
            return View(departmentViewModel);
        }

        #endregion

        #region Details of Department

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound(); //404

            return View(department);
        }
        #endregion

        #region Edit Department

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();

            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };

            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id , DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedDepartemnt = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        CreatedOn = viewModel.DateOfCreation
                    };

                    var returnedRows = _departmentService.UpdateDepartment(updatedDepartemnt);

                    if (returnedRows > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Departemnt Is Not Updated");
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
                    return View("ErrorView", ex);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete Department

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue)
        //        return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null)
        //        return NotFound();
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                bool isDeleted = _departmentService.DeleteDepartment(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
                    return RedirectToAction(nameof(Delete), new { Id = id });
                }
            }
            catch (Exception ex)
            {
                //Log Exception In:
                //Develpment => Log Error in Console (Allready Done By Default) and return the same View with Error Message.
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                //Deployment => Log Error in a File Or Table in Databse and return Error View Based on the error type.
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
