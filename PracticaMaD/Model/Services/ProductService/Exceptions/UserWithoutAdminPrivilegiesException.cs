using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.ProductService.Exceptions
{

    /// <summary>
    /// Exception throwed when a user has not the admin required 
    /// privilegies to execute an operation
    /// </summary>
    public class UserWithoutAdminPrivilegiesException : Exception
    {
        public UserWithoutAdminPrivilegiesException()
        {

        }
        public UserWithoutAdminPrivilegiesException(string message)
            : base(message)
        {
        }

        public UserWithoutAdminPrivilegiesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
