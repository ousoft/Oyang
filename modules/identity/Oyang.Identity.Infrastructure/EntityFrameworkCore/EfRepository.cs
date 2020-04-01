//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.EntityFrameworkCore;
//using Oyang.Identity.Domain;

//namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
//{

//    public class EfRepository<TEntity> : AuditRepository, IRepository<TEntity> where TEntity : Entity
//    {
//        protected IdentityDbContext DbContext { get; }
//        protected ICurrentUser CurrentUser { get; }
//        protected IQueryable<TEntity> Queryable { get; }

//        public EfRepository(IdentityDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
//        {
//            DbContext = dbContext;
//            CurrentUser = currentUser;
//            Queryable = dbContext.Set<TEntity>().AsNoTracking();
//        }

//        public List<TEntity> GetList()
//        {
//            return Queryable.ToList();
//        }

//        public TEntity Get(Guid id)
//        {
//            return DbContext.Find<TEntity>(id);
//        }

//        public void Add(TEntity entity)
//        {
//            AddAttachAudit(entity);
//        }

//        public void Update(TEntity entity)
//        {
//            UpdateAttachAudit(entity);
//        }

//        public void Remove(Guid id)
//        {
//            RemoveAttachAudit<TEntity>(id);
//        }
//    }
//}



