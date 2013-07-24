using System;
using System.Reflection;
using LinkMe.Apps.Asp.Content;
using LinkMe.Environment;

namespace LinkMe.Web.Content
{
    public static class StyleSheets
    {
        private static readonly Version Version = StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly());
        public static bool Minified = RuntimeEnvironment.Environment != ApplicationEnvironment.Dev;

        public static StyleSheetReference UniversalLayout = new StyleSheetReference(Version, Minified, "~/content/css/universal-layout.css");
        public static StyleSheetReference TextLinksHeadings = new StyleSheetReference(Version, Minified, "~/content/css/text-links-headings.css");
        public static StyleSheetReference WidgetsAndLists = new StyleSheetReference(Version, Minified, "~/content/css/widgets-and-lists.css");
        public static StyleSheetReference SidebarOnLeft = new StyleSheetReference(Version, Minified, "~/content/css/sidebar-on-left.css");
        public static StyleSheetReference Forms = new StyleSheetReference(Version, Minified, "~/content/css/forms.css");
        public static StyleSheetReference Forms2 = new StyleSheetReference(Version, Minified, "~/content/css/forms2.css");
        public static StyleSheetReference Homepage = new StyleSheetReference(Version, Minified, "~/content/css/homepage.css");
        public static StyleSheetReference Fonts = new StyleSheetReference(Version, Minified, "~/content/css/fonts/fonts.css");
        public static StyleSheetReference HeaderAndNav = new StyleSheetReference(Version, Minified, "~/content/css/header-and-nav.css");
        public static StyleSheetReference Error = new StyleSheetReference(Version, Minified, "~/content/css/error.css");
        public static StyleSheetReference Overlay = new StyleSheetReference(Version, Minified, "~/content/css/overlay.css");
        public static StyleSheetReference SidebarAbsent = new StyleSheetReference(Version, Minified, "~/content/css/sidebar-absent.css");
        public static StyleSheetReference Support = new StyleSheetReference(Version, Minified, "~/content/css/support.css");
        public static StyleSheetReference OrdersAndReceipts = new StyleSheetReference(Version, Minified, "~/content/css/ordersandreceipts.css");
        public static StyleSheetReference JobAd = new StyleSheetReference(Version, Minified, "~/content/css/jobad.css");

        public static StyleSheetReference LoginOrJoin = new StyleSheetReference(Version, Minified, "~/content/css/controls/login-or-join.css");
        public static StyleSheetReference WizardSteps = new StyleSheetReference(Version, Minified, "~/content/css/controls/wizardsteps.css");

        public static StyleSheetReference Employer = new StyleSheetReference(Version, Minified, "~/content/css/employers/employer.css");
        public static StyleSheetReference EmployerFrontPage = new StyleSheetReference(Version, Minified, "~/content/css/employers/employer-front-page.css");
        public static StyleSheetReference EmployerLoggedInFrontPage = new StyleSheetReference(Version, Minified, "~/content/css/employers/employer-logged-in-front-page.css");
        public static StyleSheetReference Folders = new StyleSheetReference(Version, Minified, "~/content/css/employers/folders.css");
        public static StyleSheetReference FlagLists = new StyleSheetReference(Version, Minified, "~/content/css/employers/flaglists.css");
        public static StyleSheetReference BlockLists = new StyleSheetReference(Version, Minified, "~/content/css/employers/blocklists.css");
        public static StyleSheetReference JobAds = new StyleSheetReference(Version, Minified, "~/content/css/employers/jobads.css");
        public static StyleSheetReference Search = new StyleSheetReference(Version, Minified, "~/content/css/employers/search.css");
        public static StyleSheetReference SearchResults = new StyleSheetReference(Version, Minified, "~/content/css/employers/search-results.css");
        public static StyleSheetReference GuestEmployerHome = new StyleSheetReference(Version, Minified, "~/content/css/employers/guest-employer-home.css");
        public static StyleSheetReference Pagination = new StyleSheetReference(Version, Minified, "~/content/css/employers/pagination.css");
        public static StyleSheetReference Notes = new StyleSheetReference(Version, Minified, "~/content/css/employers/notes.css");
        public static StyleSheetReference ViewResume = new StyleSheetReference(Version, Minified, "~/content/css/employers/view-resume.css");
        public static StyleSheetReference ResumeDetail = new StyleSheetReference(Version, Minified, "~/content/css/employers/resume-detail.css"); //for ResumeDetail.aspx only
        public static StyleSheetReference Settings = new StyleSheetReference(Version, Minified, "~/content/css/employers/settings.css");

