using System.Collections.Generic;
using HotCar.Entities;

namespace HotCar.Repositories.Abstract
{
    public interface ICarRepository
    {
        List<Car> GetAllCarsByUserId(int carId);
        Car GetCarByTripId(int tripId);
        bool DeleteCarById(int carId);
        bool UpdateCarById(Car car);
        bool RegisterNewCar(Car car);
    }
}
