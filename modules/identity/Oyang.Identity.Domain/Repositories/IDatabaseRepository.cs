
using Oyang.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Repositories
{

    public interface IDatabaseRepository : IRepository
    {
        bool GenerateDatabase();
    }
}
