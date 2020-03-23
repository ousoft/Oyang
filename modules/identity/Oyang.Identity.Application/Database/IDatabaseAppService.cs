using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.Database
{
    public interface IDatabaseAppService : IApplicationService
    {
        void GenerateDatabase();
        void GenerateSeedData();
        void ClearSeedData();

        void RegenerateSeedDataByPermission();
    }
}
