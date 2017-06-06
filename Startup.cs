using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace dotnet_new_ember
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // add the db context like you normally would
      services.AddDbContext<AppDbContext>(options =>
      { // use whatever provider you want, this is just an example
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
      }, ServiceLifetime.Transient);

      // add jsonapi dotnet core
      services.AddJsonApi<AppDbContext>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app, 
        IHostingEnvironment env, 
        ILoggerFactory loggerFactory,
        AppDbContext context)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      context.Database.EnsureCreated();
      if (context.People.Any() == false)
      {
        context.People.Add(new Person
        {
          Name = "John Doe"
        });
        context.SaveChanges();
      }

      app.UseJsonApi();
    }
  }
}
