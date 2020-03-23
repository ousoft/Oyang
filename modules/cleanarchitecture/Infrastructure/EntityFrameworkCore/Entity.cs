using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
