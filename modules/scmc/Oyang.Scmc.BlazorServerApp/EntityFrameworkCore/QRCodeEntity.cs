using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Scmc.BlazorServerApp.EntityFrameworkCore
{
    public class QRCodeEntity : Entity
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? BindTime { get; set; }
        
    }
}
