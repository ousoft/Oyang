using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore
{
    public interface ICurrentUser
    {
        Guid Id { get; }
        string LoginName { get; }
        string PasswordHash { get; }
        //IReadOnlyList<RoleDto> Roles { get; }
        //IReadOnlyList<PermissionDto> Permissions { get; }
        //IReadOnlyList<MenuDto> Menus { get; }

        bool HasRole(string name);
        bool HasPermission(string code);
    }
}
