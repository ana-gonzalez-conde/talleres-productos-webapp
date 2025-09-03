using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Model.Daos.UserDao
{
    public interface IUserDao : IGenericDao<User, Int64>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        User FindByLoginName(String loginName);
        bool ExistsByLoginName(String loginName);
    }
}
