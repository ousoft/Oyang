using Oyang.Identity.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Database
{

    public interface IDatabaseRepository : IRepository
    {
        void GenerateDatabase();
        void GenerateSeedData(GenerateSeedDataInputDto ipnut);
        void ClearSeedData();
    }
}
