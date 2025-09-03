using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.UserService.Exceptions
{

    /// <summary>
    /// Exception throwed when the exception is not correct on login operation
    /// </summary>
    [Serializable]
    public class IncorrectPasswordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncorrectPasswordException"/> class.
        /// </summary>
        /// <param name="loginName"><c>loginName</c> that causes the error.</param>
        public IncorrectPasswordException(String loginName)
            : base("Incorrect password exception => loginName = " + loginName)
        {
            this.LoginName = loginName;
        }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public String LoginName { get; private set; }
    }
}
