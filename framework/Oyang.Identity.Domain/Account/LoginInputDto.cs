using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Account
{
    public class LoginInputDto:IInputDto
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
