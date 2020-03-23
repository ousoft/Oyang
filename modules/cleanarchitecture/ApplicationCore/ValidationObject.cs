using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public class ValidationObject
    {
        private static ValidationObject _instance = new ValidationObject();

        public static ValidationObject Validate(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new ApplicationException(errorMessage);
            }
            return _instance;
        }
        public static ValidationObject Validate(bool condition, string errorMessage, string member)
        {
            if (condition)
            {
                throw new ApplicationException(errorMessage, member);
            }
            return _instance;
        }

        public ValidationObject ThenValidate(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new ApplicationException(errorMessage);
            }
            return _instance;
        }
        public ValidationObject ThenValidate(bool condition, string errorMessage, string member)
        {
            if (condition)
            {
                throw new ApplicationException(errorMessage, member);
            }
            return _instance;
        }
    }
}
