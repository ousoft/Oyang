using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Application.User.Dtos
{
    public class AddInputDto : IInputDto
    {
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
