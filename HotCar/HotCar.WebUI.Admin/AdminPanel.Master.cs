using System;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotCar.Entities.Enums;
using HotCar.WebUI.Admin.Code.Keys;

namespace HotCar.WebUI.Admin
{
    public partial class AdminPanel : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            LinkButton engButton = (LinkButton) (this.Menu.FindControl("lBtnEng"));
            LinkButton ukrButton = (LinkButton) (this.Menu.FindControl("lBtnUkr"));

            if (this.Session[SessionKeys.ERROR] == null)
            {
                engButton.Click += new EventHandler(this.EngButtonClick);
                ukrButton.Click += new EventHandler(this.UkrButtonClick);
            }

            else
            {
                this.Session[SessionKeys.ERROR] = null;
                engButton.Visible = false;
                ukrButton.Visible = false;

                Label leftSeparator = (Label)(this.Menu.FindControl("lblLeftSeparator"));
                Label coma = (Label)(this.Menu.FindControl("lblComa"));
                Label rightSeparator = (Label)(this.Menu.FindControl("lblRightSeparator"));

                leftSeparator.Visible = false;
                coma.Visible = false;
                rightSeparator.Visible = false;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void EngButtonClick(object sender, EventArgs e)
        {
            this.Session[SessionKeys.UI_CULTURE] = UICultures.en.ToString();
            this.Response.Redirect(this.Request.RawUrl);
        }

        private void UkrButtonClick(object sender, EventArgs e)
        {
            this.Session[SessionKeys.UI_CULTURE] = UICultures.uk.ToString();
            this.Response.Redirect(this.Request.RawUrl);
        }
    }
}