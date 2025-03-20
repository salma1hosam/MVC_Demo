

using Demo.DataAccess.Data.Configurations;
using System.Reflection;

namespace Demo.DataAccess.Data.Contexts
{
	internal class ApplicationDbContext : DbContext
	{
        public DbSet<Department> Departments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("ConnectionString");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  //Will get the currently executing project (Presentation Layer)
			//modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); //Will get the Configurations from the project that contain the DbContext
		}
	}
}
