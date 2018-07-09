using System;

namespace MvcAddUserExample.Core.Exceptions
{
    public class DuplicateEmailException: Exception
    {
        public DuplicateEmailException(string email)
            : base($"Email address {email} is already associated with a user account.")
        {
        }
    }
}
