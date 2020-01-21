using AutoMapper;
using Oyang.Identity.Infrastructure.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain.Account;
using Oyang.Identity.Domain.Role;
using Oyang.Identity.Domain.Permission;
using Oyang.Identity.Domain.Menu;
using Oyang.Identity.Domain.User;
using Oyang.Identity.Domain;
using Oyang.Identity.Infrastructure.Identity;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories
{
    public class CurrentUserRepository : EfRepository, IAccountRepository
    {
        public CurrentUserRepository(IdentityDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public ICurrentUser Get(Guid userId)
        {
            var userEntity = DbContext.Queryable<UserEntity>().AsNoTracking().SingleOrDefault(t => t.Id == userId);

            var listRoleId = DbContext.Queryable<UserRoleEntity>().AsNoTracking().Where(t => t.UserId == userEntity.Id).Select(t => t.RoleId).ToList();
            var listRoleEntity = DbContext.Queryable<RoleEntity>().AsNoTracking().Where(t => listRoleId.Contains(t.Id)).ToList();
            var listRoleDto = Mapper.Map<List<RoleDto>>(listRoleEntity);

            var listPermissionId = DbContext.Queryable<RolePermissionEntity>().AsNoTracking().Where(t => listRoleId.Contains(t.RoleId)).Select(t => t.PermissionId).Distinct().ToList();
            var listPermissionEntity = DbContext.Queryable<PermissionEntity>().AsNoTracking().Where(t => listPermissionId.Contains(t.Id)).ToList();
            var listPermissionDto = Mapper.Map<List<PermissionDto>>(listPermissionEntity);

            var listMenuId = DbContext.Queryable<PermissionEntity>().AsNoTracking().Where(t => t.MenuId != null).Select(t => t.MenuId.Value).ToList();
            var listMenuEntity = DbContext.Queryable<MenuEntity>().AsNoTracking().Where(t => listMenuId.Contains(t.Id)).ToList();
            var listMenuDto = Mapper.Map<List<MenuDto>>(listMenuEntity);

            var currentUser = new CurrentUser(userEntity.Id, userEntity.LoginName, userEntity.PasswordHash, listRoleDto, listPermissionDto, listMenuDto);
            return currentUser;
        }

        public UserDto Get(string loginName)
        {
            var entity = DbContext.Queryable<UserEntity>().AsNoTracking().SingleOrDefault(t => t.LoginName == loginName);
            var dto = Mapper.Map<UserDto>(entity);
            return dto;
        }

        public void ResetPassword(Domain.Account.ResetPasswordInputDto input)
        {
            var entity = DbContext.Set<UserEntity>().Find(input.Id);
            Mapper.Map(input, entity);
        }

    }
}
