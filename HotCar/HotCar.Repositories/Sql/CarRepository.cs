using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;
using HotCar.Entities;
using HotCar.Repositories.Abstract;

namespace HotCar.Repositories.Sql
{
    public class CarRepository : Repository, IRepository<Car>, ICarRepository
    {
        #region Constructor

        public CarRepository(string connectionString)
            : base(connectionString)
        { }

        #endregion

        #region IRepository

        public Car SelectInformation(System.Data.SqlClient.SqlDataReader reader)
        {
            Car car = new Car();

            car.Id = (int) reader["Id"];
            car.Photo = reader["Photo"] as byte[] ?? null;
            car.MakeCar = (string) reader["MakeCar"];
            car.OwnerId = (int) reader["OwnerId"];

            return car;
        }

        #endregion

        #region ICarRepository

        public List<Car> GetAllCarsByUserId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_ALL_CARS_BY_USER_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@userId", SqlDbType.Int).Value = id;
                    List<Car> cars = new List<Car>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(SelectInformation(reader));
                        }

                        return cars;
                    }
                }
            }
        }

        public bool DeleteCarById(int carId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.DELETE_CAR_BY_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@carId", SqlDbType.Int).Value = carId;

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public bool UpdateCarById(Car car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.UPDATE_CAR_BY_ID) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@id", car.Id);
                    command.Parameters.AddWithValue("@photo", car.Photo);
                    command.Parameters.AddWithValue("@makeCar", car.MakeCar);
                    command.Parameters.AddWithValue("@ownerId", car.OwnerId);

                    return Convert.ToBoolean(command.ExecuteNonQuery());
                }
            }
        }

        public Car GetCarByTripId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.GET_CAR_BY_TRIP_ID, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@tripId", SqlDbType.Int).Value = id;

                    Car car = null;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            car = SelectInformation(reader);
                        }

                        return car;
                    }
                }
            }
        }

        public bool RegisterNewCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcs.REGISTER_NEW_CAR, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@makeCar", car.MakeCar);
                    command.Parameters.AddWithValue("@photo", null);
                    command.Parameters.AddWithValue("@ownerId", car.OwnerId);

                    command.ExecuteNonQuery();
                    //Convert.ToBoolean(command.ExecuteNonQuery());

                    return true;
                    
                }
            }
        }

        #endregion
    }
}
