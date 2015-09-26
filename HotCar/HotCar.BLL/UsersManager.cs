using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HotCar.BLL.Abstract;
using HotCar.Entities;
using HotCar.Entities.Enums;
using HotCar.Repositories.Abstract;
using HotCar.Repositories.Sql;

namespace HotCar.BLL
{
    public class UsersManager:IUsersManager
    {
        #region Private Fields

        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructor

        public UsersManager(IUserRepository userRepository)
        {         
            this._userRepository = userRepository;
        }

        #endregion

        #region Interface implementation

        public List<User> GetAll()
        {
            return this._userRepository.GetAllUsers();
        }

        public List<User> GetUsersByRole(string roles)
        {
            if (roles == "all")
            {
                return this._userRepository.GetAllUsers();
            }

            var role = (UserRoles) Enum.Parse(typeof (UserRoles), roles);
            return this._userRepository.GetUsersByRole(role);
        }

        public IDictionary<int, string> GetAllRoles()
        {
            var enumtype = typeof (UserRoles);
            var dictionary = new Dictionary<int,string>();
            
            foreach (int value in Enum.GetValues(enumtype))
            {
                var name = Enum.GetName(enumtype, value);
                dictionary.Add(value, name);
            }
            return dictionary;
        }

        public UserPhoto GetUserPhoto(string login)
        {
            User user = this._userRepository.GetUserByLogin(login);
            int userId = user.Id;
            UserPhoto photo = this._userRepository.GetUserPhoto(userId);
            return photo;
        }

        public bool UpdateUserInfo(User user)
        {
            if (!this.IsNameValid(user.FirstName, true) ||
                !this.IsNameValid(user.SurName, false) ||
                !this.IsPhoneValid(user.Phone) ||
                !this.IsMailValid(user.Mail))
            {
                return false;
            }
          
            return this._userRepository.UpdateUserById(user);
        }

        public void Update(User user)
        {
            //Update user info           
        }
      
        public List<User> SearchUsers(string searchRequest)
        {
            List<User> users = new List<User>();

            if (String.IsNullOrEmpty(searchRequest))
            {
                return users;
            }

            String[] words = searchRequest.Split(new char[] { '.', '?', '!', '-', '–', ',', ';', ':', ' '});
            List<String> filteredWords = new List<String>();
            for (int a = 0; a < words.Length; a++)
            {
                if (!String.IsNullOrEmpty(words[a]))
                {
                    filteredWords.Add(words[a]);
                }
            }

            if (filteredWords.Count == 0)
            {
                return users;
            }

            if (filteredWords.Count < 2)
            {
                User user = this._userRepository.GetUserByLogin(filteredWords[0]);
                if (user != null)
                {
                    users.Add(user);
                }

                List<User> surNames = this._userRepository.GetUsersBySurName(filteredWords[0]);
                if (surNames.Count > 0)
                {
                    for (int a = 0; a < surNames.Count; a++)
                    {
                        users.Add(surNames[a]);
                    }
                }

                List<User> firstNames = this._userRepository.GetUsersByFirstName(filteredWords[0]);
                if (firstNames.Count > 0)
                {
                    for (int a = 0; a < firstNames.Count; a++)
                    {
                        users.Add(firstNames[a]);
                    }
                }
            }

            else
            {
                List<User> usersS = this._userRepository.GetUsersBySurFirstName(filteredWords[0], filteredWords[1]);
                if (usersS.Count > 0)
                {
                    for (int a = 0; a < usersS.Count; a++)
                    {
                        users.Add(usersS[a]);
                    }
                }

                List<User> usersF = this._userRepository.GetUsersBySurFirstName(filteredWords[1], filteredWords[0]);
                if (usersF.Count > 0)
                {
                    for (int a = 0; a < usersF.Count; a++)
                    {
                        users.Add(usersF[a]);
                    }
                }
            }

            return users;
        }

        public String UserAuthentication(string login, string password)
        {
            String userRole = null;
            //var user = this._userRepository.GetUserByLogin(login);
            //if (user != null && user.Login == login && user.Password == this.EncryptPassword(password))
            //{
            //    userRole = user.Role.ToString();
            //}

            return userRole;
        }

        public void UsersLock(IDictionary<int, string> users, UserRoles role)
        {
            if (role == UserRoles.Master)
            {
                foreach (var user in users)
                {                 
                    this._userRepository.LockUserById(user.Key);
                }
            }
            else
            {
                foreach (var user in users.Where(user => user.Value == UserRoles.User.ToString()))
                {
                    this._userRepository.LockUserById(user.Key);
                }
            }
           
        }

        public void UsersUnlock(IDictionary<int, string> users)
        {           
            foreach (var user in users)
            {
                if (user.Value == UserRoles.Inactive.ToString())
                {
                   this._userRepository.UnlockUserById(user.Key);
                }                   
            }
                        
        }

        public User GetUserByLogin(String login)
        {
            return this._userRepository.GetUserByLogin(login);
        }

        #endregion

        #region Helpers

        #region Searching

        private User SearchUserByLogin(String login)
        {
            return this._userRepository.GetUserByLogin(login);
        }
     
        private List<User> SearchUsersByFirstName(String firstName)
        {
            return this._userRepository.GetUsersByFirstName(firstName);
        }
      
        private List<User> SearchUsersBySurName(String surName)
        {
            return this._userRepository.GetUsersBySurName(surName);
        }
       
        private List<User> SearchUsersBySurFirstName(String surName, String firstName)
        {
            return this._userRepository.GetUsersBySurFirstName(surName, firstName);
        }

        #endregion

        #region Security

        private string EncryptPassword(string password)
        {
            byte[] bytesArr = Encoding.Default.GetBytes(password);
            SHA256 mySHA256 = SHA256.Create();
            byte[] hashValue = mySHA256.ComputeHash(bytesArr);
            
            return Encoding.Default.GetString(hashValue);
        }

        #endregion

        #region User update validators

        private bool IsNameValid(String name, bool first)
        {
            int maxLength = 20;
            if (first)
            {
                maxLength = 10;
            }

            if (name.Length > maxLength)
            {
                return false;
            }

            else
            {
                for (int a = 0; a < name.Length; a++)
                {
                    if (!Char.IsLetter(name[a]))
                    {
                        if (name[a] != '-')
                        {
                            return false;
                        }

                        else if ((a == 0) || (a == (name.Length - 1)))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        private bool IsPhoneValid(String phone)
        {
            if (phone.Length != 13)
            {
                return false;
            }

            if (phone[0] != '+')
            {
                return false;
            }

            for (int a = 1; a < phone.Length; a++)
            {
                if (!Char.IsDigit(phone[a]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsMailValid(String mail)
        {
            String[] atSplitted = mail.Split('@');

            if (atSplitted.Length != 2)
            {
                return false;
            }

            else
            {
                String[] afterAt = atSplitted[1].Split('.');

                if (afterAt.Length != 2)
                {
                    return false;
                }

                else
                {
                    if (afterAt[0].Length < 1 || afterAt[1].Length < 1)
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
