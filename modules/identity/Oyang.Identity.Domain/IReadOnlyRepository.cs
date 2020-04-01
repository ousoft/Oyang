using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Domain
{

    public interface IReadOnlyRepository<TEntity,TKey> where TEntity : Entity
    {
        List<TEntity> GetList();
        TEntity Get(TKey id);
    }
    public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, Guid> where TEntity : Entity
    {

    }
}
