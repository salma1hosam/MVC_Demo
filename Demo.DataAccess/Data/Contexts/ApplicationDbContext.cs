using Demo.DataAccess.Data.Configurations;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Demo.DataAccess.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        #region Configuring the options (DbContextOptionBuilder) through OnConfiguring method [Without Dependancy Injection]
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //	optionsBuilder.UseSqlServer("ConnectionString");  //Should be written in the appsettings.json
        //} 
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  //Will get the currently executing project (Presentation Layer)
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); //Will get the Configurations from the project that contain the DbContext

            //base OnModelCreating of the IdentityDbContext(which inherit from DbContext)
            base.OnModelCreating(modelBuilder);

            ////If You need to change the inhertied table name
            //modelBuilder.Entity<IdentityUser>().ToTable("ApplicationUsers");
        }
    }
}
