using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.Account
{
    public interface IAccountAppService : IApplicationService
    {
        ICurrentUser Login(LoginInputDto input);
    }
}
