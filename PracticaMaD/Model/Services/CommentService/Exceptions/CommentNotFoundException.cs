using System;

namespace Model.Services.Exceptions
{
    /// <summary>
    /// Exception throwed when Comment is not found
    /// </summary>
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException()
        {
        }

        public CommentNotFoundException(string message)
            : base(message)
        {
        }

        public CommentNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
