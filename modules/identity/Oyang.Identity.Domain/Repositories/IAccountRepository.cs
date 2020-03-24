using Oyang.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Repositories
{

    public interface IAccountRepository : IRepository
    {
        UserEntity Get(string loginName);
        ICurrentUser Get(Guid userId);
        void ResetPassword(string passwordHash);
    }
}
