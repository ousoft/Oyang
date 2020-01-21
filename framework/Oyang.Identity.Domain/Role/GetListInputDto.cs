using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Role
{
    public class GetListInputDto : Pagination, IInputDto
    {
        public string Name { get; set; }
    }
}
