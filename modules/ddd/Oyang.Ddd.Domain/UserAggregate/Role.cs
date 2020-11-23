using Oyang.Ddd.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Domain.UserAggregate
{
    public class Role : ValueObject
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }

        public Role(
            Guid id,
            string name,
            string description
            )
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }

    }
}
