using System;
using System.Collections.Generic;
using HotCar.Entities;
using HotCar.Entities.Enums;

namespace HotCar.Repositories.Abstract
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        List<User> GetUsersByRole(UserRoles role);
        User GetUserById(int id);
        User GetUserByLogin(String login);
        User GetAccountInfo(String login);
        User GetDriverByTripId(int id);
        List<User> GetAllPassengerByTripId(int id);
        List<User> GetUsersByFirstName(String firstName);
        List<User> GetUsersBySurName(String surName);
        List<User> GetUsersBySurFirstName(String surName, String firstName);
        UserRoles GetUserRoleByLogin(String login);
        UserRoles GetUserRoleById(int userId);
        int GetUsersNumber();
        String GetUserPasswordByLogin(String login);
        string GetUserLoginById(int id);
        UserPhoto GetUserPhoto(int userId);
        bool InsertUser(User user);
        bool UpdateUserByLogin(User user);
        bool UpdateUserById(User user);
        bool DeleteUserById(int id);
        bool DeleteUserByLogin(String login);
        bool LockUserById(int id);
        bool UnlockUserById(int id);
        bool IsMailValid(string mail);
        bool IsLoginValid(string login);
        bool IsPhoneValid(string phone);
    }
}
