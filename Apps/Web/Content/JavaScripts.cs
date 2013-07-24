using System;
using System.Reflection;
using LinkMe.Apps.Asp.Content;
using LinkMe.Environment;

namespace LinkMe.Web.Content
{
    public static class JavaScripts
    {
        private static readonly Version Version = StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly());
        public static bool Minified = RuntimeEnvironment.Environment != ApplicationEnvironment.Dev;

        public static JavaScriptReference PlusOne = new JavaScriptReference("https://apis.google.com/js/plusone.js");
        public static JavaScriptReference SwfObject = new JavaScriptReference("https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js");

        public static JavaScriptReference TextResizeDetector = new JavaScriptReference(Version, Minified, "~/content/js/textresizedetector.js");
        public static JavaScriptReference HeightGroups = new JavaScriptReference(Version, Minified, "~/content/js/heightgroups.js");

        public static JavaScriptReference JQuery = new JavaScriptReference(Version, Minified, "~/content/js/jquery/jquery-1.4.1.js");
        public static JavaScriptReference JQuery162 = new JavaScriptReference(Version, Minified, "~/content/js/jquery/jquery-1.6.2.js");
        public static JavaScriptReference JQueryUiCore = new JavaScriptReference(Version, Minified, "~/content/js/jquery/jquery.ui.core.js");
        public static JavaScriptReference JQueryUiDatePicker = new JavaScriptReference(Version, Minified, "~/content/js/jquery/jquery.ui.datepicker.js");
        public static JavaScriptReference JQueryCarouselLite = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/jcarousellite_1.0.1c4.js");
        public static JavaScriptReference TextOverflow = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.text-overflow.js");
        public static JavaScriptReference JQem = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/jqem-compressed.js"); // JQuery-based text-resize detector
        public static JavaScriptReference AlignWith = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.alignwith.js");
        public static JavaScriptReference DesktopMenu = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.desktop-menu.js");
        public static JavaScriptReference CustomCheckbox = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.custom-checkbox.js");
        public static JavaScriptReference Download = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.download.js");
        public static JavaScriptReference JQueryCustom = new JavaScriptReference(Version, Minified, "~/content/js/jquery/linkme.jquery-ui-1.8.12.custom.js");
        public static JavaScriptReference Autocomplete = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.autocomplete.js");
        public static JavaScriptReference JsonManipulations = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/jquery.json2.js");
        public static JavaScriptReference Slider = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.range-selector.js");
        public static JavaScriptReference Tabs = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.tabs.js");
        public static JavaScriptReference ToggleCheckbox = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.toggle-checkbox.js");
        public static JavaScriptReference Tooltips = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.tooltips.js");
        public static JavaScriptReference CenterAlign = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/jquery.center-plugin.js");
        public static JavaScriptReference HoverIntent = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/jquery.hoverIntent.js");
        public static JavaScriptReference FileUpload = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.fileupload.js");
        public static JavaScriptReference FileUploadUi = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.fileupload-ui.js");
        public static JavaScriptReference JQueryFileUpload = new JavaScriptReference(Version, Minified, "~/content/js/jquery/jquery.fileupload.js");
        public static JavaScriptReference JQueryValidation = new JavaScriptReference(Version, Minified, "~/content/js/jquery/jquery.validate.js");

        public static JavaScriptReference JqueryTinyMceInit = new JavaScriptReference(Version, Minified, "~/content/js/jquery/plugins/linkme.tinymce.js");
        public static JavaScriptReference TinyMce = new JavaScriptReference(Version, false, "~/content/js/tinymce/jscripts/tiny_mce/tiny_mce.js");
        public static JavaScriptReference TinyMceSrc = new JavaScriptReference(Version, false, "~/content/js/tinymce/jscripts/tiny_mce/tiny_mce_src.js");
        public static JavaScriptReference JqueryTinyMce = new JavaScriptReference(Version, false, "~/content/js/tinymce/jscripts/tiny_mce/jquery.tinymce.js");

        public static JavaScriptReference FireBug = new JavaScriptReference(Version, Minified, "~/content/js/firebug/firebug.js");

