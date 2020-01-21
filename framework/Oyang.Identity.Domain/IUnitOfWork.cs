using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
