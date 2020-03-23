using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public class ApplicationException : Exception
    {
        public ApplicationException()
        {

        }

        public ApplicationException(string message) : base(message)
        {

        }
        public ApplicationException(string message, string member) : base(message)
        {
            Member = member;
        }

        public string Member { get; }
    }
}
