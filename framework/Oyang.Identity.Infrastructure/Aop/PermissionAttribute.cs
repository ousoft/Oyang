using AspectCore.DynamicProxy;
using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Oyang.Identity.Infrastructure.Aop
{
    public class PermissionAttribute : AbstractInterceptorAttribute
    {
        public PermissionAttribute(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
        public string Code { get; }
        public string Name { get; }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var currentUser = context.ServiceProvider.GetService<ICurrentUser>();
            ValidationObject.Validate(!currentUser.HasPermission(Code), "没有访问权限");
            await next(context);
        }
    }
}
