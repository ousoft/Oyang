using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Entities
{
    public class UserEntity : Entity
    {
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
    }
}
