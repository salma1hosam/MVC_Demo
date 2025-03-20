namespace Demo.Presentation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Add services to the container.
			builder.Services.AddControllersWithViews(); 
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
