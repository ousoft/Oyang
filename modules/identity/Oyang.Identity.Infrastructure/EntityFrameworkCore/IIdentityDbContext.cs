using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{
    public interface IIdentityDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<PermissionEntity> Permissions { get; set; }
        DbSet<OrgEntity> Orgs { get; set; }
        DbSet<MenuEntity> Menus { get; set; }
        DbSet<UserRoleEntity> UserRoles { get; set; }
        DbSet<UserOrgEntity> UserOrgs { get; set; }
        DbSet<RolePermissionEntity> RolePermissions { get; set; }
    }
}
