using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.Role.Dtos
{
    public class GetListInputDto : Pagination, IInputDto
    {
        public string Name { get; set; }
    }
}
