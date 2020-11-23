using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Domain
{
    public interface IValidatableObject
    {
        void Validate();
    }
}
