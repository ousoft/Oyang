using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oyang.Identity.Infrastructure.EntityFrameworkCore;
using AspectCore.Configuration;
using AspectCore.DependencyInjection;
using AspectCore.Extensions.DependencyInjection;
using Oyang.Identity.Domain;
using Oyang.Identity.Web.Middlewares;
using Oyang.Identity.Web.Extensions;
using Oyang.Identity.Application.User;
using Oyang.Identity.Infrastructure.AspectCore;
using Oyang.Identity.Infrastructure.AutoMapper;
using Oyang.Identity.Infrastructure.Identity;
using AutoMapper;

namespace Oyang.Identity.Web
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
            services.AddControllersWithViews();
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddCors(options =>
            {
                var originsString = Configuration.GetValue<string>("Origins");
                var origins = originsString.Split(',');
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddScoped<IMapper>(t =>
            {
                var mapperConfiguration = new MapperConfiguration(t =>
                {
                    t.AddProfile<IdentityProfile>();
                });
                return mapperConfiguration.CreateMapper();
            });
            services.AddSingleton<Domain.IObjectMapper, ObjectMapper>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddOyangIdentity();

            //services.ConfigureDynamicProxy(t => t.Interceptors.AddTyped<PermissionInterceptor>());
            //services.BuildDynamicProxyProvider();
        }
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterAssemblyTypes(typeof(Program).Assembly).
        //        Where(x => x.Name.EndsWith("service", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
        //    builder.RegisterDynamicProxy();

        //}
        //public void ConfigureContainer(IServiceCollection builder)
        //{

        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.UseMiddleware<DomainExceptionMiddleware>();
                });
                //app.UseExceptionHandler("/Home/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }




            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
