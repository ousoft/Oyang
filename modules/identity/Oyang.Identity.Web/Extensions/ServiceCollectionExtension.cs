using Microsoft.Extensions.DependencyInjection;
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Entities;
using Oyang.Identity.Domain.Repositories;
using Oyang.Identity.Infrastructure.EntityFrameworkCore;
using Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories;
using Oyang.Identity.Application.Database;
using Oyang.Identity.Application.Account;
using Oyang.Identity.Application.User;
using Oyang.Identity.Application.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Oyang.Identity.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddOyangIdentity(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();
            services.AddScoped<IRepository<UserEntity>, EfRepository<UserEntity>>();
            services.AddScoped<IRepository<RoleEntity>, EfRepository<RoleEntity>>();
            services.AddScoped<IRepository<PermissionEntity>, EfRepository<PermissionEntity>>();
            services.AddScoped<IRepository<UserRoleEntity>, EfRepository<UserRoleEntity>>();
            services.AddScoped<IRepository<RolePermissionEntity>, EfRepository<RolePermissionEntity>>();

            services.AddScoped<IDatabaseAppService, DatabaseAppService>();
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IRoleAppService, RoleAppService>();

            return services;
        }
    }
}
