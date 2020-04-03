using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Entities
{
    public class DataDictionaryEntity : Entity
    {
        public string Category { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Description { get; set; }
        public string SortId { get; set; }
        public Guid? ParentId { get; set; }

    }
}