        public static JavaScriptReference MicrosoftAjax = new JavaScriptReference(Version, Minified, "~/content/js/ajax/MicrosoftAjax.js");
        public static JavaScriptReference MicrosoftMvcAjax = new JavaScriptReference(Version, Minified, "~/content/js/ajax/MicrosoftMvcAjax.js");

        public static JavaScriptReference Social = new JavaScriptReference(Version, Minified, "~/content/js/social.js");
        public static JavaScriptReference VoteControl = new JavaScriptReference(Version, Minified, "~/content/js/votecontrol.js");

        public static JavaScriptReference Homepage = new JavaScriptReference(Version, Minified, "~/content/js/homepage.js");
        public static JavaScriptReference JoinFlow = new JavaScriptReference(Version, Minified, "~/content/js/joinflow.js");

        public static JavaScriptReference EApi = new JavaScriptReference(Version, Minified, "~/content/js/employers/api.js");

        public static JavaScriptReference EmployerHomepage = new JavaScriptReference(Version, Minified, "~/content/js/employers/homepage.js");
        public static JavaScriptReference CandidateConnect = new JavaScriptReference(Version, Minified, "~/content/js/employers/candidateconnect.js");
        public static JavaScriptReference SectionCollapsible = new JavaScriptReference(Version, Minified, "~/content/js/employers/SectionCollapsible.js");
        public static JavaScriptReference EmployersApi = new JavaScriptReference(Version, Minified, "~/content/js/employers/employersapi.js");
        public static JavaScriptReference Search = new JavaScriptReference(Version, Minified, "~/content/js/employers/search.js");
        public static JavaScriptReference Credits = new JavaScriptReference(Version, Minified, "~/content/js/employers/credits.js");
        public static JavaScriptReference Overlay = new JavaScriptReference(Version, Minified, "~/content/js/employers/overlay.js");
        public static JavaScriptReference Actions = new JavaScriptReference(Version, Minified, "~/content/js/employers/actions.js");
        public static JavaScriptReference Folders = new JavaScriptReference(Version, Minified, "~/content/js/employers/folders.js");
        public static JavaScriptReference FlagLists = new JavaScriptReference(Version, Minified, "~/content/js/employers/flaglists.js");
        public static JavaScriptReference BlockLists = new JavaScriptReference(Version, Minified, "~/content/js/employers/blocklists.js");
        public static JavaScriptReference CandidateResult = new JavaScriptReference(Version, Minified, "~/content/js/employers/candidate-results.js");
        public static JavaScriptReference CandidateResultLogin = new JavaScriptReference(Version, Minified, "~/content/js/employers/candidate-results-login.js"); //for employer who NOT logged in
        public static JavaScriptReference EmployersJobAds = new JavaScriptReference(Version, Minified, "~/content/js/employers/jobads.js");
        public static JavaScriptReference JobAd = new JavaScriptReference(Version, Minified, "~/content/js/members/jobad.js");
        public static JavaScriptReference Notes = new JavaScriptReference(Version, Minified, "~/content/js/employers/notes.js");
        public static JavaScriptReference ViewResume = new JavaScriptReference(Version, Minified, "~/content/js/employers/view-resume.js");

