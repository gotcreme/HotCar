using System;
using System.Web.Configuration;
using System.Web.Mvc;
using HotCar.BLL;
using HotCar.BLL.Abstract;
using HotCar.WebUI.Frontend.Models;


namespace HotCar.WebUI.Frontend.Controllers
{
    public class UserAccountController : Controller
    {
        #region Fields

        private ITripManager _tripManager;
        private IUsersManager _userManager;

        #endregion

        #region Constructor

        public UserAccountController(IUsersManager userManager, ITripManager tripManager)
        {
            this._userManager = userManager;
            this._tripManager = tripManager;
        }
        #endregion


        //[HttpGet]
        public ActionResult Index()
        {
            UserModel userModel = new UserModel();
            userModel.UserInfo = this._userManager.GetUserByLogin(this.User.Identity.Name);
            userModel.ActualTrips = this._tripManager.GetAllUserTrips(userModel.UserInfo.Id)[0].ToArray();
            userModel.OutDatedTrips = this._tripManager.GetAllUserTrips(userModel.UserInfo.Id)[1].ToArray();

            for (int a = 0; a < userModel.ActualTrips.Length; a++)
            {
                userModel.ActualTrips[a].GetLocationAddresses();
            }

            return View(userModel);
        }
    }
}