using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Domain
{
    public interface IEfRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>, IRangeRepository<TEntity, TKey>, IReadOnlyRepository<TEntity, TKey>
        where TEntity : Entity
    {
        IQueryable<TEntity> Queryable { get; }
        IQueryable<TEntity> QueryableAsNoTracking { get; }
        TEntity Find(TKey id);
    }
    public interface IEfRepository<TEntity> : IEfRepository<TEntity, Guid>
        where TEntity : Entity
    {

    }
}
