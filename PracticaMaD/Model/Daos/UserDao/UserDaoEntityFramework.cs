using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.UserDao
{
    public class UserDaoEntityFramework :
         GenericDaoEntityFramework<User, Int64>, IUserDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserDaoEntityFramework()
        {
        }

        public User FindByLoginName(string loginName)
        {
            User user = null;

            DbSet<User> userProfiles = Context.Set<User>();

            var result =
                (from u in userProfiles
                 where u.login == loginName
                 select u);

            user = result.FirstOrDefault();

            if (user == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(User).FullName);

            return user;
        }

        public bool ExistsByLoginName(string loginName)
        {
            DbSet<User> users = Context.Set<User>();
            var query = from u in users
                        where u.login == loginName
                        select u;
            var user = query.FirstOrDefault();

            return user != null;
        }

        #endregion Public Constructors
    }
}
