using CQRS.Account.Commands.CreateAccount;
using CQRS.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CQRS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateAccountCommandHandler).GetTypeInfo().Assembly);
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MemoryDb"));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("sample", new OpenApiInfo
                {
                    Title = "Sample API Core with MediatR",
                    Version = $"SAMPLE-{Environment.Version.Major}.{Environment.Version.Minor}.{DateTime.Now:yyyyMMddHHmmss}",
                    Description = "Sample ASP.NET Core 5 REST API using MediatR"
                });

                string xmlDocFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlDocPath = Path.Combine(AppContext.BaseDirectory, xmlDocFile);
                c.IncludeXmlComments(xmlDocPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/sample/swagger.json", "Mediator.API.Core");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
