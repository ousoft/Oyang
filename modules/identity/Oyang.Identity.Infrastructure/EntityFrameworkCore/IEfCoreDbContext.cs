using Microsoft.EntityFrameworkCore;
using Oyang.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{
    public interface IEfCoreDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
