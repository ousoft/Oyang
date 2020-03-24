using Oyang.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Repositories
{
    public interface IRoleRepository : IRepository<RoleEntity>
    {
        //Pagination<RoleEntity> GetList(Pagination pagination, string keyword);
        //void SetUser(Guid roleId, List<Guid> userIds);
        //bool ExistRole(string loginName);
    }
}
