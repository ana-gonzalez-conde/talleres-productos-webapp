using System;

namespace Model.Services.Exceptions
{
    /// <summary>
    /// Exception throwed when a user doesn't existe or is not authenticated
    /// </summary>
    public class UserNotAuthenticatedException : Exception
    {
        public UserNotAuthenticatedException()
        {
        }

        public UserNotAuthenticatedException(string message)
            : base(message)
        {
        }

        public UserNotAuthenticatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
