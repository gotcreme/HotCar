using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotCar.Entities;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HotCar.BLL.Abstract;
using HotCar.WebUI.Frontend.Models;
using HotCar.WebUI.Frontend.Code;

namespace HotCar.WebUI.Frontend.Controllers
{
    public class AccountController : Controller
    {
        #region Private Fields

        private ISecurityManager _securityManager;

        #endregion

        #region Constructors

        public AccountController(ISecurityManager secure)
        {
            this._securityManager = secure;
        }

        #endregion

        #region Actions

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
       
        public ActionResult Logout()
        {
            this._securityManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string username = model.UserName;
                string password = model.Password;
                if (this._securityManager.Authentication(username, password))
                {
                                        
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));

                }
                else
                {
                    ModelState.AddModelError("","Incorrect username or password");
                    return View();
                }

            }
            else
            {
                return View();
            }          
        }

        [HttpGet]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.Login = model.UserName;
                user.FirstName = model.FirstName;
                user.SurName = model.LastName;
                user.Password = model.Password;
                user.Mail = model.Email;

                bool? createAccount = this._securityManager.CreateAccount(user);
                if (createAccount == true)
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else if(createAccount == false)
                {
                    ModelState.AddModelError("", "There is some user with same login");
                    return View();                   
                }
                else
                {
                    ModelState.AddModelError("", "There is some user with same email");
                    return View();
                }
             }
            else
             {
                 ModelState.AddModelError("", "Incorrect data");
                 return View();
             }                   
        }

        #endregion
    }
}