        public static JavaScriptReference Scriptaculous = new JavaScriptReference("~/js/scriptaculous.js");
        public static JavaScriptReference ScrollTracker = new JavaScriptReference("~/js/LinkMeUI/ScrollTracker.js");
        public static JavaScriptReference OverlayPopup = new JavaScriptReference("~/js/OverlayPopup.js");
        public static JavaScriptReference GroupMessagePopup = new JavaScriptReference("~/js/controls/GroupMessagePopup.js");
        public static JavaScriptReference PrototypeValidation = new JavaScriptReference("~/js/validation.js");
        public static JavaScriptReference OnChangeHandler = new JavaScriptReference("~/js/OnChangeHandler.js");
        public static JavaScriptReference LocateHelper = new JavaScriptReference("~/js/LinkMeUI/LocateHelper.js");
        public static JavaScriptReference SectionsEditor = new JavaScriptReference("~/js/LinkMeUI/SectionsEditor.js");
        public static JavaScriptReference StringUtils = new JavaScriptReference("~/js/LinkMeUI/StringUtils.js");
        public static JavaScriptReference VisibilitySettings = new JavaScriptReference("~/js/VisibilitySettings.js");
        public static JavaScriptReference FileView = new JavaScriptReference("~/js/controls/FileManager/FileView.js");
        public static JavaScriptReference AjaxHelper = new JavaScriptReference("~/js/AjaxHelper.js");
        public static JavaScriptReference Messaging = new JavaScriptReference("~/js/messaging/messaging.js");
        public static JavaScriptReference RadioButtonsField = new JavaScriptReference("~/ui/controls/fields/scripts/RadioButtonsField.js");
        public static JavaScriptReference FileManagerController = new JavaScriptReference("~/js/controls/FileManager/FileManagerController.js");
        public static JavaScriptReference BorderedPanel = new JavaScriptReference("~/js/controls/FileManager/BorderedPanel.js");
        public static JavaScriptReference UserContent = new JavaScriptReference("~/js/UserContent.js");
        public static JavaScriptReference FolderTree = new JavaScriptReference("~/js/controls/FileManager/FolderTree.js");
        public static JavaScriptReference Prototype = new JavaScriptReference("~/js/Prototype.js");
        public static JavaScriptReference CheckboxesHierarchy = new JavaScriptReference("~/js/LinkMeUI/behaviours/CheckboxesHierarchyBehaviour.js");
        public static JavaScriptReference MenuHierarchy = new JavaScriptReference("~/js/LinkMeUI/behaviours/MenuHierarchyBehaviour.js");
        public static JavaScriptReference TooltipBehaviour = new JavaScriptReference("~/js/LinkMeUI/behaviours/TooltipBehaviour.js");
        public static JavaScriptReference PrototypeHeightGroups = new JavaScriptReference("~/js/protoheightgroups.js");

        public static JavaScriptReference CandidateProfile = new JavaScriptReference(Version, Minified, "~/content/js/members/candidateprofile.js");

        public static JavaScriptReference Resources = new JavaScriptReference("~/content/js/resources/resources.js");

        public static JavaScriptReference Api = new JavaScriptReference(Version, Minified, "~/content/js/api.js");
        public static JavaScriptReference Validation = new JavaScriptReference(Version, Minified, "~/content/js/validation.js");
        public static JavaScriptReference Fields = new JavaScriptReference(Version, Minified, "~/content/js/fields.js");
        public static JavaScriptReference Checkboxes = new JavaScriptReference(Version, Minified, "~/content/js/checkboxes.js");
        public static JavaScriptReference OutsideEvents = new JavaScriptReference(Version, Minified, "~/content/js/jQuery/plugins/jquery.ba-outside-events.min.js");

        public static class JobAds
        {
            public static JavaScriptReference JobAdListView = new JavaScriptReference(Version, Minified, "~/content/js/jobads/jobadlistview.js");
        }

        public static class Members
        {
            public static class Profiles
            {
                public static JavaScriptReference Status = new JavaScriptReference(Version, Minified, "~/content/js/members/profiles/status.js");
            }
        }

        public static class Employers
        {
            public static JavaScriptReference Account = new JavaScriptReference(Version, Minified, "~/content/js/employers/account.js");

            public static class JobAds
            {
                public static JavaScriptReference JobAd = new JavaScriptReference(Version, Minified, "~/content/js/employers/jobads/jobad.js");
                public static JavaScriptReference Preview = new JavaScriptReference(Version, Minified, "~/content/js/employers/jobads/preview.js");
            }

            public static class Products
            {
                public static JavaScriptReference Choose = new JavaScriptReference(Version, Minified, "~/content/js/employers/products/choose.js");
            }

            public static class Orders
            {
                public static JavaScriptReference Order = new JavaScriptReference(Version, Minified, "~/content/js/employers/orders/order.js");
            }
        }

        public static class Administrators
        {
            public static JavaScriptReference Maintain = new JavaScriptReference(Version, Minified, "~/content/js/administrators/maintain.js");
            public static JavaScriptReference Credits = new JavaScriptReference(Version, Minified, "~/content/js/administrators/credits.js");
        }

        public static class Support
        {
            public static JavaScriptReference Faqs = new JavaScriptReference(Version, Minified, "~/content/js/support/faqs.js");
            public static JavaScriptReference ContactUs = new JavaScriptReference(Version, Minified, "~/content/js/support/contactus.js");
        }
    }
}
