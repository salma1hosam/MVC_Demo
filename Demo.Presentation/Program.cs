using Demo.BusinessLogic.Services;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Add services to the container.
			builder.Services.AddControllersWithViews();

			//builder.Services.AddScoped<ApplicationDbContext>();  //2.Register to Service in the DI Container

			//Another way to register the DbContext service with configuring the DBContextOptions
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				//3 Ways to get the Connection String from the appsettings.json
				//options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
				//options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);  //Used to get any Section in the appsettings.json
				
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  //Most Used
			});

			builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>(); //2.Registeration
			builder.Services.AddScoped<IDepartmentService , DepartmentService>();
			#endregion

			var app = builder.Build();

			#region Configure the HTTP request pipeline. (Middlewares)

			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts(); //Makes sure that all the requests are secured (under the Https protocol) in deployment phase
			}

			app.UseHttpsRedirection();  //Redirect the Http protocol to be Https

			app.UseStaticFiles();  //Routing to the static files (to wwwroot)

			app.UseRouting();  //Map the request to route of the routes in the Routing Table

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			#endregion

			app.Run();
		}
	}
}
