using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.Identity
{
    public class CurrentUser : ICurrentUser
    {
        public CurrentUser(
            Guid id,
            string loginName,
            string passwordHash
            //List<RoleDto> roles,
            //List<PermissionDto> permissions,
            //List<MenuDto> menus
            )
        {
            this.Id = id;
            this.LoginName = loginName;
            this.PasswordHash = passwordHash;
            //this.Roles = roles;
            //this.Permissions = permissions;
            //this.Menus = GetTreeMenu(menus);
        }

        public Guid Id { get; }
        public string LoginName { get; }
        public string PasswordHash { get; }
        //public IReadOnlyList<RoleDto> Roles { get; }
        //public IReadOnlyList<PermissionDto> Permissions { get; }
        //public IReadOnlyList<MenuDto> Menus { get; }

        public bool HasRole(string name)
        {
            //return Roles.Any(t => t.Name == name);
            return true;
        }
        public bool HasPermission(string code)
        {
            //return Permissions.Any(t => t.Code == code);
            return true;
        }

        //private List<MenuDto> GetTreeMenu(List<MenuDto> menus)
        //{
        //    var list = menus.Where(t => t.ParentId == null).ToList();
        //    LoadTreeMenu(menus, list);
        //    return list;
        //}

        //private void LoadTreeMenu(List<MenuDto> menuAllList, List<MenuDto> list)
        //{
        //    foreach (var item in list)
        //    {
        //        item.Children = menuAllList.Where(t => t.ParentId == item.Id).ToList();
        //        if (item.Children.Count > 0)
        //        {
        //            LoadTreeMenu(menuAllList, item.Children);
        //        }
        //    }
        //}
    }
}
