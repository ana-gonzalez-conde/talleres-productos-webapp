using System;

namespace Model.Services.Exceptions
{
    /// <summary>
    /// Exception throwed when Tag is not found
    /// </summary>
    public class TagNotFoundException : Exception
    {
        public TagNotFoundException()
        {
        }

        public TagNotFoundException(string message)
            : base(message)
        {
        }

        public TagNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
