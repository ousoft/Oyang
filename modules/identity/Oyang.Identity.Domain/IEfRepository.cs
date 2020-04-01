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
    }
    public interface IEfRepository<TEntity> : IBasicRepository<TEntity>, IRangeRepository<TEntity>, IReadOnlyRepository<TEntity>
        where TEntity : Entity
    {

    }
}
