using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oyang.Scmc.BlazorServerApp.EntityFrameworkCore
{
    public class ScmcDbContext : DbContext
    {
        public ScmcDbContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<QRCodeEntity> QRCodes { get; set; }
        public DbSet<LogEntity>  Logs { get; set; }
    }
}
