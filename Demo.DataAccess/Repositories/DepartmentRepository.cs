using Demo.DataAccess.Data.Contexts;


namespace Demo.DataAccess.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _dbContext;  // _ is the naming convention of DI 

		public DepartmentRepository(ApplicationDbContext dbContext) // 1.Injection
																	//Ask The CLR for Creating Object from ApplicationDbContext
		{
			_dbContext = dbContext;
		}

		//CRUD Operations

		//GET All
		public IEnumerable<Department> GetAll(bool withTracking = false)
		{
			if (withTracking)
				return _dbContext.Departments.ToList();
			else
				return _dbContext.Departments.AsNoTracking().ToList();
		}

		//GET By Id
		public Department? GetById(int id) => _dbContext.Departments.Find(id);  //Find() takes a PK as a parameter

		//Update
		public int Update(Department department)
		{
			_dbContext.Departments.Update(department);
			return _dbContext.SaveChanges();
		}

		//Delete
		public int Remove(Department department)
		{
			_dbContext.Departments.Remove(department);
			return _dbContext.SaveChanges();
		}

		//Insert
		public int Add(Department department)
		{
			_dbContext.Add(department);
			return _dbContext.SaveChanges();
		}
	}
}
