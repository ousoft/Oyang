using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain;
using Oyang.Identity.Infrastructure.Identity;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories
{
    //public class DatabaseRepository : EfRepository, IDatabaseRepository
    //{
    //    public DatabaseRepository(IdentityDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    //    {
    //    }

    //    public void GenerateDatabase()
    //    {
    //        DbContext.Database.EnsureCreated();
    //    }

    //    public void CleanSeedDataByPermission()
    //    {
    //        DbContext.RemoveAttachAudit(DbContext.Queryable<PermissionEntity>().ToArray());
    //    }

    //    public void GenerateSeedDataByPermission(List<PermissionModel> input)
    //    {
    //        var listPermission = Mapper.Map<PermissionEntity[]>(input);
    //        DbContext.AddAttachAudit(listPermission);
    //    }

    //}
}
