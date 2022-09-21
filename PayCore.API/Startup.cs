using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Paycore.Repository;
using PayCore.API.Filters;
using PayCore.API.Middlewares;
using PayCore.API.Modules;
using PayCore.API.StartUpExtension;
using PayCore.Core.Configurations;
using PayCore.Core.Models;
using PayCore.Service.Mapping;
using PayCore.Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PayCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //AUTOFAC register
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepoServiceModule());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenOption = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();

            services.AddControllersWithVAlidator();
            services.AddService();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseNpgsql(Configuration.GetConnectionString("Postgresql"), options =>
                {
                    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });
            services.AddIdentity<UserApp, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddJwtBearerAuthentication(tokenOption); //++
            
            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption")); //DI Conteinera bir nesne geçtik. Options Pattern olarak geçer. Microsoft Options Pattern.

            
            services.AddControllers();
            //***
            services.AddCustomizeSwagger();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayCore.API", Version = "v1" });
            //});

            //

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayCore Final Project"));
            }

            app.UseHttpsRedirection();

            app.UseCustomException(); //middleware 

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
