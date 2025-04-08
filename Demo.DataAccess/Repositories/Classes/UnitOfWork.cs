using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly Lazy<IDepartmentRepository> _departmentRepository;
		private Lazy<IEmployeeRepository> _employeeRepository;
        
		public UnitOfWork(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
			_departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
			_employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
		}
		public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

		public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

		public int SaveChanges() => _dbContext.SaveChanges();
	}
}
