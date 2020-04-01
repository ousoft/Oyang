using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public interface IAuditedObject<TKey>
    {
        public DateTime CreationTime { get; set; }
        public TKey CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public TKey LastModifierId { get; set; }
        public bool IsDeleted { get; set; }
        public TKey DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
