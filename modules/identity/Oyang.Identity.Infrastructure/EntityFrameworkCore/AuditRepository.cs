using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{
    public abstract class AuditRepository
    {
        private readonly DbContext _dbContext;
        private readonly ICurrentUser _currentUser;  
        public AuditRepository(DbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

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
                if (_currentUser != null)
                {
                    item.CreatorId = _currentUser.Id;
                }
            }
        }
        public void SetUpdateAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            var time = DateTime.Now;
            foreach (var item in entities)
            {
                item.LastModificationTime = time;
                if (_currentUser != null)
                {
                    item.LastModifierId = _currentUser.Id;
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
                if (_currentUser != null)
                {
                    item.DeleterId = _currentUser.Id;
                }
            }
        }

        public void SetAddAudit<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            SetAddAudit(entities.ToArray());
        }
        public void SetUpdateAudit<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            SetUpdateAudit(entities.ToArray());
        }
        public void SetRemoveAudit<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            SetRemoveAudit(entities.ToArray());
        }



        //public void AddAttachAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        //{
        //    SetAddAudit(entities);
        //    var entityObjects = entities.Select(t => (object)t);
        //    _dbContext.AddRange(entityObjects);
        //}
        //public void UpdateAttachAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        //{
        //    SetUpdateAudit(entities);
        //    var entityObjects = entities.Select(t => (object)t);
        //    _dbContext.UpdateRange(entityObjects);
        //}
        //public void RemoveAttachAudit<TEntity>(params TEntity[] entities) where TEntity : Entity
        //{
        //    SetRemoveAudit(entities);
        //}
        //public void RemoveAttachAudit<TEntity>(params Guid[] ids) where TEntity : Entity
        //{
        //    var entities = _dbContext.Set<TEntity>().Where(t => ids.Contains(t.Id)).ToArray();
        //    RemoveAttachAudit(entities);
        //}
        //public void RemoveAttachAudit<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : Entity
        //{
        //    var entities = _dbContext.Set<TEntity>().Where(predicate).ToArray();
        //    RemoveAttachAudit(entities);
        //}
    }
}
