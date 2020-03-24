using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Entities
{
    public class MenuEntity : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string SortId { get; set; }
        public bool IsEnable { get; set; }
        public Guid? ParentId { get; set; }
    }
}
