using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Entities
{
    public class UserRoleEntity:Entity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
