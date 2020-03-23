using ApplicationCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.EntityFrameworkCore
{
    public class EfUnitOfWork: IUnitOfWork
    {
        private readonly IdentityDbContext _dbContext;
        public EfUnitOfWork(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
