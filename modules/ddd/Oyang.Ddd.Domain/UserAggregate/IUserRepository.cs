using Oyang.Ddd.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Domain.UserAggregate
{
    public interface IUserRepository : IRepository<UserAggregateRoot>
    {
        bool IsExistLoginName(string loginName);
        bool IsExistLoginName(string loginName, Guid id);
    }
}
