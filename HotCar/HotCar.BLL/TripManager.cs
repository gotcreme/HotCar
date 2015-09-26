using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using HotCar.BLL.Abstract;
using HotCar.Entities;
using HotCar.Repositories.Sql;
using HotCar.Repositories.Abstract;

namespace HotCar.BLL
{
    public class TripManager : ITripManager
    {
        #region Private Fields

        private ITripRepository _tripRepository;

        #endregion

        #region Constructor

        public TripManager(ITripRepository tripRepository)
        {          
            this._tripRepository = tripRepository;
        }

        #endregion

        #region Interface Implementation

        public void AddNew(Trip trip)
        {
            var tripId = this.AddTrip(trip);
            this.AddRoute(trip.RouteLocations, tripId);
        }

        public List<Trip> GetPassengerAvailableTrips(String from, String to, double tolerance = 1000)
        {
            List<Trip> driverTrips = this._tripRepository.GetAllPassengerAvailableTrips(DateTime.Now.AddMinutes(15));
            List<Trip> passengerAvailableTrips = new List<Trip>();

            for (int a = 0; a < driverTrips.Count; a++)
            {
                if (driverTrips[a].IsOnRoute(from, to, tolerance))
                {
                    passengerAvailableTrips.Add(driverTrips[a]);
                }
            }

            this.GetLocationsAddressess(passengerAvailableTrips);                                                            //added

            return passengerAvailableTrips;
        }

        public List<Trip>[] GetAllUserTrips(int userId)
        {
            List<Trip> asDriver = this._tripRepository.GetAllTripsByDriverId(userId);
            List<Trip> asPassenger = this._tripRepository.GetAllTripsByPassengerId(userId);

            List<Trip> actual = new List<Trip>();
            List<Trip> outDated = new List<Trip>();

            DateTime now = DateTime.Now.AddMinutes(1);

            for (int a = 0, b = 0; ; a++, b++)
            {
                if (a < asDriver.Count)
                {
                    if (asDriver[a].TripTime > now)
                    {
                        actual.Add(asDriver[a]);
                    }

                    else
                    {
                        outDated.Add(asDriver[a]);
                    }
                }

                if (b < asPassenger.Count)
                {
                    if (asPassenger[b].TripTime > now)
                    {
                        actual.Add(asPassenger[b]);
                    }

                    else
                    {
                        outDated.Add(asPassenger[b]);
                    }
                }

                if (a >= asDriver.Count && b >= asPassenger.Count)
                {
                    break;
                }
            }

            this.GetLocationsAddressess(actual);
            this.GetLocationsAddressess(outDated);

            return new List<Trip>[] { actual, outDated };
        }

        public Trip GetTripById(int tripId) // added
        {
            List<Trip> trips = this._tripRepository.GetTripById(tripId);
            this.GetLocationsAddressess(trips);

            return trips[0];
        }

        #endregion

        #region Helpers

        private int AddTrip(Trip trip)
        {
            return this._tripRepository.InsertTrip(trip);
        }

        private void AddRoute(List<LocationInfo> location, int tripId)
        {
            foreach (var locationInfo in location)
            {
                this._tripRepository.AddRoute(locationInfo, tripId);
            }
        }

        private void GetLocationsAddressess(List<Trip> trips)
        {
            for (int a = 0; a < trips.Count; a++)
            {
                trips[a].GetLocationAddresses();
            }
        }

        #endregion
    }
}
