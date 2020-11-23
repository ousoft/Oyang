using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Domain.SeedWork
{
    public abstract class Entity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }

    public abstract class Entity : Entity<Guid>
    {

    }
}
