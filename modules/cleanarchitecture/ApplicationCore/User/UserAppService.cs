using ApplicationCore.User.Dtos;
using System;

namespace ApplicationCore.User
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IUserRepository _repository;
        public UserAppService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Add(AddInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.LoginName), "登录名不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password), "密码不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password2), "确认密码不能为空");
            ValidationObject.Validate(input.Password != input.Password2, "密码和确认密码不一致");
            ValidationObject.Validate(_repository.ExistUser(input.LoginName), "登录名已存在");
            _repository.Add(input);
        }

        public Pagination<UserDto> GetList(GetListInputDto input)
        {
            return _repository.GetList(input);
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }

        public void ResetPassword(ResetPasswordInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password), "密码不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password2), "确认密码不能为空");
            ValidationObject.Validate(input.Password != input.Password2, "密码和确认密码不一致");
            _repository.ResetPassword(input);
        }

        public void SetRole(SetRoleInputDto input)
        {
            _repository.SetRole(input);
        }

        public void Update(UpdateInputDto input)
        {
            _repository.Update(input);
        }
    }
}
