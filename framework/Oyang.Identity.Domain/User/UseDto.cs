using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.User
{
    public class UserDto : IDto
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
        public string NickName { get; set; }
    }
}
