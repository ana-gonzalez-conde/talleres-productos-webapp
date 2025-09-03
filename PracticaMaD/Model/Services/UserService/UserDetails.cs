using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.UserService
{
    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable()]
    public class UserDetails
    {
        #region Properties Region

        public String Login { get; private set; }

        public String FirstName { get; private set; }

        public String Surnames { get; private set; }

        public String Email { get; private set; }
        public String Address { get; private set; }

        public string Language { get; private set; }

        public string Country { get; private set; }

        public bool IsAdmin { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetails"/>
        /// class.
        /// </summary>
        /// <param name="login">The user's login.</param>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="address">The user's address.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>

        public UserDetails(String firstName, String surnames,
            String email, String address, String language, String country)
        {
            this.FirstName = firstName;
            this.Surnames = surnames;
            this.Email = email;
            this.Address = address;
            this.Language = language;
            this.Country = country;
        }
        public UserDetails(String login, String firstName, String surnames,
            String email, String address, String language, String country)
        {
            this.Login = login;
            this.FirstName = firstName;
            this.Surnames = surnames;
            this.Email = email;
            this.Address = address;
            this.Language = language;
            this.Country = country;
        }
        
        public UserDetails(String login, String firstName, String surnames,
    String email, String address, String language, String country, bool isAdmin)
        {
            this.Login = login;
            this.FirstName = firstName;
            this.Surnames = surnames;
            this.Email = email;
            this.Address = address;
            this.Language = language;
            this.Country = country;
            this.IsAdmin = isAdmin;
        }

        public override bool Equals(object obj)
        {

            UserDetails target = (UserDetails)obj;

            return (this.FirstName == target.FirstName)
                   && (this.Surnames == target.Surnames)
                   && (this.Email == target.Email)
                   && (this.Address == target.Address)
                   && (this.Language == target.Language)
                   && (this.Country == target.Country);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode();
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
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ login = " + Login + " | " +
                "firstName = " + FirstName + " | " +
                "lastName = " + Surnames + " | " +
                "email = " + Email + " | " +
                "address =" + Address + " | " +
                "language = " + Language + " | " +
                "country = " + Country + " ]";


            return strUserProfileDetails;
        }
    }
}
