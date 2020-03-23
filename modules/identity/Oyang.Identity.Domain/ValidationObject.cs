using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public class ValidationObject
    {
        private static ValidationObject _instance = new ValidationObject();

        public static ValidationObject Validate(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new DomainException(errorMessage);
            }
            return _instance;
        }
        public static ValidationObject Validate(bool condition, string errorMessage, string member)
        {
            if (condition)
            {
                throw new DomainException(errorMessage, member);
            }
            return _instance;
        }

        public ValidationObject ThenValidate(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new DomainException(errorMessage);
            }
            return _instance;
        }
        public ValidationObject ThenValidate(bool condition, string errorMessage, string member)
        {
            if (condition)
            {
                throw new DomainException(errorMessage, member);
            }
            return _instance;
        }
    }
}
