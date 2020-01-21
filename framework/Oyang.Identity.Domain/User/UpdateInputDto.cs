using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.User
{
    public class UpdateInputDto : IInputDto
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
    }
}
