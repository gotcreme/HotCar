using System.Web.Optimization;
using Forloop.HtmlHelpers;

namespace HotCar.WebUI.Frontend
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Scripts bundles

            #region Common Scripts

            bundles.Add(new ScriptBundle("~/bundles/common/scripts").Include(
                        "~/Scripts/Libs/jquery/jquery-1.9.1.js",
                         "~/Scripts/Libs/bootstrap/bootstrap.min.js",
                          "~/Scripts/Libs/plugins/retina.min.js",
                           "~/Scripts/Libs/jquery/plugins/jquery.validate.js",
                            "~/Scripts/Libs/bootstrap/jednotka.js",
                            "~/Scripts/Pages/Layout/style-switcher.js"));

            #endregion

            #region Home Page

            bundles.Add(new ScriptBundle("~/bundles/HomePage/scripts").Include(
                        "~/Scripts/Libs/jquery/jquery.mobile.custom.min.js",
                         "~/Scripts/Libs/plugins/modernizr.custom.min.js",
                          "~/Scripts/Libs/jquery/plugins/jquery.isotope.min.js",
                           "~/Scripts/Libs/jquery/plugins/jquery.flexslider.min.js",
                            "~/Scripts/Libs/plugins/lightbox.min.js"));

            #endregion
          
            #region Login Page

            bundles.Add(new ScriptBundle("~/bundles/Login/scripts").Include(
                       "~/Scripts/Pages/Login/login-page.js"));

            #endregion

            #region FindTrip Page

            bundles.Add(new ScriptBundle("~/bundles/FindTrip/scripts").Include(
                       "~/Scripts/Pages/FindTrip/index-page.js"));

            bundles.Add(new ScriptBundle("~/bundles/TripDetail/scripts").Include(
                      "~/Scripts/Pages/FindTrip/trip_detail-page.js"));

            bundles.Add(new ScriptBundle("~/bundles/FindTrip/results").Include(
                       "~/Scripts/Libs/jquery/plugins/jquery-ui.js",
                        "~/Scripts/Libs/jquery/plugins/jquery.timepicker.js",
                         "~/Scripts/Pages/FindTrip/results-page.js"));

            #endregion

            #region SuggestTrip Page

            bundles.Add(new ScriptBundle("~/bundles/SuggestPage/scripts").Include(
                       "~/Scripts/Libs/jquery/plugins/jquery-ui.js",
                        "~/Scripts/Libs/jquery/plugins/jquery.timepicker.js",
                         "~/Scripts/Pages/SuggestTrip/suggestTrip-page.js"));

            bundles.Add(new ScriptBundle("~/bundles/SuggestNext/scripts").Include(
                       "~/Scripts/Pages/SuggestTrip/suggestTripNext-page.js"));

            #endregion

            #region UserAccount Page

            bundles.Add(new ScriptBundle("~/bundles/UserAccount/scripts").Include(
                         "~/Scripts/Pages/UserAccount/userAccount.js"));

            #endregion

            #endregion

            #region Site Specific CSS

            bundles.Add(new StyleBundle("~/Content/jednotka/css").Include(
                     "~/Content/bootstrap/bootstrap.min.css",                    
                     "~/Content/jednotka/demo.css"));

            bundles.Add(new StyleBundle("~/Content/jednotka/login/css").Include(
                     "~/Content/jednotka/Login/style.css",
                     "~/Content/jednotka/Login/form-elements.css"));

            bundles.Add(new StyleBundle("~/Content/jednotka/suggest/css").Include(
                     "~/Content/bootstrap/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/jednotka/FindTrip/css").Include(
                     "~/Content/bootstrap/jquery-ui.css", 
                     "~/Content/jednotka/FindTrip/Results.css"));

            bundles.Add(new StyleBundle("~/Content/jednotka/UserAccount/css").Include(
                    "~/Content/jednotka/UserAccount/styles.css"));
      
            #endregion

            BundleTable.EnableOptimizations = true;
            ScriptContext.ScriptPathResolver = Scripts.Render;
        }
    }
}