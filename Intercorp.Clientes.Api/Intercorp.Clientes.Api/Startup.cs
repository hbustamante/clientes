using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Intercorp.Clientes.Api.Swagger;
using Intercorp.Clientes.Application.ApplicationProviders.ClientServices;
using Intercorp.Clientes.Application.ApplicationServices.ClientServices;
using Intercorp.Clientes.Domain.Interfaces.Repositories;
using Intercorp.Clientes.Repository;
using Intercorp.Clientes.Repository.EFConfigurations.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Intercorp.Clientes.Api
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //db context
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddMvc()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            SetupDependenciesInjection(services);
            SetupSwagger(services);
        }

        private void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger => {
                var contact = new Contact() { Url = SwaggerConfiguration.ContactUrl , Name = SwaggerConfiguration.ContactName};
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                                   new Info
                                   {
                                       Title = SwaggerConfiguration.DocInfoTitle,
                                       Version = SwaggerConfiguration.DocInfoVersion,
                                       Description = SwaggerConfiguration.DocInfoDescription,
                                       Contact = contact
                                   });

                var filepath = Path.Combine(AppContext.BaseDirectory, "Intercorp.Clientes.Api.xml");
                swagger.IncludeXmlComments(filepath);
            });
        }

        private void SetupDependenciesInjection(IServiceCollection services)
        {
            #region Repositories Settings

            services.AddTransient<IClientRepository, ClientRepository>();

            #endregion Repositories Settings

            #region Services Settings

            services.AddTransient<ICreateClientService, CreateClientService>();

            #endregion Services Settings

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
