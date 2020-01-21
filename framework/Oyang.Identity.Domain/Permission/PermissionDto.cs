using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Permission
{
    public class PermissionDto:IDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SortId { get; set; }
        public Guid? MenuId { get; set; }
        public bool IsEnable { get; set; }
    }
}
