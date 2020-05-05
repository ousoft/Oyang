using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Scmc.BlazorServerApp.EntityFrameworkCore
{
    public class LogEntity : Entity
    {
        public Guid QRCodeId { get; set; }
        public int ActionId { get; set; }
        public string Json { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
