using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public interface IRepository
    {

    }
    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        List<TEntity> GetList();
        TEntity Get(Guid id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(Guid id);
    }
}
