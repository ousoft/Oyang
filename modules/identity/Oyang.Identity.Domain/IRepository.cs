using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Domain
{
    public interface IRepository
    {

    }
    public interface IRepository<TEntity> : IBasicRepository<TEntity>, IRangeRepository<TEntity>, IReadOnlyRepository<TEntity> where TEntity : Entity
    {

    }
}
