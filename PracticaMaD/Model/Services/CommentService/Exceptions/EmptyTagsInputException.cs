using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.CommentService.Exceptions
{
    public class EmptyTagsInputException : Exception
    {
        /// <summary>
        /// Exception used on operations which need a List of tags as input
        /// which could not be empty
        /// </summary>
        public EmptyTagsInputException()
        {
        }

        public EmptyTagsInputException(string message)
            : base(message)
        {
        }

        public EmptyTagsInputException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
