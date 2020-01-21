using Oyang.Identity.Domain;
using Oyang.Identity.Domain.User;
using Oyang.Identity.Infrastructure.Aop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.User
{
    public interface IUserAppService : IApplicationService
    {
        [PermissionInterceptor(PermissionNames.User_GetList, PermissionNames.User_GetList_Name)]
        Pagination<UserDto> GetList(GetListInputDto input);
        [PermissionInterceptor(PermissionNames.User_Add, PermissionNames.User_Add_Name)]
        void Add(AddInputDto input);
        [PermissionInterceptor(PermissionNames.User_Update, PermissionNames.User_Update_Name)]
        void Update(UpdateInputDto input);
        [PermissionInterceptor(PermissionNames.User_Remove, PermissionNames.User_Remove_Name)]
        void Remove(Guid id);
        [PermissionInterceptor(PermissionNames.User_ResetPassword, PermissionNames.User_ResetPassword_Name)]
        void ResetPassword(ResetPasswordInputDto input);
        [PermissionInterceptor(PermissionNames.User_SetRole, PermissionNames.User_SetRole_Name)]
        void SetRole(SetRoleInputDto input);
    }
}
