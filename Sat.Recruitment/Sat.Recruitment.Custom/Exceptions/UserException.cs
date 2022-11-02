using System;

namespace Sat.Recruitment.Custom.Exceptions
{
    public class UserException: Exception
    {
        public UserException(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
