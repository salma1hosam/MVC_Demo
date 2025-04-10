using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Add services to the container.
			builder.Services.AddControllersWithViews(options =>
			{
				//All Actions will be checked for the token that submits the form in the request by AutoValidateAntiforgeryTokenAttribute
				options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
			});

			//builder.Services.AddScoped<ApplicationDbContext>();  //2.Register to Service in the DI Container

			//Another way to register the DbContext service with configuring the DBContextOptions
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				//3 Ways to get the Connection String from the appsettings.json
				//options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
				//options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);  //Used to get any Section in the appsettings.json
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  //Most Used

				options.UseLazyLoadingProxies();
			});

			builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //2.Registeration
			builder.Services.AddScoped<IDepartmentService, DepartmentService>();

			builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			builder.Services.AddScoped<IEmployeeService, EmployeeService>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IAttachmentService, AttachmentService>();

			#region Registering AutoMapper
			builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly); //Gets the Assembly that contains the Mapping Profiles (if it's public)

			//builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles())); //Add a profile using AddProfile and takes an object that inherit from Profile Base Class (if it's public) 
			//																		    //(But you've to add each profile separatly if you've separet profile for each Module)

			//builder.Services.AddAutoMapper(typeof(ProjectReference).Assembly); //An Empty Public Class exists in the same Assembly of the Profiles just to get the Assembly (In Case the Profiles are not public) 
			#endregion


			//Registering the Identity Services
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
							.AddEntityFrameworkStores<ApplicationDbContext>(); //To Allow the Identity services Implementations , Specify the DbContext

			#region To change the the Configuration (for ex of user or Password)
			//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			//{
			//	//Default
			//	options.User.RequireUniqueEmail = true;
			//	options.Password.RequireUppercase = true;
			//	options.Password.RequireDigit = true;
			//}).AddEntityFrameworkStores<ApplicationDbContext>(); 
			#endregion

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
				pattern: "{controller=Account}/{action=Register}/{id?}");

			#endregion

			app.Run();
		}
	}
}
