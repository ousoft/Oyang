using Oyang.Identity.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Account
{

    public interface IAccountRepository : IRepository
    {
        UserDto Get(string loginName);
        ICurrentUser Get(Guid userId);
        void ResetPassword(ResetPasswordInputDto input);
    }
}
