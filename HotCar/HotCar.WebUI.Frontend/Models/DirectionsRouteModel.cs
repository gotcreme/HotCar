using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI.WebControls;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using System.ComponentModel.DataAnnotations;
using System.Web.Configuration;


namespace HotCar.WebUI.Frontend.Models
{
    public class DirectionsRouteModel
    {
        #region Fields

        public String[] WayPoints { get; set; }
        
        public DirectionsResponse DirectionsResponse { get; set; }

        public bool Regular { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime Date { get; set; }

        public decimal Price { get; set; }
        public int Free { get; set; }
        public String AdditionalInfo { get; set; }

        #endregion

        #region Properties

        public String Distanse
        {
            get { return this.GetDistance().ToString() + " км"; }
        }

        public String Duration
        {
            get
            {
                TimeSpan timeSpan = this.GetDuration();
                return timeSpan.Hours + " год " + timeSpan.Minutes + "хв";
            }
        }

        public String DateString
        {
            get
            {
                return this.Date.Day.ToString("D2") + "/" +
                        this.Date.Month.ToString("D2") + "/" +
                        this.Date.Year.ToString("D4") + " " +
                        this.Date.Hour.ToString("D2") + ":" +
                        this.Date.Minute.ToString("D2");
            }
        }

        #endregion

        #region Constructors

        public DirectionsRouteModel()
        {
        }

        #endregion

        #region Methods

        public void CreateDirectionsResponse()
        {
            List<String> wayPoints = new List<String>();
            for (int a = 1; a < this.WayPoints.Length - 1; a++)
            {
                if (!String.IsNullOrEmpty(this.WayPoints[a]) && !String.IsNullOrWhiteSpace(this.WayPoints[a]))
                    wayPoints.Add(this.WayPoints[a]);
            }

            DirectionsRequest directionsRequest = new DirectionsRequest()
            {
                Origin = this.WayPoints[0],
                Destination = this.WayPoints[this.WayPoints.Length - 1],
                Waypoints = wayPoints.ToArray(),
                TravelMode = TravelMode.Driving,
                ApiKey = WebConfigurationManager.AppSettings["GoogleMapsAPIKey"]
            };

            this.DirectionsResponse = GoogleMapsApi.GoogleMaps.Directions.Query(directionsRequest);
        }

        public double GetDistance()
        {
            double distance = 0.0;

            if (this.DirectionsResponse == null)
            {
                this.CreateDirectionsResponse();
            }

            Leg[] legs = this.DirectionsResponse.Routes.ElementAt(0).Legs.ToArray();
            for (int a = 0; a < legs.Length; a++)
            {
                distance += legs[a].Distance.Value;
            }

            return (distance / 1000.0);
        }

        public TimeSpan GetDuration()
        {
            TimeSpan duration = new TimeSpan();

            if (this.DirectionsResponse == null)
            {
                this.CreateDirectionsResponse();
            }

            Leg[] legs = this.DirectionsResponse.Routes.ElementAt(0).Legs.ToArray();
            for (int a = 0; a < legs.Length; a++)
            {
                duration += legs[a].Duration.Value;
            }

            return duration;
        }

        public Location[] GetLocations()
        {
            List<Location> locations = new List<Location>();

            for (int a = 0; a < this.WayPoints.Length; a++)
            {
                if (!String.IsNullOrEmpty(this.WayPoints[a]) && !String.IsNullOrWhiteSpace(this.WayPoints[a]))
                {
                    GeocodingRequest geocodingRequest = new GeocodingRequest()
                    {
                        Address = this.WayPoints[a]
                    };

                    GeocodingResponse geocodingResponse = GoogleMapsApi.GoogleMaps.Geocode.Query(geocodingRequest);

                    locations.Add(geocodingResponse.Results.ElementAt(0).Geometry.Location);
                }
            }

            return locations.ToArray();
        }

        #endregion
    }
}