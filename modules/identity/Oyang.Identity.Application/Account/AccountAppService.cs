using Oyang.Identity.Application.Account.Dtos;
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Repositories;
using System;

namespace Oyang.Identity.Application.Account
{
    public class AccountAppService : BaseAppService, IAccountAppService
    {
        private readonly IAccountRepository _repository;
        public AccountAppService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public ICurrentUser Login(LoginInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.LoginName), "登录名不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password), "密码不能为空");
            var userDto = _repository.Get(input.LoginName);
            ValidationObject.Validate(userDto == null, "登录名不存在");
            ValidationObject.Validate(userDto.PasswordHash != input.Password, "密码不正确");

            var currentUser = _repository.Get(userDto.Id);
            return currentUser;
        }
    }
}
