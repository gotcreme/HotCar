using System;
using System.Collections.Generic;
using HotCar.Entities;
using HotCar.Entities.Enums;

namespace HotCar.BLL.Abstract
{
    public interface IUsersManager
    {
        List<User> GetAll();
        List<User> GetUsersByRole(string role);
        UserPhoto GetUserPhoto(string login);
        String UserAuthentication(string login, string password);
        IDictionary<int, string> GetAllRoles();
        bool UpdateUserInfo(User user);
        List<User> SearchUsers(string searchRequest);
        void UsersLock(IDictionary<int, string> users, UserRoles role);
        void UsersUnlock(IDictionary<int, string> users);
        User GetUserByLogin(String login);
    }
}
