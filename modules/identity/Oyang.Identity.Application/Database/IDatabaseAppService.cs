using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.Database
{
    //[Permission(PermissionNames.Database, PermissionNames.Database_Name)]
    public interface IDatabaseAppService : IApplicationService
    {
        //[Permission(PermissionNames.Database_GenerateDatabase, PermissionNames.Database_GenerateDatabase_Name)]
        bool GenerateDatabase();
        void GenerateSeedData();
    }
}
