using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public class DomainException:Exception
    {
        public DomainException()
        {

        }

        public DomainException(string message) : base(message)
        {

        }
        public DomainException(string message, string member) : base(message)
        {
            Member = member;
        }

        public string Member { get; }
    }
}
