using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotCar.Entities;
using HotCar.Repositories.Abstract;

namespace HotCar.Repositories.Sql
{
    public class TripRepository : Repository, IRepository<Trip>, ITripRepository
    {
        #region Constructor

        public TripRepository(string connectionString)
            : base(connectionString)
        {
        }

        #endregion

        #region IRepository

        public Trip SelectInformation(SqlDataReader reader) // заміни свій метод на цей або якшо ти не міняв нічого в бд чи тут , то просто заміни файл + знизу ше метод доданий є
        {
            Trip trip = new Trip();

            trip.Id = (int)reader["Id"];
            trip.TripTime = (DateTime)reader["TripTime"];
            trip.CostOneSeat = (decimal) reader["CostOneSeat"];
            //trip.CarId = (int)reader["CarId"];
            trip.AvailablePlacesCount = (int)reader["AvailablePlacesCount"];
            trip.AdditionalInfo = (String) reader["AdditionalInfo"];

            #region Need change !!!!!!

            try
            {
                trip.Driver.Login = (String) reader["UserLogin"];
            }
            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.AboutMe = (String) reader["AboutMe"];
            }

            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.FirstName = (String) reader["FirstName"];
            }

            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.SurName = (String) reader["SurName"];
            }

            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.Birthday = reader["Birthday"] == DBNull.Value ? null : (DateTime?)reader["Birthday"];
            }

            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.Phone = (String) reader["PhoneNumber"];
            }

            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.Mail = (String) reader["Mail"];
            }

            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.Photo.Photo = (byte[])(reader["Photo"] != DBNull.Value ? reader["Photo"] : null);
            }

            catch (Exception e)
            {

            }

            try
            {
                trip.Driver.Photo.FileExtension = (string)(reader["PhotoFileExtension"] != DBNull.Value ? reader["PhotoFileExtension"] : null);
            }

            catch (Exception e)
            {

            }

            #endregion

            return trip;
        }

        public void SelectLocations(SqlDataReader reader, Trip trip)
        {
            trip.RouteLocations.Add(new LocationInfo((double)(reader["Latitude"]), (double)(reader["Longitude"])));
        }

        #endregion

        #region ITripRepository

        public List<Trip> GetAllPassengerAvailableTrips(DateTime dateTime)
        {
            List<Trip> trips = this.GetAllTripsWithoutLocations(dateTime);

            this.AddLocations(trips);

            return trips;
        }

        #region Available Helpers

        private List<Trip> GetAllTripsWithoutLocations(DateTime dateTime)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_ALL_AVAILABLE_TRIPS, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@Time", SqlDbType.SmallDateTime).Value = dateTime;

                    List<Trip> trips = new List<Trip>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trips.Add(this.SelectInformation(reader));
                        }

                        return trips;
                    }
                }
            }
        }

        private void AddLocations(List<Trip> trips)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_LOCATIONS, connection) { CommandType = CommandType.StoredProcedure })
                {
                    for (int a = 0; a < trips.Count; a++)
                    {
                        command.Parameters.Clear();
                        command.Parameters.Add("@id", SqlDbType.Int).Value = trips[a].Id;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.SelectLocations(reader, trips[a]);
                            }
                        }
                    }
                }
            }
        }
        
        #endregion


        public List<Trip> GetAllTripsByDriverId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_ALL_TRIPS_BY_DRIVER_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@driverId", SqlDbType.Int).Value = id;

                    List<Trip> trips = new List<Trip>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trips.Add(SelectInformation(reader));
                        }

                        this.AddLocations(trips); 

                        return trips;
                    }
                }
            }
        }

        public int GetCountConductedTripsByDriverId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_COUNT_CONDUCTED_TRIPS_BY_DRIVER_ID,
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@driverId", SqlDbType.Int).Value = id;

                    return (int) command.ExecuteScalar();
                }
            }
        }

        public List<Trip> GetAllTripsByPassengerId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_ALL_TRIPS_BY_PASSENGER_ID,
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@passengerId", SqlDbType.Int).Value = id;

                    List<Trip> trips = new List<Trip>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trips.Add(SelectInformation(reader));
                        }

                        this.AddLocations(trips); 

                        return trips;
                    }
                }
            }
        }

        public int GetCountConductedTripsByPassengerId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_COUNT_CONDUCTED_TRIPS_BY_PASSENGER_ID,
                    connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@passengerId", SqlDbType.Int).Value = id;

                    return (int) command.ExecuteScalar();
                }
            }
        }

        public bool DeleteTripById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.DELETE_TRIP_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool UpdateTripById(Trip trip)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.UPDATE_TRIP_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@id", trip.Id);
                    command.Parameters.AddWithValue("@ownerId", trip.Driver.Id);                 
                    command.Parameters.AddWithValue("@availablePlacesCount", trip.AvailablePlacesCount);
                    command.Parameters.AddWithValue("@tripTime", trip.TripTime);
                    command.Parameters.AddWithValue("@costOneSeat", trip.CostOneSeat);
                    command.Parameters.AddWithValue("@carId", trip.CarId);

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public int InsertTrip(Trip tripInfo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.INSERT_TRIP, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@ownerLogin", tripInfo.Driver.Login);
                    command.Parameters.AddWithValue("@placeCount", tripInfo.AvailablePlacesCount);                 
                    command.Parameters.AddWithValue("@tripTime", tripInfo.TripTime);
                    command.Parameters.AddWithValue("@costOneSeat", tripInfo.CostOneSeat);
                    command.Parameters.AddWithValue("@carId", tripInfo.CarId);
                    command.Parameters.AddWithValue("@additionalInfo", tripInfo.AdditionalInfo);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            
        }

        public bool AddRoute(LocationInfo location, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.ADD_ROUTE, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@tripId", id);
                    command.Parameters.AddWithValue("@longitude ", location.Longitude);
                    command.Parameters.AddWithValue("@latitude", location.Latitude);
                  

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }

        }

        public List<Trip> GetTripById(int tripId)     //added
        {
             using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_TRIP_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = tripId;

                    List<Trip> trips = new List<Trip>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trips.Add(this.SelectInformation(reader));
                        }

                        this.AddLocations(trips);

                        return trips;
                    }
                }
            }
        }

        #endregion
    }
}
