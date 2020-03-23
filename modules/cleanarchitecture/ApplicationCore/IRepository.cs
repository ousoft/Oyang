using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public interface IRepository
    {

    }
    public interface IRepository<TAggregateRoot> : IRepository where TAggregateRoot : IAggregateRoot
    {
        List<TAggregateRoot> GetList(ISpecification<TAggregateRoot> spec);
        TAggregateRoot Get(ISpecification<TAggregateRoot> spec);
        void Create(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        void Remove(TAggregateRoot aggregateRoot);
    }
}
