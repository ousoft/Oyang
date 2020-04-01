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

        public DatabaseRepository(IdentityDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
        {
            _dbContext = dbContext;
        }

        bool IDatabaseRepository.GenerateDatabase()
        {
            return _dbContext.Database.EnsureCreated();
        }
    }
}
