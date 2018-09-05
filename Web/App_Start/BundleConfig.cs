using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryeasing").Include(
                      "~/Scripts/jquery.easing.1.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerywaypoints").Include(
                      "~/Scripts/jquery.waypoints.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryflexslider").Include(
                      "~/Scripts/jquery.flexslider-min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/stickkit").Include(
            //          "~/Scripts/stick-kit.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/owlcarousel").Include(
                      "~/Scripts/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycount").Include(
                      "~/Scripts/jquery.countTo.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/animate.css",
                      "~/Content/icomoon.css",
                      "~/Content/bootstrap.css",
                      "~/Content/flexslider.css",
                      "~/Content/fonts/flaticon.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/owl.theme.default.min.css",
                      "~/Content/style.css",
                      "~/Content/site.css"));
        }
    }
}
