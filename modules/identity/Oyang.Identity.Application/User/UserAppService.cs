using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Repositories;
using Oyang.Identity.Application.User.Dtos;
using System;
using System.Collections.Generic;
using Oyang.Identity.Domain.Entities;
using System.Linq;

namespace Oyang.Identity.Application.User
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IUserRepository _repository;
        private readonly IObjectMapper  _objectMapper;
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
            var entity = _objectMapper.Map<AddInputDto, UserEntity>(input);
            _repository.Add(entity);
        }

        public Pagination<UserDto> GetList(GetListInputDto input)
        {
            var pagination = _repository.GetList(input, input.LoginName);
            var list = _objectMapper.Map<List<UserEntity>,List<UserDto>>(pagination.Items);
            return new Pagination<UserDto>(pagination, pagination.TotalCount, list);
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
        }

        public void SetRole(SetRoleInputDto input)
        {            
            _repository.SetRole(input.UserId,input.RoleIds);
        }

        public void Update(UpdateInputDto input)
        {
            _repository.Update(null);
        }
    }
}
