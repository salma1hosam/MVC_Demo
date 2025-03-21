using Demo.DataAccess.Repositories;


namespace Demo.BusinessLogic.Services
{
	public class DepartmentService
	{
		private readonly IDepartmentRepository _departmentRepository;

		public DepartmentService(IDepartmentRepository departmentRepository) //1.Injection
        {
			_departmentRepository = departmentRepository;
		}
    }
}
