using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.User.Dtos
{
    public class UpdateInputDto : IInputDto
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
    }
}
