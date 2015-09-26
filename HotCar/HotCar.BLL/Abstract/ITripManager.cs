using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotCar.Entities;

namespace HotCar.BLL.Abstract
{
    public interface ITripManager
    {
        void AddNew(Trip trip);
        List<Trip> GetPassengerAvailableTrips(String from, String to, double tolerance = 1000);
        List<Trip>[] GetAllUserTrips(int userId);
        Trip GetTripById(int tripId);  // added

    }
}
