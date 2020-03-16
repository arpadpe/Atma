using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AtmaAPI.Data;
using AtmaAPI.Repository;
using AtmaAPI.Repository.Interface;
using AtmaAPI.Services;
using AtmaAPI.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SimpleInjector;

namespace AtmaAPI
{
    public class Startup
    {
        private readonly Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SalesContext>(opt => opt.UseInMemoryDatabase("Sales"));
            services.AddControllers();

            // Configure Swashbuckle
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Atma API", 
                    Version = "v1",
                    Description = "Atma ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Árpád Perényi",
                        Email = "arpad.perenyi@gmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Configure Simple Injector
            services.AddSimpleInjector(_container, options => {
                options.AddAspNetCore()
                .AddControllerActivation()
                .AddViewComponentActivation();
            });

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            // Register services and repositories in container
            _container.Register<ISalesService, SalesService>(Lifestyle.Scoped);
            _container.Register<ISalesRepository, SalesRepository>(Lifestyle.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Atma API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _container.Verify();
        }
    }
}
