using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public class PermissionAttribute: Attribute
    {
        public PermissionAttribute(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
        public string Code { get; }
        public string Name { get; }
    }
}
