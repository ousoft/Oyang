using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.User.Dtos
{
    public class SetRoleInputDto : IInputDto
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
