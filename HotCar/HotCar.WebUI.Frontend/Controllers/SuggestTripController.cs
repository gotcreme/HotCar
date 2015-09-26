using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using GoogleMapsApi.Entities.Common;
using HotCar.BLL.Abstract;
using HotCar.Entities;
using HotCar.WebUI.Frontend.Code;
using HotCar.WebUI.Frontend.Models;

namespace HotCar.WebUI.Frontend.Controllers
{
    public class SuggestTripController : Controller
    {
        #region Fields

        private ITripManager _tripManager;

        #endregion

        #region Constructor

        public SuggestTripController(ITripManager tripManager)
        {
            this._tripManager = tripManager;
        }
        #endregion

        #region WebActions

        [Authorize]
        [HttpGet]
        public ViewResult SuggestTrip()
        {
            DirectionsRouteModel directionsRoute = (this.Session[SessionKeys.ROUTE] as DirectionsRouteModel) ?? new DirectionsRouteModel();
            return this.View(directionsRoute);
        }

        [HttpPost]
        public ActionResult SuggestTrip(DirectionsRouteModel directionsRoute)
        {
            this.Session[SessionKeys.ROUTE] = directionsRoute;
            return this.RedirectToAction("SuggestTripNext", "SuggestTrip");
        }

        [Authorize]
        [HttpGet]
        public ViewResult SuggestTripNext()
        {
            DirectionsRouteModel directionsRoute = this.Session[SessionKeys.ROUTE] as DirectionsRouteModel;
            return this.View(directionsRoute);
        }

        [HttpPost]
        public ActionResult SuggestTripNext(DirectionsRouteModel directionsRoute)
        {
            this._tripManager.AddNew(this.GetTrip(directionsRoute));

            return this.RedirectToAction("Index","Home");
        }

        #endregion

        #region Helpers

        public Trip GetTrip(DirectionsRouteModel directionsRoute)
        {
            DirectionsRouteModel driverRoute = this.Session[SessionKeys.ROUTE] as DirectionsRouteModel;
            this.Session[SessionKeys.ROUTE] = null;

            Trip trip = new Trip();
            trip.AvailablePlacesCount = directionsRoute.Free;
            trip.CarId = 1;
            trip.CostOneSeat = directionsRoute.Price;
            trip.TripTime = driverRoute.Date;
            trip.Driver.Login = this.User.Identity.Name;
            trip.AdditionalInfo = directionsRoute.AdditionalInfo;

            Location[] locations = driverRoute.GetLocations();
            for (int a = 0; a < locations.Length; a++)
            {
                trip.RouteLocations.Add(new LocationInfo(locations[a].Latitude, locations[a].Longitude));
            }

            return trip;
        }

        #endregion
    }
}