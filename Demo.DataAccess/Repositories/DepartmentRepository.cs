using Demo.DataAccess.Data.Contexts;


namespace Demo.DataAccess.Repositories
{
	internal class DepartmentRepository
	{
		private readonly ApplicationDbContext _dbContext;  // _ is the naming convention of DI 

		public DepartmentRepository(ApplicationDbContext dbContext) // 1.Injection
		//Ask The CLR for Creating Object from ApplicationDbContext
        {
			_dbContext = dbContext;
		}

		//CRUD Operations

		//GET All
		//GET By Id
		public Department? GetById(int id)
		{
			var department = _dbContext.Departments.Find(id);  //Find() takes a PK as a parameter
			return department;
		}

		//Update
		//Delete
		//Insert
    }
}
