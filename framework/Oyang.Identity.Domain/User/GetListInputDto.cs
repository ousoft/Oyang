using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.User
{
    public class GetListInputDto : Pagination, IInputDto
    {
        public string LoginName { get; set; }
    }
}
