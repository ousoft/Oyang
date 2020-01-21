using AutoMapper;
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Role;
using Oyang.Identity.Infrastructure.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories
{
    public class RoleRepository : EfRepository<RoleEntity>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public void Add(AddInputDto input)
        {
            var entity = Mapper.Map<RoleEntity>(input);
            DbContext.AddAttachAudit(entity);
        }

        public bool ExistRole(string name)
        {
            return Queryable.Any(t => t.Name == name);
        }

        public bool ExistRole(Guid id, string name)
        {
            return Queryable.Any(t => t.Id != id && t.Name == name);
        }

        public RoleDto Get(Guid id)
        {
            var entity = Queryable.Single(t => t.Id == id);
            var dto = Mapper.Map<RoleDto>(entity);
            return dto;
        }

        public Pagination<RoleDto> GetList(GetListInputDto input)
        {
            var query = Queryable;
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Name), t => t.Name.Contains(input.Name));
            var (list, totalCount) = query.ToPagination(input);
            var listDto = Mapper.Map<List<RoleDto>>(list);
            var pagination = new Pagination<RoleDto>(input, listDto, totalCount);
            return pagination;
        }

        public void Remove(Guid id)
        {
            var entity = DbContext.Set<RoleEntity>().Find(id);
            DbContext.RemoveAttachAudit(entity);
        }

        public void SetPermission(SetPermissionInputDto input)
        {
            var list = DbContext.Set<RolePermissionEntity>().Where(t => t.RoleId == input.RoleId).ToList();
            var listRemove = list.Where(t => !input.PermissionIds.Contains(t.PermissionId)).ToArray();
            DbContext.RemoveAttachAudit(listRemove);

            var listExisPermissionId = list.Select(t => t.PermissionId).ToList();
            var listAdd = input.PermissionIds.Where(t => !listExisPermissionId.Contains(t))
                .Select(t => new RolePermissionEntity()
                {
                    Id = Guid.NewGuid(),
                     PermissionId = t,
                    RoleId = input.RoleId,
                }).ToArray();
            DbContext.AddAttachAudit(listAdd);
        }

        public void SetUser(SetUserInputDto input)
        {
            var list = DbContext.Set<UserRoleEntity>().Where(t => t.RoleId == input.RoleId).ToList();
            var listRemove = list.Where(t => !input.UserIds.Contains(t.UserId));
            DbContext.Set<UserRoleEntity>().RemoveRange(listRemove);

            var listExistUserId = list.Select(t => t.UserId).ToList();
            var listAdd = input.UserIds.Where(t => !listExistUserId.Contains(t))
                .Select(t => new UserRoleEntity()
                {
                    Id = Guid.NewGuid(),
                    UserId = t,
                    RoleId = input.RoleId,
                });
            DbContext.Set<UserRoleEntity>().AddRange(listAdd);
        }

        public void Update(UpdateInputDto input)
        {
            var entity = DbContext.Set<RoleEntity>().Find(input.Id);
            Mapper.Map(input, entity);
        }
    }
}
