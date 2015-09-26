using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;

namespace HotCar.Entities
{
    public class Trip
    {
        #region Fields

        public int Id { get; set; }
        public User Driver { get; set; }
        public List<LocationInfo> RouteLocations { get; set; }
        public int AvailablePlacesCount { get; set; }
        public DateTime TripTime { get; set; }
        public decimal CostOneSeat { get; set; }
        public int CarId { get; set; }
        public List<String> LocationAddresses { get; set; }
        public String AdditionalInfo { get; set; }
        private DirectionsResponse DirectionsResponse { get; set; }

        #endregion

        #region Constructors

        public Trip()
        {
            this.RouteLocations = new List<LocationInfo>();
            this.Driver = new User();
        }

        #endregion

        #region Methods
        public void CreateDirectionsResponse()
        {
            Location[] locations = new Location[this.RouteLocations.Count];
            for (int a = 0; a < locations.Length; a++)
            {
                locations[a] = new Location(this.RouteLocations[a].Latitude, this.RouteLocations[a].Longitude);
            }

            String[] wayPoints = new String[locations.Length - 2];
            for (int a = 1; a < locations.Length - 1; a++)
            {
                wayPoints[a - 1] = locations[a].LocationString;
            }

            DirectionsRequest directionsRequest = new DirectionsRequest()
            {
                Origin = locations[0].LocationString,
                Destination = locations[locations.Length - 1].LocationString,
                Waypoints = wayPoints,
                TravelMode = TravelMode.Driving,
                ApiKey = ConfigurationManager.AppSettings["GoogleMapsAPIKey"]
            };

            this.DirectionsResponse = GoogleMapsApi.GoogleMaps.Directions.Query(directionsRequest);
        }

        public bool IsOnRoute(String from, String to, double tolerance)
        {
            if (this.DirectionsResponse == null)
            {
                this.CreateDirectionsResponse();
            }

            Location[] locations = this.DecodePolyline(this.DirectionsResponse.Routes.ElementAt(0).OverviewPath.GetRawPointsData());

            DirectionsRequest startRequest = new DirectionsRequest()
            {
                Origin = from,
                Destination = locations[locations.Length - 1].LocationString,
                ApiKey = ConfigurationManager.AppSettings["GoogleMapsAPIKey"]
            };

            DirectionsRequest endRequest = new DirectionsRequest()
            {
                Origin = to,
                Destination = locations[locations.Length - 1].LocationString,
                ApiKey = ConfigurationManager.AppSettings["GoogleMapsAPIKey"]
            };

            DirectionsResponse startResponse = GoogleMapsApi.GoogleMaps.Directions.Query(startRequest);

            DirectionsResponse endResponse = GoogleMapsApi.GoogleMaps.Directions.Query(endRequest);


            double distanceStart = this.GetDistance(startResponse);
            double distanceEnd = this.GetDistance(endResponse);

            if ((distanceStart > distanceEnd) && this.IsLocationOnEdge(locations, from, to, tolerance))
            {
                return true;
            }

            return false;

            return true;
        }

        private bool IsLocationOnEdge(Location[] locations, String from, string to, double tolerance)
        {
            GeocodingRequest startRequest = new GeocodingRequest()
            {
                Address = from,
                ApiKey = ConfigurationManager.AppSettings["GoogleMapsAPIKey"]
            };

            GeocodingRequest endRequest = new GeocodingRequest()
            {
                Address = to,
                ApiKey = ConfigurationManager.AppSettings["GoogleMapsAPIKey"]

            };

            GeocodingResponse startResponse = GoogleMapsApi.GoogleMaps.Geocode.Query(startRequest);
            GeocodingResponse endResponse = GoogleMapsApi.GoogleMaps.Geocode.Query(endRequest);

            Location startLocation = startResponse.Results.ElementAt(0).Geometry.Location;
            Location endLocation = endResponse.Results.ElementAt(0).Geometry.Location;

            bool startIsOnEdge = false;
            bool endIsOnEdge = false;

            for (int a = 0; a < locations.Length; a++)
            {
                if (!startIsOnEdge && this.GetStraightDistance(locations[a], startLocation) <= tolerance)
                {
                    startIsOnEdge = true;
                }

                if (!endIsOnEdge && this.GetStraightDistance(locations[a], endLocation) <= tolerance)
                {
                    endIsOnEdge = true;
                }

                if (startIsOnEdge && endIsOnEdge)
                {
                    break;
                }
            }

            if (startIsOnEdge && endIsOnEdge)
            {
                return true;
            }

            return false;
        }

        public double GetDistance(DirectionsResponse directionsResponse)
        {
            double distance = 0.0;

            Leg[] legs = directionsResponse.Routes.ElementAt(0).Legs.ToArray();
            for (int a = 0; a < legs.Length; a++)
            {
                distance += legs[a].Distance.Value;
            }

            return distance / 1000.0;
        }

        private double GetStraightDistance(Location location, Location location2)
        {
            double R = 6371000;
            double dLat = this.ToRad(location2.Latitude - location.Latitude);
            double dLon = this.ToRad(location2.Longitude - location.Longitude);

            double dLat1 = this.ToRad(location.Latitude);
            double dLat2 = this.ToRad(location2.Latitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(dLat1) * Math.Cos(dLat1) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return distance;
        }

        private double ToRad(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private Location[] DecodePolyline(String encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
            {
                return null;
            }

            List<Location> locations = new List<Location>();
            char[] polyLineChars = encodedPoints.ToCharArray();

            int index = 0;
            int currentLat = 0;
            int currentLng = 0;
            int next5bits;

            while (index < polyLineChars.Length)
            {
                int sum = 0;
                int shifter = 0;

                do
                {
                    next5bits = (int)polyLineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polyLineChars.Length);

                if (index >= polyLineChars.Length)
                {
                    break;
                }

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polyLineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polyLineChars.Length);

                if (index >= polyLineChars.Length && next5bits >= 32)
                {
                    break;
                }

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                Location p = new Location(0, 0);
                p.Latitude = Convert.ToDouble(currentLat) / 100000.0;
                p.Longitude = Convert.ToDouble(currentLng) / 100000.0;
                locations.Add(p);
            }

            return locations.ToArray();
        }

        public void GetLocationAddresses()
        {
            List<String> addressess = new List<string>();

            if (this.DirectionsResponse == null)
            {
                this.CreateDirectionsResponse();
            }

            Leg[] legs = this.DirectionsResponse.Routes.ElementAt(0).Legs as Leg[];

            for (int a = 0; a < legs.Length; a++)
            {
                addressess.Add(legs[a].StartAddress);
            }

            addressess.Add(legs[legs.Length - 1].EndAddress);

            this.LocationAddresses = addressess;
        }

        #endregion
    }
}
