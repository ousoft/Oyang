using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore.Entities
{
    public class RoleEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
