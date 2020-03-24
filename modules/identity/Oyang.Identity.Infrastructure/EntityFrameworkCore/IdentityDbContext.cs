using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Entities;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity>  Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<OrgEntity> Orgs { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<UserRoleEntity>  UserRoles { get; set; }
        public DbSet<UserOrgEntity>   UserOrgs { get; set; }
        public DbSet<RolePermissionEntity>  RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<RoleEntity>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<PermissionEntity>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<OrgEntity>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<MenuEntity>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<UserRoleEntity>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<UserOrgEntity>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<RolePermissionEntity>().HasQueryFilter(t => !t.IsDeleted);
        }

        void SetTableAndColumn(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var entityTypeBuilder = modelBuilder.Entity(entity.Name);
                string tableName = "T_" + entity.Name.Replace("Entity", "");
                entityTypeBuilder.ToTable(tableName);

                //var properties = entity.GetProperties();
                //foreach (var property in properties)
                //{
                //    var propertyBuilder = entityTypeBuilder.Property(property.Name);
                //    propertyBuilder.HasColumnName(property.Name.ToLower());
                //    if (property.Name == "Id")
                //    {
                //        propertyBuilder.HasColumnType("char(36)");
                //    }
                //    else if (property.ClrType == typeof(string))
                //    {
                //        propertyBuilder.HasColumnType("varchar(255)");
                //    }
                //}

            }
        }
    }
}
