using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotCar.BLL;
using HotCar.BLL.Abstract;
using HotCar.Entities.Enums;
using HotCar.Repositories.Sql;
using HotCar.WebUI.Admin.Code.Keys;

namespace HotCar.WebUI.Admin.Pages
{
    public partial class Login : Page
    {
        #region Private Fields

        private IUsersManager _usersManager;

        #endregion

       
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Page.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    this.Response.Redirect("~/Pages/Login.aspx");
                }
            }
        }

        protected void loginForm_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var userLogin = this.loginForm.UserName;
            var userPassword = this.loginForm.Password;
            var rememberUser = this.loginForm.RememberMeSet;
            
            //read data from repository
            var userManager = new UsersManager(new UserRepository(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString));
            var userRoles = userManager.UserAuthentication(userLogin, userPassword);         
            if (userRoles != null)
            {                             
                // Create forms authentication ticket
                var ticket = new FormsAuthenticationTicket(
                    1,
                    userLogin,
                    DateTime.Now, 
                    DateTime.Now.AddMinutes(10),
                    rememberUser,
                    userRoles,
                    FormsAuthentication.FormsCookiePath);

                var hash = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }

                // Add the cookie to the response
                this.Response.Cookies.Add(cookie);

                var returnUrl = this.Request.QueryString["ReturnUrl"] ?? "~/Secure/AdminPage.aspx";

                this.Response.Redirect(returnUrl);
                            
            } 
        }

        protected override void InitializeCulture()
        {
            if (this.Session[SessionKeys.UI_CULTURE] == null)
            {
                this.Session[SessionKeys.UI_CULTURE] = UICultures.uk.ToString();
            }

            this.UICulture = (String)this.Session[SessionKeys.UI_CULTURE];
            base.InitializeCulture();
        }
    }
}