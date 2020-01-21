using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{
    public abstract class EfRepository : IRepository
    {
        public EfRepository(IdentityDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public EfRepository(IdentityDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        protected IdentityDbContext DbContext { get; }
        protected IMapper Mapper { get; }

    }
    public class EfRepository<TEntity> : EfRepository where TEntity : Entity
    {
        public EfRepository(IdentityDbContext dbContext) : base(dbContext)
        {
            Queryable = dbContext.Queryable<TEntity>().AsNoTracking();
        }

        public EfRepository(IdentityDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            Queryable = dbContext.Queryable<TEntity>().AsNoTracking();
        }

        protected IQueryable<TEntity> Queryable { get; }

    }
}



