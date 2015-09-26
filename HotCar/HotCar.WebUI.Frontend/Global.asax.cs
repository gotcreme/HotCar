using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using HotCar.BLL;
using HotCar.Entities;
using HotCar.Repositories.Sql;
using HotCar.WebUI.Frontend.Code;

namespace HotCar.WebUI.Frontend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);          
        }

       
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {                                      
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;
                        bool status;

                        var securityManager =
                            new SecurityManager(
                                new UserRepository(
                                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString));

                        roles = securityManager.ReadUserRole(username, out status);

                        if (status)
                        {
                            securityManager.SignOut();
                        }
                        else
                        {
                            //Let us set the Pricipal with our user specific details
                            HttpContext.Current.User = new GenericPrincipal(
                              new GenericIdentity(username, "Forms"), roles.Split(';'));
                        }
                        
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        } 
    }
}
