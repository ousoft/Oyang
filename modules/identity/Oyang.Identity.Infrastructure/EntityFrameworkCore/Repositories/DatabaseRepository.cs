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
using Oyang.Identity.Domain.Database;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories
{
    public class DatabaseRepository : EfRepository, IDatabaseRepository
    {
        public DatabaseRepository(IdentityDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public void GenerateDatabase()
        {
            DbContext.Database.EnsureCreated();
        }

        public void CleanSeedDataByPermission()
        {
            DbContext.RemoveAttachAudit(DbContext.Queryable<PermissionEntity>().ToArray());
        }

        public void GenerateSeedDataByPermission(List<PermissionDto> input)
        {
            var listPermission = Mapper.Map<PermissionEntity[]>(input);
            DbContext.AddAttachAudit(listPermission);
        }

    }
}
