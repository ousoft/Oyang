using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using System.Reflection;

namespace Infrastructure.AspectCore
{
    public class PermissionInterceptorAttribute : AbstractInterceptor
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var permissionAttribute = context.ServiceMethod.GetCustomAttribute<PermissionAttribute>();
            if (permissionAttribute != null)
            {
                var currentUser = (ICurrentUser)context.ServiceProvider.GetService(typeof(ICurrentUser));
                ValidationObject.Validate(!currentUser.HasPermission(permissionAttribute.Code), "没有访问权限");
            }

            await next(context);
        }
    }
}
