using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotCar.Entities;

namespace HotCar.Repositories.Abstract
{
    public interface ITripRepository
    {
        List<Trip> GetAllPassengerAvailableTrips(DateTime time);
        List<Trip> GetAllTripsByDriverId(int id);
        int GetCountConductedTripsByDriverId(int id);
        List<Trip> GetAllTripsByPassengerId(int id);
        int GetCountConductedTripsByPassengerId(int id);
        bool DeleteTripById(int id);
        bool UpdateTripById(Trip trip);
        int InsertTrip(Trip tripInfo);
        bool AddRoute(LocationInfo location, int id);
        List<Trip> GetTripById(int tripId);     //added
    }
}
