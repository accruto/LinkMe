using System.Web.Optimization;
using LinkMe.Apps.Asp.Content;
using LinkMe.Environment;

namespace LinkMe.Web.Content
{
    public class ScriptBundles
        : Bundles
    {
        public static BundleUrl Site { get; private set; }

        public static BundleUrl Home { get; private set; }
        public static BundleUrl EmployerHome { get; private set; }
        public static BundleUrl JobSearch { get; private set; }
        public static BundleUrl JobSearchResults { get; private set; }
        public static BundleUrl RecentJobSearches { get; private set; }
        public static BundleUrl Resources { get; private set; }
        public static BundleUrl Devices { get; private set; }
        public static BundleUrl DeviceSearch { get; private set; }
        public static BundleUrl DeviceResults { get; private set; }
        //public static BundleUrl IScroll { get; private set; }
        //public static BundleUrl SpinningWheel { get; private set; }
        public static BundleUrl DeviceJobAd { get; private set; }
        public static BundleUrl DeviceLogin { get; private set; }
        public static BundleUrl DeviceApply { get; private set; }
        public static BundleUrl DeviceApplied { get; private set; }
        public static BundleUrl DeviceSavedSearches { get; private set; }
        public static BundleUrl DeviceSaveSearch { get; private set; }
        public static BundleUrl DeviceJobAdItem { get; private set; }
        public static BundleUrl DeviceEmailJobAd { get; private set; }
        public static BundleUrl DeviceJoin { get; private set; }

        public class EmployersJobAdsBundleUrls
        {
            public BundleUrl JobAd { get; set; }
            public BundleUrl Preview { get; set; }
        }

        public class EmployersProductsBundleUrls
        {
            public BundleUrl Choose { get; set; }
        }

        public class EmployersBundleUrls
        {
            public BundleUrl Orders { get; set; }
            public EmployersJobAdsBundleUrls JobAds { get; set; }
            public EmployersProductsBundleUrls Products { get; set; }
        }

        public class MembersBundleUrls
        {
            public BundleUrl Profiles { get; set; }
        }

        public class SupportBundleUrls
        {
            public BundleUrl Faqs { get; set; }
        }

        public static EmployersBundleUrls Employers { get; private set; }
        public static MembersBundleUrls Members { get; private set; }
        public static BundleUrl Administrators { get; private set; }
        public static SupportBundleUrls Support { get; private set; }

        public static void RegisterBundles(BundleCollection bundles)
        {
            Site = AddScriptBundle(
                bundles,
                "~/bundle/js/site",
                JavaScripts.Api,
                JavaScripts.Validation,
                JavaScripts.OutsideEvents,
                JavaScripts.Fields,
                JavaScripts.Checkboxes);

            Home = AddScriptBundle(
                bundles,
                "~/bundle/js/home",
                "~/content/js/homepage.js",
                "~/content/js/common/fields.js",
                "~/content/js/jquery/plugins/linkme.autocomplete.js",
                "~/content/js/jquery/plugins/jcarousellite_1.0.1c4.js",
                "~/content/js/UnMungeUrl.js",
                "~/content/js/jquery/plugins/linkme.custom-checkbox.js",
                "~/content/js/jQuery/plugins/jquery.ba-outside-events.js");

            EmployerHome = AddScriptBundle(
                bundles,
                "~/bundle/js/employerhome",
                "~/content/js/employers/homepage.js",
                "~/content/js/jquery/plugins/linkme.autocomplete.js",
                "~/content/js/jquery/plugins/linkme.tooltips.js",
                "~/content/js/employers/search.js",
                "~/content/js/employers/overlay.js");

            JobSearch = AddScriptBundle(
                bundles,
                "~/bundle/js/jobsearch",
                "~/content/js/members/jobsearch.js",
                "~/content/js/common/fields.js",
                "~/content/js/jquery/plugins/linkme.autocomplete.js",
                "~/content/js/jQuery/plugins/jquery.ellipsis.js",
                "~/content/js/jQuery/plugins/jquery.ba-outside-events.js");

            JobSearchResults = AddScriptBundle(
                bundles,
                "~/bundle/js/jobsearchresults",
                "~/content/js/members/result.js",
                "~/content/js/common/fields.js",
                "~/content/js/jquery/plugins/linkme.autocomplete.js",
                "~/content/js/jQuery/plugins/jquery.ellipsis.js",
                "~/content/js/UnMungeUrl.js",
                "~/content/js/jQuery/plugins/jquery.ba-outside-events.js");

            RecentJobSearches = AddScriptBundle(
                bundles,
                "~/bundle/js/recentjobsearches",
                "~/content/js/members/recentjobsearch.js",
                "~/content/js/common/fields.js",
                "~/content/js/UnMungeUrl.js",
                "~/content/js/jquery/linkme.jquery-ui-1.8.12.custom.js",
                "~/content/js/jQuery/plugins/jquery.ba-outside-events.js");

            Resources = AddScriptBundle(
                bundles,
                "~/bundle/js/resources",
                "~/content/js/Resources/resources.js",
                "~/content/js/UnMungeUrl.js",
                "~/content/js/VoteControl.js",
                "~/content/js/jQuery/plugins/jquery.json2.js",
                "~/content/js/jQuery/plugins/jquery.ellipsis.js",
                "~/content/js/social.js");

            Devices = (RuntimeEnvironment.Environment == ApplicationEnvironment.Prod || RuntimeEnvironment.Environment == ApplicationEnvironment.Uat) ? AddScriptBundle(
                bundles,
                "~/bundle/js/devices",
                "~/content/js/device/fields.js",
                "~/content/js/UnMungeUrl.js",
                "~/content/js/jQuery/plugins/jquery.ba-outside-events.min.js") : AddScriptBundle(
                bundles,
                "~/bundle/js/devices",
                "~/content/js/device/ggs.min.js",
                "~/content/js/device/fields.js",
                "~/content/js/UnMungeUrl.js",
                "~/content/js/jQuery/plugins/jquery.ba-outside-events.min.js");

            DeviceSearch = AddScriptBundle(
                bundles,
                "~/bundle/js/devicesearch",
                "~/content/js/device/search.js",
                "~/content/js/device/jquery.ui.touch-punch.min.js");

            DeviceResults = AddScriptBundle(
                bundles,
                "~/bundle/js/deviceresults",
                "~/content/js/members/jobads.api.js",
                "~/content/js/device/results.js",
                "~/content/js/device/iscroll.js",
                "~/content/js/device/jobaditem.js",
                "~/content/js/device/spinningwheel-min.js",
                "~/content/js/device/jquery.ui.touch-punch.min.js");

            //IScroll = AddScriptBundle(
            //    bundles,
            //    "~/bundle/js/iscroll",
            //    "~/content/js/device/iscroll.js");

            //SpinningWheel = AddScriptBundle(
            //    bundles,
            //    "~/bundle/js/spinningwheel",
            //    "~/content/js/device/spinningwheel-min.js");

            DeviceJobAd = AddScriptBundle(
                bundles,
                "~/bundle/js/devicejobad",
                "~/content/js/members/jobads.api.js",
                "~/content/js/device/jobad.js");

            DeviceLogin = AddScriptBundle(
                bundles,
                "~/bundle/js/devicelogin",
                "~/content/js/device/login.js");

            DeviceApply = AddScriptBundle(
                bundles,
                "~/bundle/js/deviceapply",
                "~/content/js/device/apply.js");

            DeviceApplied = AddScriptBundle(
                bundles,
                "~/bundle/js/deviceapplied",
                "~/content/js/device/applied.js");

            DeviceSavedSearches = AddScriptBundle(
                bundles,
                "~/bundle/js/devicesavedsearches",
                "~/content/js/device/savedsearches.js");

            DeviceSaveSearch = AddScriptBundle(
                bundles,
                "~/bundle/js/devicesavesearch",
                "~/content/js/device/savesearch.js");

            DeviceJobAdItem = AddScriptBundle(
                bundles,
                "~/bundle/js/devicejobaditem",
                "~/content/js/device/jobaditem.js");

            DeviceEmailJobAd = AddScriptBundle(
                bundles,
                "~/bundle/js/deviceemailjobad",
                "~/content/js/device/emailjobad.js");

            DeviceJoin = AddScriptBundle(
                bundles,
                "~/bundle/js/devicejoin",
                "~/content/js/device/join.js");

            // Employers.

            Employers = new EmployersBundleUrls
            {
                Orders = AddScriptBundle(
                    bundles,
                    "~/bundle/js/employers/orders",
                    JavaScripts.Employers.Orders.Order),

                JobAds = new EmployersJobAdsBundleUrls
                {
                    JobAd = AddScriptBundle(
                        bundles,
                        "~/bundle/js/employers/jobads/jobad",
                        JavaScripts.Employers.JobAds.JobAd),

                    Preview = AddScriptBundle(
                        bundles,
                        "~/bundle/js/employers/jobads/preview",
                        JavaScripts.Employers.JobAds.Preview,
                        JavaScripts.JobAds.JobAdListView),
                },

                Products = new EmployersProductsBundleUrls
                {
                    Choose = AddScriptBundle(
                        bundles,
                        "~/bundle/js/employers/products/choose",
                        JavaScripts.Employers.Products.Choose),
                },
            };

            // Members.

            Members = new MembersBundleUrls
            {
                Profiles = AddScriptBundle(
                    bundles,
                    "~/bundle/js/members/profiles",
                    JavaScripts.Members.Profiles.Status),
            };

            // Administrators.

            Administrators = AddScriptBundle(
                bundles,
                "~/bundle/js/administrators",
                JavaScripts.Administrators.Maintain,
                JavaScripts.Administrators.Credits);

            // Support.

            Support = new SupportBundleUrls
            {
                Faqs = AddScriptBundle(
                    bundles,
                    "~/bundle/js/support/faqs",
                    JavaScripts.Support.Faqs,
                    JavaScripts.Support.ContactUs),
            };
        }
    }
}