using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Entities;
using Oyang.Identity.Domain.Repositories;
using Oyang.Identity.Infrastructure.Identity;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories
{
    public class DatabaseRepository : AuditRepository, IDatabaseRepository
    {
        private readonly IdentityDbContext _dbContext;

        public DatabaseRepository(DbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
        {
        }

        public void GenerateDatabase()
        {
            _dbContext.Database.EnsureCreated();
        }

        public void CleanSeedDataByPermission()
        {
            _dbContext.RemoveRange(_dbContext.Set<PermissionEntity>());
        }

        public void GenerateSeedDataByPermission(List<PermissionEntity> input)
        {
            AddAttachAudit(input.ToArray());
        }

    }
}
