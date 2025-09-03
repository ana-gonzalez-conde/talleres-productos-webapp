using System;

namespace Model.Services.UserService
{
    /// <summary>
    /// A Custom VO which keeps the results for a login action.
    /// </summary>
    [Serializable()]
    public class LoginResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        /// <param name="userId">The user profile id.</param>
        /// <param name="name">Users's first name.</param>
        /// <param name="password">The encrypted password.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <param name="isAdmin">Has admin privilegies?.</param>
        public LoginResult(long userId, String name, String login,
            String password, String language, String country, Boolean isAdmin)
        {
            this.UserId = userId;
            this.Name = name;
            this.Login = login;
            this.Password = password;
            this.Language = language;
            this.Country = country;
            this.IsAdmin = isAdmin;
        }

        #region Properties Region

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the encrypted password.
        /// </summary>
        /// <value>The <c>encryptedPassword.</c></value>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>The <c>firstName</c></value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the login.
        /// </summary>
        /// <value>The login value</value>
        public string Login { get; private set; }


        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string Language { get; private set; }

        /// <summary>
        /// Gets the user profile id.
        /// </summary>
        /// <value>The user profile id.</value>
        public long UserId { get; private set; }

        /// <summary>
        /// Gets the information about admin privilegies
        /// </summary>
        /// <value> The admin privilegies info</value>
        public Boolean IsAdmin { get; private set; }

        #endregion Properties Region

        public override bool Equals(object obj)
        {
            LoginResult target = (LoginResult)obj;

            return (this.UserId == target.UserId)
                   && (this.Name == target.Name)
                   && (this.Login == target.Login)
                   && (this.Password == target.Password)
                   && (this.Language == target.Language)
                   && (this.Country == target.Country)
                   && (this.IsAdmin == target.IsAdmin);
        }

        // The GetHashCode method is used in hashing algorithms and data
        // structures such as a hash table. In order to ensure that it works
        // properly, it is based on a field that does not change.
        public override int GetHashCode()
        {
            return this.UserId.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strLoginResult;

            strLoginResult =
                "[ userProfileId = " + UserId + " | " +
                "name = " + Name + " | " +
                "login = " + Login + " | " +
                "password = " + Password + " | " +
                "language = " + Language + " | " +
                "country = " + Country +
                "isAdmin = " + IsAdmin + " | " + " ]";

            return strLoginResult;
        }
    }
}
