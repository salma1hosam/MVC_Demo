using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
	public class DepartmentsController(IDepartmentService _departmentService) : Controller
	{
		//GET : BaseURL/Departments/Index
		[HttpGet] //Default
		public IActionResult Index()
		{
			var departments = _departmentService.GetAllDepartments();
			return View(departments);
		}
	}
}
