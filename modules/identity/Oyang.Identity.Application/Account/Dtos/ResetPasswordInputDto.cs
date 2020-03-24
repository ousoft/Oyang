using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.Account.Dtos
{
    public class ResetPasswordInputDto : IInputDto
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }

    }
}
