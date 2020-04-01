using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }

}
