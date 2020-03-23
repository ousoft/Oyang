using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Linq;
using ApplicationCore;

namespace Infrastructure.EntityFrameworkCore
{
    public class AuditDbContext : DbContext
    {

        public AuditDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
        {
            _user = currentUser;
        }

        private readonly ICurrentUser _user;

        public void SetAddAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            var time = DateTime.Now;
            foreach (var item in entities)
            {
                item.IsDeleted = false;
                item.CreationTime = time;
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                if (_user != null)
                {
                    item.CreatorId = _user.Id;
                }
            }
        }
        public void SetUpdateAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            var time = DateTime.Now;
            foreach (var item in entities)
            {
                item.LastModificationTime = time;
                if (_user != null)
                {
                    item.LastModifierId = _user.Id;
                }
            }
        }
        public void SetRemoveAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            var time = DateTime.Now;
            foreach (var item in entities)
            {
                item.IsDeleted = true;
                item.DeletionTime = time;
                if (_user != null)
                {
                    item.DeleterId = _user.Id;
                }
            }
        }
        public void AddAttachAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            SetAddAudit(entities);
            var entityObjects = entities.Select(t => (object)t);
            base.AddRange(entityObjects);
        }
        public void UpdateAttachAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            SetUpdateAudit(entities);
            var entityObjects = entities.Select(t => (object)t);
            base.UpdateRange(entityObjects);
        }
        public void RemoveAttachAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            SetRemoveAudit(entities);
        }
        public void RemoveAttachAudit<TEntity>(params Guid[] ids) where TEntity : Entity
        {
            var entities = base.Set<TEntity>().Where(t => ids.Contains(t.Id)).ToArray();
            RemoveAttachAudit(entities);
        }
        public void RemoveAttachAudit<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity
        {
            var entities = base.Set<TEntity>().Where(predicate).ToArray();
            RemoveAttachAudit(entities);
        }
        public IQueryable<TEntity> Queryable<TEntity>(bool isEnableFilter = true) where TEntity : Entity
        {
            var query = Set<TEntity>().AsQueryable();
            if (isEnableFilter)
            {
                query = query.Where(t => !t.IsDeleted);
            }
            return query;
        }

    }
}
