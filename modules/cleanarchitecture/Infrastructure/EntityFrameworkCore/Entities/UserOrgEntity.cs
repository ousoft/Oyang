using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore.Entities
{
    public class UserOrgEntity : Entity
    {
        public Guid UserId { get; set; }
        public Guid OrgId { get; set; }
    }
}
