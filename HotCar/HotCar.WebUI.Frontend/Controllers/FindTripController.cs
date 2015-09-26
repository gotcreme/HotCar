using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoogleMapsApi.Entities.Directions.Response;
using HotCar.BLL;
using HotCar.BLL.Abstract;
using HotCar.Entities;
using HotCar.WebUI.Frontend.Code;
using HotCar.WebUI.Frontend.Models;

namespace HotCar.WebUI.Frontend.Controllers
{
    public class FindTripController : Controller
    {
        #region Private Fields

        private List<Trip> _trips;                  // static ???
        private ITripManager _tripManager;

        #endregion

        #region Constructors

        public FindTripController(ITripManager tripManager)
        {
            this._tripManager = tripManager;
        }

        #endregion

        #region Actions

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FindTripModel findTrip)
        {          
            _trips = new List<Trip>(this._tripManager.GetPassengerAvailableTrips(findTrip.From, findTrip.To));
            this.Session[SessionKeys.TRIPS] = _trips;
            
            return Results();
        }

        [HttpPost]
        public ActionResult Results(int page = 0, FiltersModel filters = null)
        {
            const int PageSize = 4;

            this._trips = (List<Trip>)this.Session[SessionKeys.TRIPS];

            var data = new List<Trip>(_trips);

            this.ViewBag.NoSeatsCount = data.Count(x => x.AvailablePlacesCount == 0);
            this.ViewBag.PhotoCount = data.Count(x => x.Driver.Photo.Photo != null);
            
            if (filters != null)
            {
                if (filters.Date != null)
                {
                    DateTime date = DateTime.ParseExact(filters.Date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    data.RemoveAll(x => x.TripTime.Date != date.Date);
                }

                data.RemoveAll(x => (x.TripTime.Hour < filters.FromHour));
                data.RemoveAll(x => (x.TripTime.Hour > filters.ToHour));
                data.RemoveAll(x => (x.TripTime.Hour == filters.ToHour) && (x.TripTime.Minute > 0));

                if (filters.HideWithNoSeats == true)
                {
                    data.RemoveAll(x => x.AvailablePlacesCount == 0);
                }

                this.ViewBag.WithWithoutPhotoCount = data.Count;
                this.ViewBag.PhotoCount = data.Count(x => x.Driver.Photo.Photo != null);

                if (filters.HideWithNoPhoto == true)
                {
                    data.RemoveAll(x => x.Driver.Photo.Photo == null);
                }
                this.ViewBag.NoSeatsCount = data.Count(x => x.AvailablePlacesCount == 0);
            }
            data = data.OrderBy(x => x.TripTime).ToList();

            var count = data.Count;

            var data_to_show_count = (count - (page * PageSize) >= PageSize) ? PageSize : count - (page * PageSize);
            data = data.GetRange((page * PageSize), data_to_show_count);

            this.ViewBag.MaxPage = (count == 0) ? 0 : (count / PageSize) - ((count % PageSize) == 0 ? 1 : 0);
            this.ViewBag.Page = page;
            this.ViewBag.Data = data;
            this.ViewBag.Count = count;


            if (Request.IsAjaxRequest())
            {
                return PartialView("ResultsPartial", ViewBag.Data);
            }
            return View("Results");
        }

        [HttpGet]
        public ActionResult TripDetail(int id)
        {
            Trip data = this._tripManager.GetTripById(id);  //copy this

            return View(data);
        }

        #endregion
    }
}