using System;
using System.Collections.Generic;
using System.Linq;
using Oyang.Ddd.Application.User.Dtos;
using Oyang.Ddd.Domain.UserAggregate;

namespace Oyang.Ddd.Application.User
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IUserRepository _repository;

        public UserAppService(
            IUserRepository repository
            )
        {
            _repository = repository;
        }

        public void Add(AddInputDto input)
        {
            var user = new UserAggregateRoot(input.LoginName, input.NickName, input.Password);
            user.Validate();
            _repository.Add(user);
            _repository.UnitOfWork.Commit();
        }
    }
}
