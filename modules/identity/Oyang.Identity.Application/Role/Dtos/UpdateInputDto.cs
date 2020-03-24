using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.Role.Dtos
{
    public class UpdateInputDto : IInputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
