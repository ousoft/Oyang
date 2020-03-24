using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Application.User.Dtos
{
    public class GetListInputDto : Pagination, IInputDto
    {
        public string LoginName { get; set; }
    }
}
