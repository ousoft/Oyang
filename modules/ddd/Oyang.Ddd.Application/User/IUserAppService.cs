using Oyang.Ddd.Application.User.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Application.User
{
    public interface IUserAppService : IApplicationService
    {
        void Add(AddInputDto input);
        //Pagination<UserDto> GetList(GetListInputDto input);
        //void Add(AddInputDto input);
        //void Update(UpdateInputDto input);
        //void Remove(Guid id);
        //void ResetPassword(ResetPasswordInputDto input);
        //void SetRole(SetRoleInputDto input);
    }
}