        public static StyleSheetReference JQueryUiAll = new StyleSheetReference(Version, Minified, "~/content/css/jquery/jquery.ui.all.css");
        public static StyleSheetReference JQueryAutocomplete = new StyleSheetReference(Version, Minified, "~/content/css/jquery/plugins/linkme.autocomplete.css");
        public static StyleSheetReference JQueryCustom = new StyleSheetReference(Version, Minified, "~/content/css/jquery/linkme.jquery-ui.custom.css");
        public static StyleSheetReference JQueryWidgets = new StyleSheetReference(Version, Minified, "~/content/css/jquery/plugins/linkme.widgets.css");
        public static StyleSheetReference JQuerySlider = new StyleSheetReference(Version, Minified, "~/content/css/jquery/plugins/linkme.slider.css");
        public static StyleSheetReference JQueryTabs = new StyleSheetReference(Version, Minified, "~/content/css/jquery/plugins/linkme.jquery.tabs.css");
        public static StyleSheetReference JQueryDragDrop = new StyleSheetReference(Version, Minified, "~/content/css/jquery/plugins/linkme.dragdrop.css");
        public static StyleSheetReference JQueryFileUploadUi = new StyleSheetReference(Version, Minified, "~/content/css/jquery/plugins/linkme.fileupload-ui.css");

        public static StyleSheetReference Sidebar = new StyleSheetReference("~/ui/css/sidebar.css");
        public static StyleSheetReference ExampleResume = new StyleSheetReference("~/ui/styles/exampleResume.css");
        public static StyleSheetReference Jobs = new StyleSheetReference("~/ui/css/jobs.css");
        public static StyleSheetReference JobsHome = new StyleSheetReference("~/ui/css/jobshome.css");
        public static StyleSheetReference Networking = new StyleSheetReference("~/ui/css/networking.css");
        public static StyleSheetReference CandidateFolders = new StyleSheetReference("~/ui/css/candidate-folders.css");
        public static StyleSheetReference Resume = new StyleSheetReference("~/ui/css/resume.css");
        public static StyleSheetReference Groups = new StyleSheetReference("~/ui/css/groups.css");
        public static StyleSheetReference Records = new StyleSheetReference("~/ui/css/records.css");
        public static StyleSheetReference Discussions = new StyleSheetReference("~/ui/css/discussions.css");
        public static StyleSheetReference Organisations = new StyleSheetReference("~/ui/css/organisations.css");
        public static StyleSheetReference WizardLocus = new StyleSheetReference("~/ui/styles/wizardLocus.css");
        public static StyleSheetReference Communities = new StyleSheetReference("~/Content/css/communities.css");
        public static StyleSheetReference Messaging = new StyleSheetReference("~/ui/css/messaging.css");
        public static StyleSheetReference LandingSample = new StyleSheetReference("~/landing/css/sample.css");
        public static StyleSheetReference Landing = new StyleSheetReference("~/landing/css/landing.css");
        public static StyleSheetReference CheckBoxMenu = new StyleSheetReference("~/ui/css/controls/checkboxmenu.css");

        public static StyleSheetReference Scal = new StyleSheetReference("~/ui/css/scal.css");
        public static StyleSheetReference ResumePreview = new StyleSheetReference("~/ui/css/ResumePreview.css");
        public static StyleSheetReference ResumePrint = new StyleSheetReference("~/ui/css/ResumePrint.css", "print");
        public static StyleSheetReference ResumeEditor = new StyleSheetReference("~/ui/css/ResumeEditor.css");
        public static StyleSheetReference FrontPage = new StyleSheetReference("~/ui/css/front-page.css");
        public static StyleSheetReference HomePages = new StyleSheetReference("~/ui/css/home-pages.css");
        public static StyleSheetReference ResumeEditMode = new StyleSheetReference("~/ui/css/ResumeEditMode.css");

        public static StyleSheetReference VoteControl = new StyleSheetReference(Version, Minified, "~/content/css/resources/votecontrol.css");
        public static StyleSheetReference Button = new StyleSheetReference(Version, Minified, "~/content/css/common/buttons.css");

        // MF (2009-02-04):

        // Per-control stylesheets
        // (as we find groups of controls which are often used together, merge those stylesheets together and
        //  point the respective stylesheet references to the same sheet.)

