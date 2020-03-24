using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Entities
{
    public class UserOrgEntity : Entity
    {
        public Guid UserId { get; set; }
        public Guid OrgId { get; set; }
    }
}
