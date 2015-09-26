using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using NLog;

namespace HotCar.WebUI.Admin
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, roles);

                    }
                }
            }

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                if (this.Server.GetLastError() != null)
                {
                    Exception ex = this.Server.GetLastError().GetBaseException();

                    if (ex is HttpException)
                    {
                        this.Session["Error"] = "Error";
                        this.Server.Transfer("~/Pages/HttpErrorPage.aspx");
                    }
                    else
                    {
                        this.Session["Error"] = "Error";
                        this.Server.Transfer("~/Pages/ExceptionErrorPage.aspx");
                        Logger logger = LogManager.GetCurrentClassLogger();
                        logger.Log(LogLevel.Error, ex);
                    }
                }
            }
            catch
            { }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}