using Microsoft.Extensions.DependencyInjection;
using Oyang.Identity.Application;
using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddOyangIdentity(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes());
            var applicationInterfaces = types.Where(t => t.IsInterface && t.GetInterface(nameof(IApplicationService)) != null);
            foreach (var item in applicationInterfaces)
            {
                var implementationType = types.FirstOrDefault(t => t.IsClass && t.GetInterface(item.Name) != null);
                if (implementationType != null)
                {
                    services.AddScoped(item, implementationType);
                }
            }

            var repositoryInterfaces = types.Where(t => t.IsInterface && t.GetInterface(nameof(IRepository)) != null);
            foreach (var item in repositoryInterfaces)
            {
                var implementationType = types.FirstOrDefault(t => t.IsClass && t.GetInterface(item.Name) != null);
                if (implementationType != null)
                {
                    services.AddScoped(item, implementationType);
                }
            }
            return services;
        }
    }
}
