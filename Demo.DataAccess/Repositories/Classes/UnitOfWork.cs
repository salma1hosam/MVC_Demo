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
		private IDepartmentRepository _departmentRepository;
		private IEmployeeRepository _employeeRepository;
		private readonly ApplicationDbContext _dbContext;
        
		public UnitOfWork(IDepartmentRepository departmentRepository , 
			              IEmployeeRepository employeeRepository,
						  ApplicationDbContext dbContext)
        {
			_departmentRepository = departmentRepository;
			_employeeRepository = employeeRepository;
			_dbContext = dbContext;
		}
		public IDepartmentRepository DepartmentRepository => _departmentRepository;

		public IEmployeeRepository EmployeeRepository => _employeeRepository;

		public int SaveChanges() => _dbContext.SaveChanges();
	}
}
