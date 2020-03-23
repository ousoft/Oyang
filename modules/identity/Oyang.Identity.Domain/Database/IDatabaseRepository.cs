using Oyang.Identity.Domain.Permission;
using Oyang.Identity.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Database
{

    public interface IDatabaseRepository : IRepository
    {
        void GenerateDatabase();

        void GenerateSeedDataByPermission(List<PermissionDto> input);
        void CleanSeedDataByPermission();

    }
}
