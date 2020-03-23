using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.User.Dtos
{
    public class GetListInputDto : Pagination, IInputDto
    {
        public string LoginName { get; set; }
    }
}
