
namespace Demo.DataAccess.Repositories.Interfaces
{
	public interface IUnitOfWork
	{
        public IDepartmentRepository DepartmentRepository { get;}
		public IEmployeeRepository EmployeeRepository { get;}
		int SaveChanges();
    }
}
