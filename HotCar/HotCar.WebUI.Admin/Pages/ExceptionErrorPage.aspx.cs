using System;
using System.Globalization;
using System.Web.UI;
using HotCar.Entities.Enums;
using HotCar.WebUI.Admin.Code.ExceptionWorking;
using HotCar.WebUI.Admin.Code.Keys;

namespace HotCar.WebUI.Admin.Pages
{
    public partial class ExceptionErrorPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.lblWhat.Text = (String)this.Session[SessionKeys.WHAT] ?? String.Empty;
                this.lblWhy.Text = (String)this.Session[SessionKeys.WHY] ?? String.Empty;
                this.lblSuggestion.Text = (String)this.Session[SessionKeys.SUGGESTION] ?? String.Empty;

                Exception ex = this.Server.GetLastError().GetBaseException();
                ExceptionInfo exceptionInfo = new ExceptionInfo(ex);

                int index = 0;

                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    index = 1;
                }

                string what = exceptionInfo.GetInfo.WhatHappenedInfo.Split('/')[index];
                string why = exceptionInfo.GetInfo.WhyHappenedInfo.Split('/')[index];
                string suggestion = exceptionInfo.GetInfo.SuggestionInfo.Split('/')[index];

                this.lblWhat.Text = what;
                this.lblWhy.Text = why;
                this.lblSuggestion.Text = suggestion;

                this.Session[SessionKeys.WHAT] = what;
                this.Session[SessionKeys.WHY] = why;
                this.Session[SessionKeys.SUGGESTION] = suggestion;
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