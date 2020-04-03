using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{

    public class EfRepository<TEntity> : AuditRepository, IRepository<TEntity> where TEntity : Entity
    {
        protected IdentityDbContext DbContext { get; }
        protected ICurrentUser CurrentUser { get; }

        public IQueryable<TEntity> Queryable => DbContext.Set<TEntity>();

        public IQueryable<TEntity> QueryableAsNoTracking => DbContext.Set<TEntity>().AsNoTracking();

        public EfRepository(IdentityDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
        {
            DbContext = dbContext;
            CurrentUser = currentUser;
        }
        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public TEntity Find(Guid id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            SetAddAudit(entity);
            var temp = DbContext.Set<TEntity>().Add(entity);
            return temp.Entity;
        }

        public void Update(TEntity entity)
        {
            SetUpdateAudit(entity);
        }

        public void Remove(TEntity entity)
        {
            SetRemoveAudit(entity);
        }

        public void Remove(Guid id)
        {
            var entity = DbContext.Find<TEntity>(id);
            SetRemoveAudit(entity);
        }

        public void AddRange(TEntity[] entities)
        {
            SetAddAudit(entities);
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            SetAddAudit(entities);
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public void UpdateRange(TEntity[] entities)
        {
            SetUpdateAudit(entities);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            SetUpdateAudit(entities);
        }

        public void RemoveRange(TEntity[] entities)
        {
            SetRemoveAudit(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            SetRemoveAudit(entities);
        }

        public void RemoveRange(Guid[] ids)
        {
            var entities = DbContext.Set<TEntity>().Where(t => ids.Contains(t.Id));
            SetRemoveAudit(entities);
        }

        public void RemoveRange(IEnumerable<Guid> ids)
        {
            var entities = DbContext.Set<TEntity>().Where(t => ids.Contains(t.Id));
            SetRemoveAudit(entities);
        }

        public List<TEntity> GetList()
        {
            return DbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity Get(Guid id)
        {
            return DbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(t => t.Id == id);
        }
    }
}



