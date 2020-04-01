using Microsoft.Extensions.DependencyInjection;
using Oyang.Identity.Application;
using Oyang.Identity.Domain;
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
            var list = new List<Assembly>();
            list.Add(Assembly.LoadFrom("Oyang.Identity.Domain"));
            list.Add(Assembly.LoadFrom("Oyang.Identity.Application"));
            list.Add(Assembly.LoadFrom("Oyang.Identity.Infrastructure"));
            var types = list.SelectMany(t => t.GetTypes());

            var listInterfaces = new List<Type>();
            var applicationInterfaces = types.Where(t => t.IsInterface && t.GetInterface(nameof(IApplicationService)) != null);
            listInterfaces.AddRange(applicationInterfaces);
            var repositoryInterfaces = types.Where(t => t.IsInterface && t.GetInterface(nameof(IRepository)) != null);
            listInterfaces.AddRange(repositoryInterfaces);

            foreach (var item in listInterfaces)
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
