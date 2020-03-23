using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore.Entities
{
    public class OrgEntity:Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SortId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
