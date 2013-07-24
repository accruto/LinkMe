using System.Web.Optimization;
using LinkMe.Apps.Asp.Content;

namespace LinkMe.Web.Content
{
    public class StyleBundles
        : Bundles
    {
        public static BundleUrl Site1 { get; private set; }
        public static BundleUrl Site2 { get; private set; }
        public static BundleUrl Resources { get; private set; }
        public static BundleUrl HeaderAndNav { get; private set; }
        public static BundleUrl JQueryCustom { get; private set; }
        public static BundleUrl Home { get; private set; }
        public static BundleUrl JQueryAutocomplete { get; private set; }
        public static BundleUrl Join { get; private set; }
        public static BundleUrl CandidateProfile { get; private set; }
        public static BundleUrl JobAds { get; private set; }
        public static BundleUrl EmployerHome { get; private set; }
        public static BundleUrl CandidateConnect { get; private set; }
        public static BundleUrl JobSearch { get; private set; }
        public static BundleUrl JobSearchResults { get; private set; }
        public static BundleUrl RecentJobSearches { get; private set; }
        public static BundleUrl Devices { get; private set; }
        public static BundleUrl DeviceSearch { get; private set; }
        public static BundleUrl DeviceResults { get; private set; }
        public static BundleUrl DeviceJobAd { get; private set; }
        public static BundleUrl DeviceLogin { get; private set; }
        public static BundleUrl DeviceApply { get; private set; }
        public static BundleUrl DeviceApplied { get; private set; }
        public static BundleUrl DeviceRecentSearches { get; private set; }
        public static BundleUrl DeviceMySearches { get; private set; }
        public static BundleUrl DeviceSavedSearches { get; private set; }
        public static BundleUrl DeviceSaveSearch { get; private set; }
        public static BundleUrl DeviceMyJobs { get; private set; }
        public static BundleUrl DeviceMobileFolder { get; private set; }
        public static BundleUrl DeviceEmailJobAd { get; private set; }
        public static BundleUrl DeviceJoin { get; private set; }

        public class MembersBundleUrls
        {
            public BundleUrl Profiles { get; set; }
        }

        public class EmployersJobAdsBundleUrls
        {
            public BundleUrl JobAd { get; set; }
            public BundleUrl Preview { get; set; }
        }

        public class EmployersProductsBundleUrls
        {
            public BundleUrl Choose { get; set; }
            public BundleUrl NewOrder { get; set; }
        }

        public class EmployersBundleUrls
        {
            public BundleUrl Page { get; set; }
            public BundleUrl Orders { get; set; }
            public EmployersJobAdsBundleUrls JobAds { get; set; }
            public EmployersProductsBundleUrls Products { get; set; }
        }

        public class AdministratorsBundleUrls
        {
            public BundleUrl Page { get; set; }
        }

        public class SupportBundleUrls
        {
            public BundleUrl Faqs { get; set; }
        }

        public class BundleUrls
        {
            public BundleUrl Page { get; set; }
            public BundleUrl Elements { get; set; }
            public BundleUrl Forms { get; set; }
            public MembersBundleUrls Members { get; set; }
            public EmployersBundleUrls Employers { get; set; }
            public AdministratorsBundleUrls Administrators { get; set; }
            public SupportBundleUrls Support { get; set; }
        }

        public static BundleUrls Block { get; private set; }

        public static void RegisterBundles(BundleCollection bundles)
        {
            Site1 = AddStyleBundle(
                bundles,
                "~/bundle/css/site1",
                "~/content/css/universal-layout.css",
                "~/content/css/text-links-headings.css",
                "~/content/css/widgets-and-lists.css",
                "~/content/css/forms.css",
                "~/content/css/common/fields.css");

            Site2 = AddStyleBundle(
                bundles,
                "~/bundle/css/site2",
                "~/content/css/sidebar-on-left.css",
                "~/content/css/forms2.css",
                "~/content/css/common/buttons.css");

            Resources = AddStyleBundle(
                bundles,
                "~/bundle/css/resources",
                "~/content/css/Resources/resources.css",
                "~/content/css/Resources/VoteControl.css",
                "~/content/css/employers/pagination.css");

            HeaderAndNav = AddStyleBundle(
                bundles,
                "~/bundle/css/headerandnav",
                "~/content/css/header-and-nav.css");

            JQueryCustom = AddStyleBundle(
                bundles,
                "~/bundle/css/jquerycustom",
                "~/content/css/jquery/linkme.jquery-ui.custom.css");

            Home = AddStyleBundle(
                bundles,
                "~/bundle/css/home",
                "~/content/css/homepage.css",
                "~/content/css/jquery/plugins/linkme.autocomplete.css");

            JQueryAutocomplete = AddStyleBundle(
                bundles,
                "~/bundle/css/jqueryautocomplete",
                "~/content/css/jquery/plugins/linkme.autocomplete.css");

            Join = AddStyleBundle(
                bundles,
                "~/bundle/css/join",
                "~/content/css/joinflow.css");

            CandidateProfile = AddStyleBundle(
                bundles,
                "~/bundle/css/candidateprofile",
                "~/content/css/candidateprofile.css");

            JobAds = AddStyleBundle(
                bundles,
                "~/bundle/css/jobads",
                "~/content/css/jobad.css");

            EmployerHome = AddStyleBundle(
                bundles,
                "~/bundle/css/employerhome",
                "~/content/css/employers/homepage.css",
                "~/content/css/jquery/plugins/linkme.autocomplete.css",
                "~/content/css/overlay.css",
                "~/content/css/error.css");

            CandidateConnect = AddStyleBundle(
                bundles,
                "~/bundle/css/candidateconnect",
                "~/content/css/employers/candidateconnect.css");

            JobSearch = AddStyleBundle(
                bundles,
                "~/bundle/css/jobsearch",
                "~/content/css/members/jobsearch/search.css",
                "~/content/css/jquery/plugins/linkme.autocomplete.css");

            JobSearchResults = AddStyleBundle(
                bundles,
                "~/bundle/css/jobsearchresults",
                "~/content/css/jquery/linkme.jquery-ui.custom.css",
                "~/content/css/members/jobsearch/result.css",
                "~/content/css/jquery/plugins/linkme.autocomplete.css",
                "~/content/css/employers/pagination.css");

            RecentJobSearches = AddStyleBundle(
                bundles,
                "~/bundle/css/recentjobsearches",
                "~/content/css/jquery/linkme.jquery-ui.custom.css",
                "~/content/css/members/jobsearch/recentsearch.css",
                "~/content/css/employers/pagination.css");

            Devices = AddStyleBundle(
                bundles,
                "~/bundle/css/devices",
                "~/content/css/device/ggs.css",
                "~/content/css/device/common.css",
                "~/content/css/device/fields.css");

            DeviceSearch = AddStyleBundle(
                bundles,
                "~/bundle/css/devicesearch",
                "~/content/css/device/search.css");

            DeviceResults = AddStyleBundle(
                bundles,
                "~/bundle/css/deviceresults",
                "~/content/css/device/results.css",
                "~/content/css/device/jobaditem.css",
                "~/content/css/device/spinningwheel.css");

            DeviceJobAd = AddStyleBundle(
                bundles,
                "~/bundle/css/devicejobad",
                "~/content/css/device/jobad.css");

            DeviceLogin = AddStyleBundle(
                bundles,
                "~/bundle/css/devicelogin",
                "~/content/css/device/login.css");

            DeviceApply = AddStyleBundle(
                bundles,
                "~/bundle/css/deviceapply",
                "~/content/css/device/apply.css");

            DeviceApplied = AddStyleBundle(
                bundles,
                "~/bundle/css/deviceapplied",
                "~/content/css/device/applied.css");

            DeviceRecentSearches = AddStyleBundle(
                bundles,
                "~/bundle/css/devicerecentsearches",
                "~/content/css/device/recentsearches.css");

            DeviceMySearches = AddStyleBundle(
                bundles,
                "~/bundle/css/devicemysearches",
                "~/content/css/device/mysearches.css");

            DeviceSavedSearches = AddStyleBundle(
                bundles,
                "~/bundle/css/devicesavedsearches",
                "~/content/css/device/savedsearches.css");

            DeviceSaveSearch = AddStyleBundle(
                bundles,
                "~/bundle/css/devicesavesearch",
                "~/content/css/device/savesearch.css");

            DeviceMyJobs = AddStyleBundle(
                bundles,
                "~/bundle/css/devicemyjobs",
                "~/content/css/device/myjobs.css");

            DeviceMobileFolder = AddStyleBundle(
                bundles,
                "~/bundle/css/devicemobilefolder",
                "~/content/css/device/mobilefolder.css",
                "~/content/css/device/jobaditem.css");

            DeviceEmailJobAd = AddStyleBundle(
                bundles,
                "~/bundle/css/deviceemailjobad",
                "~/content/css/device/emailjobad.css");

            DeviceJoin = AddStyleBundle(
                bundles,
                "~/bundle/css/devicejoin",
                "~/content/css/device/join.css");

            RegisterBlockBundles(bundles);
        }

        private static void RegisterBlockBundles(BundleCollection bundles)
        {
            Block = new BundleUrls
            {
                // Page.

                Page = AddStyleBundle(
                    bundles,
                    "~/bundle/block/css/page",
                    StyleSheets.Block.Page,
                    StyleSheets.Block.Header,
                    StyleSheets.Block.Footer,
                    StyleSheets.Block.Validation),

                Elements = AddStyleBundle(
                    bundles,
                    "~/bundle/block/css/page/elements",
                    StyleSheets.Block.Sections,
                    StyleSheets.Block.Lists,
                    StyleSheets.Block.Actions,
                    StyleSheets.Block.Checkboxes),

                // Forms.

                Forms = AddStyleBundle(
                    bundles,
                    "~/bundle/block/css/forms",
                    StyleSheets.Block.Forms,
                    StyleSheets.Block.Inputs,
                    StyleSheets.Block.Fields),

                // Members.

                Members = new MembersBundleUrls
                {
                    Profiles = AddStyleBundle(
                        bundles,
                        "~/bundle/block/css/members/profiles",
                        StyleSheets.Block.Members.Profiles.Status),
                },

                // Employers.

                Employers = new EmployersBundleUrls
                {
                    Page = AddStyleBundle(
                        bundles,
                        "~/bundle/block/css/employers",
                        StyleSheets.Block.Employers.Page),

                    Orders = AddStyleBundle(
                        bundles,
                        "~/bundle/block/css/employers/orders",
                        StyleSheets.Block.Employers.Products.Orders,
                        StyleSheets.Block.Employers.Products.Receipts),

                    JobAds = new EmployersJobAdsBundleUrls
                    {
                        JobAd = AddStyleBundle(
                            bundles,
                            "~/bundle/block/css/employers/jobads/jobad",
                            StyleSheets.Block.Employers.JobAds.JobAd),

                        Preview = AddStyleBundle(
                            bundles,
                            "~/bundle/block/css/employers/jobads/preview",
                            StyleSheets.Block.Employers.JobAds.JobAd,
                            StyleSheets.Block.Employers.JobAds.Preview,
                            StyleSheets.Block.JobAds.JobAdView,
                            StyleSheets.Block.JobAds.JobAdListView),
                    },

                    Products = new EmployersProductsBundleUrls
                    {
                        Choose = AddStyleBundle(
                            bundles,
                            "~/bundle/block/css/employers/products/choose",
                            StyleSheets.Block.Employers.Products.WizardSteps,
                            StyleSheets.Block.Employers.Products.Choose),

                        NewOrder = AddStyleBundle(
                            bundles,
                            "~/bundle/block/css/employers/products/account",
                            StyleSheets.Block.Employers.Products.WizardSteps),
                    },
                },

                // Administrators.

                Administrators = new AdministratorsBundleUrls
                {
                    Page = AddStyleBundle(
                        bundles,
                        "~/bundle/block/css/administrators",
                        StyleSheets.Block.Administrators.Search,
                        StyleSheets.Block.Administrators.Credits),
                },

                // Support.

                Support = new SupportBundleUrls
                {
                    Faqs = AddStyleBundle(
                        bundles,
                        "~/bundle/block/css/support/faqs",
                        StyleSheets.Block.Support.Faqs,
                        StyleSheets.Block.Support.ContactUs),
                }
            };
        }
    }
}