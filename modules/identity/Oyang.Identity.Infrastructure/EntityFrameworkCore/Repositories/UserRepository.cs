
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Entities;
using Oyang.Identity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories
{
    public class UserRepository : EfRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IdentityDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
        {
        }

        public bool ExistUser(string loginName)
        {
            return Queryable.Any(t => t.LoginName == loginName);
        }

        public Pagination<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var query = Queryable;
            query = query.WhereIf(!string.IsNullOrWhiteSpace(keyword), t => t.LoginName.Contains(keyword));
            var (list, totalCount) = query.ToPagination(pagination);
            var p = new Pagination<UserEntity>(pagination, totalCount, list);
            return p;
        }

        public void SetRole(Guid userId, List<Guid> roleIds)
        {
            var list = DbContext.Set<UserRoleEntity>().Where(t => t.UserId == userId).ToList();
            var listRemove = list.Where(t => !roleIds.Contains(t.RoleId)).ToArray();
            RemoveAttachAudit(listRemove);

            var listExistRoleId = list.Select(t => t.RoleId).ToList();
            var listAdd = roleIds.Where(t => !listExistRoleId.Contains(t))
                .Select(t => new UserRoleEntity()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RoleId = t,
                });
            DbContext.Set<UserRoleEntity>().AddRange(listAdd);
        }
    }
}
