using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Entities;
using MediatR;
using FluentValidation.AspNetCore;
using Timelogger.Api.Infrastructure.Errors;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace Timelogger.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;
		public IConfigurationRoot Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMediatR(typeof(Startup));
			services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			if (_environment.IsDevelopment())
			{
				services.AddCors();
			}

			services.AddMvc(options =>
			{
				options.EnableEndpointRouting = false;
			})
				.AddNewtonsoftJson()
				.AddFluentValidation(cfg =>
				{
					cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ErrorHandlingMiddleware>();

			if (env.IsDevelopment())
			{
				app.UseCors(builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)
					.AllowCredentials());
			}

			app.UseMvc();

			var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
			using (var scope = serviceScopeFactory.CreateScope())
			{
				SeedDatabase(scope);
			}
		}

		private static void SeedDatabase(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<ApiContext>();
			IList<Project> testProjects = new List<Project>();

			var initialProject = new Project("e-conomic Interview", DateTime.ParseExact("20220331", "yyyyMMdd", CultureInfo.InvariantCulture));

			initialProject.AddTimeLog("Creating new documents", 35);
			initialProject.AddTimeLog("Interviewing users", 55);
			initialProject.AddTimeLog("Creating new diagrams", 30);
			initialProject.AddTimeLog("Finishing last details", 45);

			testProjects.Add(initialProject);
			testProjects.Add(new Project("Fintech Interview", DateTime.ParseExact("20220531", "yyyyMMdd", CultureInfo.InvariantCulture)));
			testProjects.Add(new Project("Airport Interview", DateTime.ParseExact("20220630", "yyyyMMdd", CultureInfo.InvariantCulture)));
			testProjects.Add(new Project("Real Estate Interview", DateTime.ParseExact("20220831", "yyyyMMdd", CultureInfo.InvariantCulture)));

			context.Projects.AddRange(testProjects);

			context.SaveChanges();
		}
	}
}