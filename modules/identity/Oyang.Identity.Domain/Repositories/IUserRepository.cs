using System;
using System.Collections.Generic;
using System.Text;
using Oyang.Identity.Domain.Entities;

namespace Oyang.Identity.Domain.Repositories
{

    public interface IUserRepository : IRepository<UserEntity>
    {
        Pagination<UserEntity> GetList(Pagination pagination, string keyword);
        void SetRole(Guid userId, List<Guid> roleIds);
        bool ExistUser(string loginName);
    }
}
