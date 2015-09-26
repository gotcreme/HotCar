using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotCar.Entities.Enums;
using HotCar.Entities;

namespace HotCar.BLL.Abstract
{
    public interface ISecurityManager
    {
        /// <summary>
        /// Verifying the identity of a user.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>True or False</returns>
        bool Authentication(string login, string password);     
        bool IsUserInRole(UserRoles role);
        bool IsAuthenticated();
        string ReadUserRole(string login, out bool status);    
        void SignOut();
        bool? CreateAccount(User user);


    }
}
