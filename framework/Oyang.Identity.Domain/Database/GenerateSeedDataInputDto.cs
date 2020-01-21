using Oyang.Identity.Domain.Permission;

using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Database
{
    public class GenerateSeedDataInputDto : IInputDto
    {
        public List<PermissionDto>  Permissions { get; set; }
        //public List<UserDto> Users { get; set; }
        //public List<RoleDto>  Roles { get; set; }
        //public List<MenuDto>  Menus { get; set; }
        //public List<OrgDto> Orgs { get; set; }
    }
}
