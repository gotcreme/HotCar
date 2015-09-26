using System.Collections.Generic;
using HotCar.Entities;

namespace HotCar.Repositories.Abstract
{
    public interface IPassengerRepository
    {
        List<Passenger> GetAllPassengersByTripId(int tripId);
        bool SignPassengerToTrip(Passenger passenger);
        bool UnsignPassengerFromTrip(Passenger passenger);
        bool UpdatePassengerFromTrip(Passenger passenger);
    }
}
