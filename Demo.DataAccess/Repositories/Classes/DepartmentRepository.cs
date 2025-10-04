using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Repositories.Interfaces;


namespace Demo.DataAccess.Repositories.Classes
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;  // _ is the naming convention of DI 

        public DepartmentRepository(ApplicationDbContext dbContext) :base(dbContext) 
                                                                    // 1.Injection
                                                                    //Ask The CLR for Creating Object from ApplicationDbContext
        {
            _dbContext = dbContext;
        }

        
    }
}
