using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Org
{
    public class OrgDto : IDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SortId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
