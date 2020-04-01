using Castle.DynamicProxy;
using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Oyang.Identity.Web
{
    public class MyInterceptor : IInterceptor
    {
        private readonly ICurrentUser _currentUser;
        public MyInterceptor(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public void Intercept(IInvocation invocation)
        {
            var permissionAttribute = invocation.Method.GetCustomAttribute<PermissionAttribute>();
            if (permissionAttribute != null)
            {
                ValidationObject.Validate(!_currentUser.HasPermission(permissionAttribute.Code), "没有访问权限");
            }
            invocation.Proceed();
        }
    }
}
