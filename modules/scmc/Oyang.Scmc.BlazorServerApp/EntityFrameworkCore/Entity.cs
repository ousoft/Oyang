using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Scmc.BlazorServerApp.EntityFrameworkCore
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