        public static StyleSheetReference Whiteboard = new StyleSheetReference("~/ui/css/controls/whiteboard.css");
        public static StyleSheetReference JobSearchControl = new StyleSheetReference("~/ui/css/controls/jobsearchcontrol.css");
        public static StyleSheetReference LocationConfirmation = new StyleSheetReference("~ui/css/controls/locationconfirmation.css");
        public static StyleSheetReference ReportDates = new StyleSheetReference("~/ui/css/controls/reportdates.css");
        public static StyleSheetReference UploadPhoto = new StyleSheetReference("~/ui/css/controls/upload-photo.css");
        public static StyleSheetReference CandidateNotes = new StyleSheetReference("~/ui/css/controls/candidatenotes.css");
        public static StyleSheetReference WillingnessToRelocate = new StyleSheetReference("~/ui/css/controls/willingness-to-relocate.css");
        public static StyleSheetReference Login = new StyleSheetReference("~/ui/css/controls/login.css");
        public static StyleSheetReference FilesMiniList = new StyleSheetReference("~/ui/css/controls/filesminilist.css");
        public static StyleSheetReference FilesList = new StyleSheetReference("~/ui/css/controls/fileslist.css");
        public static StyleSheetReference ProfileThumbnail = new StyleSheetReference("~/ui/css/controls/profilethumbnail.css");
        public static StyleSheetReference BrowseJobAds = new StyleSheetReference("~/ui/css/controls/browsejobads.css");
        public static StyleSheetReference SortOrder = new StyleSheetReference("~/ui/css/controls/sortorder.css");
        public static StyleSheetReference JobSearchResults = new StyleSheetReference("~/ui/css/controls/jobsearchresults.css");
        public static StyleSheetReference FolderSearchForm = new StyleSheetReference("~/ui/css/controls/foldersearchform.css");
        public static StyleSheetReference SavedResumeSearches = new StyleSheetReference("~/ui/css/controls/savedresumesearches.css");
        public static StyleSheetReference SimpleSearchForm = new StyleSheetReference("~/ui/css/controls/simplesearchform.css");

        public static class Block
        {
            public static StyleSheetReference Page = new StyleSheetReference(Version, Minified, "~/content/block/css/page.css");
            public static StyleSheetReference Header = new StyleSheetReference(Version, Minified, "~/content/block/css/header.css");
            public static StyleSheetReference Footer = new StyleSheetReference(Version, Minified, "~/content/block/css/footer.css");
            public static StyleSheetReference Validation = new StyleSheetReference(Version, Minified, "~/content/block/css/validation.css");
            public static StyleSheetReference Sections = new StyleSheetReference(Version, Minified, "~/content/block/css/sections.css");
            public static StyleSheetReference Lists = new StyleSheetReference(Version, Minified, "~/content/block/css/lists.css");
            public static StyleSheetReference Actions = new StyleSheetReference(Version, Minified, "~/content/block/css/actions.css");
            public static StyleSheetReference Forms = new StyleSheetReference(Version, Minified, "~/content/block/css/forms.css");
            public static StyleSheetReference Inputs = new StyleSheetReference(Version, Minified, "~/content/block/css/inputs.css");
            public static StyleSheetReference Fields = new StyleSheetReference(Version, Minified, "~/content/block/css/fields.css");
            public static StyleSheetReference Checkboxes = new StyleSheetReference(Version, Minified, "~/content/block/css/checkboxes.css");

            public static StyleSheetReference NoLeftSideBar = new StyleSheetReference(Version, Minified, "~/content/block/css/noleftsidebar.css");
            public static StyleSheetReference LeftSideBar = new StyleSheetReference(Version, Minified, "~/content/block/css/leftsidebar.css");

            public static class Members
            {
                public static class Profiles
                {
                    public static StyleSheetReference Status = new StyleSheetReference(Version, Minified, "~/content/block/css/members/profiles/status.css");
                }
            }

            public static class Employers
            {
                public static StyleSheetReference Page = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/page.css");
                public static StyleSheetReference Account = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/account.css");

                public static class JobAds
                {
                    public static StyleSheetReference JobAd = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/jobads/jobad.css");
                    public static StyleSheetReference Preview = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/jobads/preview.css");
                    public static StyleSheetReference Account = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/jobads/account.css");
                }

                public static class Products
                {
                    public static StyleSheetReference Orders = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/products/orders.css");
                    public static StyleSheetReference Receipts = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/products/receipts.css");
                    public static StyleSheetReference WizardSteps = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/products/wizardsteps.css");
                    public static StyleSheetReference Choose = new StyleSheetReference(Version, Minified, "~/content/block/css/employers/products/choose.css");
                }
            }

            public static class Administrators
            {
                public static StyleSheetReference Search = new StyleSheetReference(Version, Minified, "~/content/block/css/administrators/search.css");
                public static StyleSheetReference Credits = new StyleSheetReference(Version, Minified, "~/content/block/css/administrators/credits.css");
            }

            public static class Accounts
            {
                public static StyleSheetReference Passwords = new StyleSheetReference(Version, Minified, "~/content/block/css/accounts/passwords.css");
            }

            public static class JobAds
            {
                public static StyleSheetReference JobAdView = new StyleSheetReference(Version, Minified, "~/content/block/css/jobads/jobadview.css");
                public static StyleSheetReference JobAdListView = new StyleSheetReference(Version, Minified, "~/content/block/css/jobads/jobadlistview.css");
            }

            public static class Support
            {
                public static StyleSheetReference Faqs = new StyleSheetReference(Version, Minified, "~/content/block/css/support/faqs.css");
                public static StyleSheetReference ContactUs = new StyleSheetReference(Version, Minified, "~/content/block/css/support/contactus.css");
            }
        }
    }
}
