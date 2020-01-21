using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Role
{
    public class SetUserInputDto : IInputDto
    {
        public Guid RoleId { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
