using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Domain
{
    public interface IBasicRepository<TEntity, TKey> where TEntity : Entity
    {
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(TKey id);
    }

    public interface IBasicRepository<TEntity> : IBasicRepository<TEntity, Guid> where TEntity : Entity
    {

    }

}
