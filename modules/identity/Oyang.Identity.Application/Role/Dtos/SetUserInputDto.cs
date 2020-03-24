using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.Role.Dtos
{
    public class SetUserInputDto : IInputDto
    {
        public Guid RoleId { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
