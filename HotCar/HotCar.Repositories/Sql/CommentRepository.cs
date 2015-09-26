using System;
using System.Collections.Generic;
using System.Data;
using HotCar.Repositories.Abstract;
using System.Data.SqlClient;
using HotCar.Entities;


namespace HotCar.Repositories.Sql
{
    public class CommentRepository: Repository, IRepository<Comment>, ICommentRepository
    {
        #region Constructor

        public CommentRepository(string connectionString)
            : base(connectionString)
        {
        }

        #endregion Constructor

        #region ICommentRepository

        public Comment SelectInformation(SqlDataReader reader)
        {
            Comment comment = new Comment();

            comment.Id = (int) reader["Id"];
            comment.Time = (DateTime) reader["CommentTime"];
            comment.Text = (string) reader["CommentText"];
            comment.SenderLogin = (string) reader["SenderLogin"];
            comment.TripId = (int) reader["TripId"];

            return comment;
        }

        #endregion

        #region ICommentRepository
        public List<Comment> GetCommentsDriversAboutPassengerByPassengerId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_COMMENTS_DRIVERS_ABOUT_PASSENGER_BY_PASSENGER_ID,
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    List<Comment> users = new List<Comment>();
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

        public List<Comment> GetCommentsPassengersAboutDriverByDriverId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_COMMENTS_PASENGERS_ABOUT_DRIVER_BY_DRIVER_ID,
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    List<Comment> users = new List<Comment>();
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

        #endregion 
    }
}
