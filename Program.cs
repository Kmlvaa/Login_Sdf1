using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Login.Data;
using Login.Entities;
using Login.Helper;

namespace Login
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<AppDbContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});

			builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
			{
				opt.Password.RequiredLength = 4;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
			}).AddEntityFrameworkStores<AppDbContext>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			await DataSeed.InitializeAsync(app.Services);

			app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}