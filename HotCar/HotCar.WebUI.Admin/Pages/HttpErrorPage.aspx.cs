using System;
using System.Web;
using System.Web.UI;
using HotCar.Entities.Enums;
using HotCar.WebUI.Admin.Code.Keys;

namespace HotCar.WebUI.Admin.Pages
{
    public partial class HttpErrorPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.lblErrorCode.Text = (String)(this.Session[SessionKeys.HTTP_EXCEPTION]) ?? String.Empty;
                HttpException ex = (HttpException) Server.GetLastError();
                String error = ex.GetHttpCode().ToString();
                this.lblErrorCode.Text = error;
                this.Session[SessionKeys.HTTP_EXCEPTION] = error;
            }

            catch
            { }
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