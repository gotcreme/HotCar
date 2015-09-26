using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;
using HotCar.Entities;
using HotCar.Repositories.Abstract;

namespace HotCar.Repositories.Sql
{
    public class PassengerRepository : Repository, IRepository<Passenger>, IPassengerRepository
    {
        #region Constructor

        public PassengerRepository(string connectionString)
            : base(connectionString)
        { }

        #endregion

        #region IRepository

        public Passenger SelectInformation(System.Data.SqlClient.SqlDataReader reader)
        {
            Passenger passenger = new Passenger();

            passenger.Id = (int) reader["Id"];
            passenger.PassengerLogin = (string) reader["PassengerLogin"];
            passenger.CountReservedSeats = (int) reader["CountReservedSeats"];
            passenger.TripId = (int) reader["TripId"];
            passenger.Cost = (int) reader["Cost"];
            passenger.PassengerRoute = (string) reader["PassengerRoute"];
            passenger.CommentId = (int) reader["CommentId"];

            return passenger;
        }

        #endregion

        #region IPassengerRepository

        public List<Passenger> GetAllPassengersByTripId(int tripId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_ALL_PASSENGERS_BY_TRIP_ID, 
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@tripId", SqlDbType.Int).Value = tripId;

                    List<Passenger> passengers = new List<Passenger>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            passengers.Add(SelectInformation(reader));
                        }

                        return passengers;
                    }
                }
            }
        }

        public bool SignPassengerToTrip(Passenger passenger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.SIGN_PASSENGER_TO_TRIP, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@passengerLogin", passenger.PassengerLogin);
                    command.Parameters.AddWithValue("@countReserverSeats", passenger.CountReservedSeats);
                    command.Parameters.AddWithValue("@tripId", passenger.TripId);
                    command.Parameters.AddWithValue("@cost", passenger.Cost);
                    command.Parameters.AddWithValue("@passengerRoute", passenger.PassengerRoute);

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool UnsignPassengerFromTrip(Passenger passenger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.UNSIGN_PASSENGER_FROM_TRIP, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@id", passenger.Id);
                    command.Parameters.AddWithValue("@tripId", passenger.TripId);

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool UpdatePassengerFromTrip(Passenger passenger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.UPDATE_PASSENGER_FROM_TRIP, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@id", passenger.Id);
                    command.Parameters.AddWithValue("@passengerLogin", passenger.PassengerLogin);
                    command.Parameters.AddWithValue("@countReserverSeats", passenger.CountReservedSeats);
                    command.Parameters.AddWithValue("@tripId", passenger.TripId);
                    command.Parameters.AddWithValue("@cost", passenger.Cost);
                    command.Parameters.AddWithValue("@passengerRoute", passenger.PassengerRoute);

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        #endregion
    }
}
