using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Domain.SeedWork
{
    public interface IRepository
    {
    }

    public interface IRepository<TAggregateRoot, TKey> : IRepository where TAggregateRoot : IAggregateRoot
    {
        void Add(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        void Remove(TAggregateRoot aggregateRoot);
        TAggregateRoot Find(TKey id);
    }

    public interface IRepository<TAggregateRoot> : IRepository<TAggregateRoot, Guid> where TAggregateRoot : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }

}
