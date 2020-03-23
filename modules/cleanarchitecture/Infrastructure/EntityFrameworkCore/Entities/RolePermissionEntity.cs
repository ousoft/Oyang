using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore.Entities
{
    public class RolePermissionEntity : Entity
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
