using Blog.BLL.Interfaces;
using Blog.BLL.Repositories;
using Blog.DAL.Context;
using Blog.DAL.Entities;
using Demo.PL.Mapping;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.PL
{
	public class Program
	{
		
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			//Add Hangfire
			builder.Services.AddHangfire(x=>x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddHangfireServer();
			// Add services to the container.
			builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Add Dbcontext
            builder.Services.AddDbContext<BlogDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			//Authentication Configration
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.LoginPath = new PathString("/Account/Login");
					options.AccessDeniedPath = new PathString("/Home/Error");
				});
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 6;
				options.SignIn.RequireConfirmedAccount = false;
				//options.Lockout.MaxFailedAccessAttempts = 5;
			}).AddEntityFrameworkStores<BlogDbContext>();
			var app = builder.Build();
			
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
            //Add Hangfire (Should be after UseAuthorization) 
            app.UseHangfireDashboard("/dashboard");
            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Login}/{id?}");

			app.Run();
		}
	}
}