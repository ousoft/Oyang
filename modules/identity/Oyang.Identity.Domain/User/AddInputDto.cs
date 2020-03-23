using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.User
{
    public class AddInputDto : IInputDto
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string NickName { get; set; }

    }
}
