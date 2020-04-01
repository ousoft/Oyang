using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Domain
{
    public interface IRangeRepository<TEntity, TKey> where TEntity : Entity
    {
        void AddRange(TEntity[] entities);
        void AddRange(IEnumerable<TEntity> entities);
        void UpdateRange(TEntity[] entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void RemoveRange(TEntity[] entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        void RemoveRange(TKey[] ids);
        void RemoveRange(IEnumerable<TKey> ids);
    }

    public interface IRangeRepository<TEntity> : IRangeRepository<TEntity, Guid> where TEntity : Entity
    {

    }
}
