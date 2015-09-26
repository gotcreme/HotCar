using HotCar.BLL.Abstract;
using HotCar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotCar.WebUI.Frontend.Controllers
{
    public class LayoutController : Controller
    {

        #region Private Fields

        private ISecurityManager _securityManager;
        private IUsersManager _userManager;

        #endregion

        #region Constructors

        public LayoutController(ISecurityManager securityManager, IUsersManager userManager)
        {
            this._securityManager = securityManager;
            this._userManager = userManager;
        }

        #endregion

        #region Web Actions

        public ActionResult UserPhoto()
        {
            string login = this.User.Identity.Name; // ToDo: use SecurityManager for this purpose
            UserPhoto userPhoto = this._userManager.GetUserPhoto(login);
            if (userPhoto.Photo != null)
            {
                return File(userPhoto.Photo, MimeMapping.GetMimeMapping(userPhoto.FileExtension));
            }
            else
            {
                string fileName = Server.MapPath("~/Content/images/no-user-photo.png");
                return File(fileName, MimeMapping.GetMimeMapping(fileName));
            }

        }

        #endregion
    }
}