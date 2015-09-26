using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HotCar.Entities;
using HotCar.Entities.Enums;
using HotCar.Repositories.Abstract;

namespace HotCar.Repositories.Sql
{
    public class UserRepository : Repository, IRepository<User>, IUserRepository
    {
        #region Constructor

        public UserRepository(string connectionString)
            : base (connectionString)
        { }

        #endregion

        #region IUserRepositoty

        public List<User> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_ALL_USER, connection) { CommandType = CommandType.StoredProcedure })
                {
                    List<User> users = new List<User>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            users.Add(SelectInformation(reader));
                        }

                        return users;
                    }
                }
            }
        }

        public List<User> GetUsersByRole(UserRoles role)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USERS_BY_ROLE, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@roleName", SqlDbType.VarChar).Value = role.ToString();

                    List<User> users = new List<User>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(SelectInformation(reader));
                        }

                        return users;
                    }
                }
            }
        }

        public User GetUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USER_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@idUser", SqlDbType.Int).Value = id;

                    User user = null;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = SelectInformation(reader);
                        }

                        return user;
                    }
                }
            }
        }

        public User GetUserByLogin(String login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USER_BY_LOGIN, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;

                    User user = null;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = SelectInformation(reader);
                        }

                        return user;
                    }
                }
            }
        }

        public User GetAccountInfo(String login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_ACCOUNT, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;

                    User user = null;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = SelectAccountInformation(reader);
                        }

                        return user;
                    }
                }
            }
        }
        
        public List<User> GetUsersByFirstName(String firstName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USERS_BY_FIRST_NAME, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@firstName", SqlDbType.VarChar).Value = firstName;

                    List<User> users = new List<User>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(SelectInformation(reader));
                        }

                        return users;
                    }
                }
            }
        }

        public List<User> GetUsersBySurName(String surName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USERS_BY_SUR_NAME, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@surName", SqlDbType.VarChar).Value = surName;

                    List<User> users = new List<User>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(SelectInformation(reader));
                        }

                        return users;
                    }
                }
            }
        }

        public List<User> GetUsersBySurFirstName(String surName, String firstName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USERS_BY_SUR_FIRST_NAME, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@surName", SqlDbType.VarChar).Value = surName;
                    command.Parameters.Add("@firstName", SqlDbType.VarChar).Value = firstName;

                    List<User> users = new List<User>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(SelectInformation(reader));
                        }

                        return users;
                    }
                }
            }
        }
        
        public UserRoles GetUserRoleByLogin(String login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USER_ROLE_BY_LOGIN, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;

                    return (UserRoles)Enum.Parse(typeof(UserRoles), (String)(command.ExecuteScalar()));
                }
            }
        }

        public UserRoles GetUserRoleById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USER_ROLE_NAME_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@idUser",SqlDbType.Int).Value = userId;
                    return (UserRoles)Enum.Parse(typeof(UserRoles),(String)(command.ExecuteScalar()));
                }
            }
        }

        public int GetUsersNumber()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_NUMBER_OF_USERS, connection) { CommandType = CommandType.StoredProcedure })
                {
                    int count = 0;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);
                        }

                        return count;
                    }
                }
            }
        }

        public String GetUserPasswordByLogin(String login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USER_PASS_BY_LOGIN, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@userLogin", SqlDbType.VarChar).Value = login;

                    String password = String.Empty;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            password = (String)(reader["UserPassword"]);
                        }

                        return password;
                    }
                }
            }
        }

        public string GetUserLoginById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USER_LOGIN_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    return (string) command.ExecuteScalar();
                }
            }
        }

        public User GetDriverByTripId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_DRIVER_BY_TRIP_ID,
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@TripId", SqlDbType.Int).Value = id;

                    User user = null;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = SelectInformation(reader);
                        }

                        return user;
                    }
                }
            }
        }

        public List<User> GetAllPassengerByTripId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_DRIVER_BY_TRIP_ID,
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@TripId", SqlDbType.Int).Value = id;

                    List<User> users = new List<User>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(SelectInformation(reader));
                        }

                        return users;
                    }
                }
            }
        }

        public UserPhoto GetUserPhoto(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_USER_PHOTO, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@userId", SqlDbType.VarChar).Value = userId;

                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserPhoto userPhoto = null;
                        if (reader.Read())
                        {
                            userPhoto = SelectUserPhotoInformation(reader);
                        }

                        return userPhoto;
                    }
                }
            }
        }

        public bool InsertUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.CREATE_NEW_USER, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@firstName", SqlDbType.VarChar).Value = user.FirstName;
			        command.Parameters.Add("@surName", SqlDbType.VarChar).Value = user.SurName;
			        command.Parameters.Add("@roleName", SqlDbType.VarChar).Value = user.Role;
			        command.Parameters.Add("@userLogin", SqlDbType.VarChar).Value = user.Login;
			        command.Parameters.Add("@userPassword", SqlDbType.VarBinary).Value = Encoding.Default.GetBytes(user.Password);
			        command.Parameters.Add("@mail", SqlDbType.VarChar).Value = user.Mail;		        
                    command.Parameters.Add("@inactive", SqlDbType.Bit).Value = user.Inactive;			        

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool UpdateUserByLogin(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.UPDATE_USER_BY_LOGIN, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@idUser",SqlDbType.Int).Value = user.Id;
                    command.Parameters.Add("@firstName", SqlDbType.VarChar).Value = user.FirstName;
                    command.Parameters.Add("@surName", SqlDbType.VarChar).Value = user.SurName;
                    command.Parameters.Add("@roleName", SqlDbType.VarChar).Value = user.Role.ToString();
                    command.Parameters.Add("@userLogin", SqlDbType.VarChar).Value = user.Login;
                    command.Parameters.Add("@mail", SqlDbType.VarChar).Value = user.Mail;
                    command.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = user.Phone;
                    command.Parameters.Add("@birthday", SqlDbType.Date).Value = user.Birthday;
                    command.Parameters.Add("@inactive", SqlDbType.Bit).Value = user.Inactive;
                    command.Parameters.Add("@aboutMe", SqlDbType.VarChar).Value = user.AboutMe;

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool UpdateUserById(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.UPDATE_USER_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {                                                                                                         
                    command.Parameters.AddWithValue("@idUser", user.Id);                  
                    command.Parameters.AddWithValue("@firstName", user.FirstName);
                    command.Parameters.AddWithValue("@surName", user.SurName);                 
                    command.Parameters.AddWithValue("@roleName", user.Role.ToString());
                    command.Parameters.AddWithValue("@phoneNumber", user.Phone);
                    command.Parameters.AddWithValue("@mail", user.Mail);
                    command.Parameters.AddWithValue("@birthday", user.Birthday);
                    command.Parameters.Add("@inactive", SqlDbType.Bit).Value = user.Inactive;
                    command.Parameters.AddWithValue("@aboutMe", user.AboutMe);

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool DeleteUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.DELETE_USER_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@idUser", SqlDbType.Int).Value = id;
                    
                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool DeleteUserByLogin(String login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.DELETE_USER_BY_LOGIN, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool LockUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.LOCK_USER_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@idUser", SqlDbType.Int).Value = id;
                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool UnlockUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.UNLOCK_USER_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@idUser", SqlDbType.Int).Value = id;
                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool IsMailValid(string mail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.IS_MAIL_VALID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@mail", mail);

                    return Convert.ToBoolean(command.ExecuteScalar());
                }
            }
        }

        public bool IsLoginValid(string login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.IS_LOGIN_VALID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@login", login);

                    return Convert.ToBoolean(command.ExecuteScalar());
                }
            }
        }

        public bool IsPhoneValid(string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.IS_PHONE_VALID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@phone", phone);

                    return Convert.ToBoolean(command.ExecuteScalar());
                }
            }
        }

        #endregion

        #region IRepository<User>

        public User SelectInformation(SqlDataReader reader)
        {
            User user = new User();
            
            user.Id = (int)reader["Id"];
            user.FirstName = (string)reader["FirstName"];
            user.SurName = (string)reader["SurName"];
            user.Role = (UserRoles)(Enum.Parse(typeof(UserRoles),(String)reader["RoleName"]));
            user.Login = (string)reader["UserLogin"];
            user.Password = Encoding.Default.GetString((byte[])reader["UserPassword"]);          
            user.Mail = (string)reader["Mail"];

            try             // Change !!!!
            {
                user.Birthday = (DateTime?) reader["Birthday"];
                user.AboutMe = (string) reader["AboutMe"];
                user.Phone = (string) reader["PhoneNumber"];
            }

            catch (Exception e)
            {
            }

            user.Inactive = (bool) reader["Inactive"];

            return user;
        }


        public User SelectAccountInformation(SqlDataReader reader)
        {
            User user = new User();

            user.Id = (int)reader["Id"];            
            user.Role = (UserRoles)(Enum.Parse(typeof(UserRoles), (String)reader["RoleName"]));
            user.Login = (string)reader["UserLogin"];
            user.Password = Encoding.Default.GetString((byte[])reader["UserPassword"]);                  
            user.Inactive = (bool)reader["Inactive"];

            return user;
        }

        private UserPhoto SelectUserPhotoInformation(SqlDataReader reader)
        {
            UserPhoto userPhoto = new UserPhoto();

            userPhoto.Id = (int)reader["Id"];
            userPhoto.Photo = (byte[])(reader["Photo"] != DBNull.Value ? reader["Photo"] : null);
            userPhoto.FileExtension = (string)(reader["PhotoFileExtension"] != DBNull.Value ? reader["PhotoFileExtension"] : null);

            return userPhoto;
        }

        #endregion
    }

}
