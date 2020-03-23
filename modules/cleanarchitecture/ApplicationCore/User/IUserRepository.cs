using ApplicationCore.User.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.User
{
    public interface IUserRepository : IRepository
    {
        Pagination<UserDto> GetList(GetListInputDto input);
        UserDto Get(Guid id);
        void Add(AddInputDto input);
        void Update(UpdateInputDto input);
        void Remove(Guid id);
        void ResetPassword(ResetPasswordInputDto input);
        void SetRole(SetRoleInputDto input);
        bool ExistUser(string loginName);
    }
}
