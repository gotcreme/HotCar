using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using HotCar.BLL.Abstract;
using HotCar.Repositories.Abstract;
using System.Web.Security;
using HotCar.Entities;
using HotCar.Entities.Enums;


namespace HotCar.BLL
{
    public class SecurityManager : ISecurityManager
    {
        #region Private Fields

        private readonly IUserRepository _userRepository;       
     
        public string ValidMess { get; set; }

        #endregion

        #region Constructors

        public SecurityManager(IUserRepository userRepository)
        {      
            this._userRepository = userRepository;
        }

        #endregion

        #region Interface Implementation

        public bool Authentication(string login, string password)
        {
            var user = this._userRepository.GetAccountInfo(login);
            if (user != null && user.Inactive == false && 
                CheckPassword(user.Password, password))
            {
                FormsAuthentication.SetAuthCookie(login, false);                   
                return true;
            }

            return false;            
        }

        public bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public bool IsUserInRole(UserRoles role)
        {
            return HttpContext.Current.User.IsInRole(role.ToString());
        }

        public string ReadUserRole(string login, out bool status)
        {
            var user = this._userRepository.GetAccountInfo(login);
            var roles = user.Role;
            status = user.Inactive;
            return roles.ToString();
        }
         
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool? CreateAccount(User user)
        {
            user.Inactive = false;
            user.Role = UserRoles.User;
            user.Password = this.EncryptPassword(user.Password);

            bool? validation = this.RegisterValidation(user);
            bool? result;
            switch (validation)
            {
                case true:
                    result =  this._userRepository.InsertUser(user);
                    break;
                case false:
                    result = false;
                    break;
                case null:
                    result = null;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
                    
        #endregion

        #region Helpers

        private string EncryptPassword(string password)
        {
            byte[] bytesArr = Encoding.Default.GetBytes(password);
            SHA256 mySHA256 = SHA256.Create();
            byte[] hashValue = mySHA256.ComputeHash(bytesArr);

            return Encoding.Default.GetString(hashValue);                             
        }

        private bool CheckPassword(string userPass, string password)
        {
            password = EncryptPassword(password);
            if (userPass == password)
            {
                return true;
            }
            return false;
        }

        private bool? RegisterValidation(User user)
        {
            if (!this._userRepository.IsLoginValid(user.Login))
            {
                if(!this._userRepository.IsMailValid(user.Mail))
                {
                    return true;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return false;
            }         
        }
       
        #endregion
    
    }
}
