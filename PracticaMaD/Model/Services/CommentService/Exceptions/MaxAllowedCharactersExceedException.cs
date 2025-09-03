using System;

namespace Model.Services.CommentService.Exceptions
{
    public class MaxAllowedCharactersExceedException : Exception
    {
        /// <summary>
        /// Exception throwed when some text exceed the maximum allowed
        /// </summary>
        public MaxAllowedCharactersExceedException()
        {

        }
        public MaxAllowedCharactersExceedException(string message)
            : base(message)
        {
        }

        public MaxAllowedCharactersExceedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
