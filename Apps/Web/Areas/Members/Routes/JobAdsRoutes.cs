using System;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Query.Search.JobAds;
using LinkMe.Web.Areas.Members.Controllers.JobAds;
using LinkMe.Web.Areas.Members.Controllers.Search;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;

namespace LinkMe.Web.Areas.Members.Routes
{
    public class JobAdsRoutes
    {
        public const string SegmentSuffix = "jobs";

        public static RouteReference BrowseJobAds { get; private set; }
        public static RouteReference IndustryJobAds { get; private set; }
        public static RouteReference LocationJobAds { get; private set; }
        public static RouteReference LocationIndustryJobAds { get; private set; }
        public static RouteReference PagedLocationIndustryJobAds { get; private set; }

        public static RouteReference Jobs { get; private set; }
        public static RouteReference JobAd { get; private set; }
        public static RouteReference JobAdApplied { get; private set; }
        public static RouteReference JobAdQuestions { get; private set; }
        public static RouteReference Download { get; private set; }
        public static RouteReference Logo { get; private set; }

        public static RouteReference ApiEmailJobAds { get; private set; }
        public static RouteReference EmailJobAd { get; private set; }
        public static RouteReference EmailJobAdSent { get; private set; }

        public static RouteReference ApiExternallyApplied { get; private set; }
        public static RouteReference ApiViewed { get; private set; }

        public static RouteReference Applications { get; private set; }

        public static RouteReference ApiApplyWithLastUsedResume { get; private set; }
        public static RouteReference ApiApplyWithUploadedResume { get; private set; }
        public static RouteReference ApiApplyWithProfile { get; private set; }
        public static RouteReference ApiApply { get; private set; }

        public static RouteReference Similar { get; private set; }
        public static RouteReference SimilarPartial { get; private set; }

        public static RouteReference Suggested { get; private set; }
        public static RouteReference SuggestedPartial { get; private set; }

        public static RouteReference Folder { get; private set; }
        public static RouteReference PartialFolder { get; private set; }
        public static RouteReference MobileFolder { get; private set; }
        public static RouteReference PartialMobileFolder { get; private set; }

        public static RouteReference ApiAddJobAdsToFolder { get; private set; }
        public static RouteReference ApiAddJobAdsToMobileFolder { get; private set; }
        public static RouteReference ApiRemoveJobAdsFromFolder { get; private set; }
        public static RouteReference ApiRemoveJobAdsFromMobileFolder { get; private set; }
        public static RouteReference ApiEmptyFolder { get; private set; }
        public static RouteReference ApiRenameFolder { get; private set; }

        public static RouteReference FlagList { get; private set; }
        public static RouteReference PartialFlagList { get; private set; }

        public static RouteReference ApiFlagJobAds { get; private set; }
        public static RouteReference ApiUnflagJobAds { get; private set; }
        public static RouteReference ApiUnflagAllJobAds { get; private set; }
        public static RouteReference ApiUnflagCurrentJobAds { get; private set; }

        public static RouteReference BlockList { get; private set; }
        public static RouteReference PartialBlockList { get; private set; }

        public static RouteReference ApiBlockJobAds { get; private set; }
        public static RouteReference ApiUnblockJobAds { get; private set; }
        public static RouteReference ApiUnblockAllJobAds { get; private set; }

        public static RouteReference ApiEditNote { get; private set; }
        public static RouteReference ApiDeleteNote { get; private set; }
        public static RouteReference ApiNotes { get; private set; }
        public static RouteReference ApiNewNote { get; private set; }

        public static RouteReference LoggedInUserApplyArea { get; private set; }
        public static RouteReference RedirectToExternal { get; private set; }

        public static RouteReference AddJobAdToMobileFolder { get; private set; }
        public static RouteReference JobAdApply { get; private set; }

        private static RouteReference OldJobAd { get; set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            // These are all old job ad urls that need to map to the job ad page.

            OldJobAd = context.MapAreaRoute<BrowseJobAdsController, Guid?>("jobs/{jobAdId}", c => c.OldJobAdId);
            context.MapAreaRoute<BrowseJobAdsController, Guid?>(false, "jobads/{jobAdId}", c => c.OldJobAdId);
            context.MapAreaRoute<BrowseJobAdsController, Guid?>(false, "jobads/{jobAdId}/apply", c => c.OldJobAdId);
            context.MapAreaRoute<BrowseJobAdsController, Guid?>(false, "jobs/Job.aspx", c => c.OldJobAdId);
            context.MapAreaRoute<BrowseJobAdsController, Guid?>(false, "ui/unregistered/common/JobApplicationSignInForm.aspx", c => c.OldJobAdId);
            context.MapAreaRoute<BrowseJobAdsController, Guid?>(false, "ui/unregistered/ViewJobAdForm.aspx", c => c.OldJobAdId);

            JobAd = context.MapAreaRoute<JobAdsWebController, Guid>("jobs/{locationSegment}/{industrySegment}/{titleSegment}/{jobAdId}", c => c.JobAd);
            BrowseJobAds = context.MapAreaRoute<BrowseJobAdsController>("jobs", c => c.JobAds);
            IndustryJobAds = context.MapAreaRoute<BrowseJobAdsController, string>("jobs/-/{industrySegment}", c => c.IndustryJobAds);
            LocationJobAds = context.MapAreaRoute<BrowseJobAdsController, string>("jobs/{locationSegment}", c => c.LocationJobAds);

            Download = context.MapAreaRoute<JobAdFilesController, JobAdMimeType?, Guid[]>("members/jobs/download", c => c.Download);
            Logo = context.MapAreaRoute<JobAdFilesController, Guid>("members/jobs/{jobAdId}/logo", c => c.Logo);

            LocationIndustryJobAds = context.MapAreaRoute<SearchController, string, string>("jobs/{locationSegment}/{industrySegment}", c => c.LocationIndustryJobAds);
            PagedLocationIndustryJobAds = context.MapAreaRoute<SearchController, string, string, int?>("jobs/{locationSegment}/{industrySegment}/{page}", c => c.PagedLocationIndustryJobAds);

            Jobs = context.MapAreaRoute<JobAdsMobileController>("members/jobs", c => c.Jobs);
            context.MapRedirectRoute("members/jobs", SearchRoutes.Search);

            JobAdQuestions = context.MapAreaRoute<JobAdsWebController, Guid, Guid>("members/jobs/{jobAdId}/questions", c => c.JobAdQuestions);
            JobAdApplied = context.MapAreaRoute<JobAdsWebController, Guid>("members/jobs/{jobAdId}/applied", c => c.JobAdApplied);

            ApiEmailJobAds = context.MapAreaRoute<JobAdsApiController, EmailJobAdsModel>("members/jobs/api/email", c => c.EmailJobAds);
            EmailJobAd = context.MapAreaRoute<JobAdsMobileController, Guid>("members/jobs/{jobAdId}/email", c => c.Email);
            context.MapRedirectRoute("members/jobs/{jobAdId}/email", OldJobAd, new { jobAdId = new RedirectRouteValue() });
            EmailJobAdSent = context.MapAreaRoute<JobAdsMobileController, Guid>("members/jobs/{jobAdId}/emailsent", c => c.EmailSent);
            context.MapRedirectRoute("members/jobs/{jobAdId}/emailsent", OldJobAd, new { jobAdId = new RedirectRouteValue() });

            LoggedInUserApplyArea = context.MapAreaRoute<JobAdsWebController, Guid>("members/jobs/{jobAdId}/loggedinuserapplyarea", c => c.LoggedInUserApplyArea);
            RedirectToExternal = context.MapAreaRoute<JobAdsWebController, Guid, Guid?>("members/jobs/{jobAdId}/redirecttoexternal", c => c.RedirectToExternal);
            ApiApplyWithLastUsedResume = context.MapAreaRoute<ApplicationsApiController, Guid, string>("members/jobs/api/{jobAdId}/applywithlastusedresume", c => c.ApplyWithLastUsedResume);
            ApiApplyWithUploadedResume = context.MapAreaRoute<ApplicationsApiController, Guid, Guid, bool, string>("members/jobs/api/{jobAdId}/applywithuploadedresume", c => c.ApplyWithUploadedResume);
            ApiApplyWithProfile = context.MapAreaRoute<ApplicationsApiController, Guid, string>("members/jobs/api/{jobAdId}/applywithprofile", c => c.ApplyWithProfile);
            ApiApply = context.MapAreaRoute<ApplicationsApiController, Guid, Guid, ContactDetailsModel>("members/jobs/api/{jobAdId}/apply", c => c.Apply);

            JobAdApply = context.MapAreaRoute<JobAdsMobileController, Guid>("members/jobs/{jobAdId}/apply", c => c.Apply);
            context.MapRedirectRoute("members/jobs/{jobAdId}/apply", OldJobAd, new { jobAdId = new RedirectRouteValue() });

            // Applications.

            Applications = context.MapAreaRoute<ApplicationsMobileController>("members/jobs/applications", c => c.Applications);
            context.MapRedirectUrl("members/jobs/applications", "ui/registered/networkers/previousapplications.aspx");

            // Similar.

            Similar = context.MapAreaRoute<SimilarJobsController, Guid, JobAdsPresentationModel>("members/jobs/similar/{jobAdId}", c => c.SimilarJobs);
            SimilarPartial = context.MapAreaRoute<SimilarJobsController, Guid, JobAdsPresentationModel>("members/jobs/similar/{jobAdId}/partial", c => c.SimilarJobsPartial);

            // Suggested.

            context.MapAreaRoute<SuggestedJobsMobileController>("members/jobs/suggested", c => c.SuggestedJobs);
            Suggested = context.MapAreaRoute<SuggestedJobsController, JobAdsPresentationModel>("members/jobs/suggested", c => c.SuggestedJobs);
            SuggestedPartial = context.MapAreaRoute<SuggestedJobsController, JobAdsPresentationModel>("members/jobs/suggested/partial", c => c.SuggestedJobsPartial);

            // Folders.

            Folder = context.MapAreaRoute<FoldersWebController, Guid, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/folders/{folderId}", c => c.Folder);
            PartialFolder = context.MapAreaRoute<FoldersWebController, Guid, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/folders/{folderId}/partial", c => c.PartialFolder);
            MobileFolder = context.MapAreaRoute<FoldersMobileController, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/folders/mobile", c => c.MobileFolder);
            PartialMobileFolder = context.MapAreaRoute<FoldersMobileController, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/folders/mobile/partial", c => c.PartialMobileFolder);
            context.MapRedirectRoute("members/jobs/folders/mobile", SearchRoutes.Search);

            // Folders API.

            ApiAddJobAdsToFolder = context.MapAreaRoute<FoldersApiController, Guid, Guid[]>("members/jobs/folders/api/{folderId}/addjobs", c => c.AddJobAds);
            ApiAddJobAdsToMobileFolder = context.MapAreaRoute<FoldersApiController, Guid[]>("members/jobs/folders/api/mobile/addjobs", c => c.AddMobileJobAds);
            ApiRemoveJobAdsFromFolder = context.MapAreaRoute<FoldersApiController, Guid, Guid[]>("members/jobs/folders/api/{folderId}/removejobs", c => c.RemoveJobAds);
            ApiRemoveJobAdsFromMobileFolder = context.MapAreaRoute<FoldersApiController, Guid[]>("members/jobs/folders/api/mobile/removejobs", c => c.RemoveMobileJobAds);
            ApiEmptyFolder = context.MapAreaRoute<FoldersApiController, Guid>("members/jobs/folders/api/{folderId}/removealljobs", c => c.RemoveAllJobAds);
            ApiRenameFolder = context.MapAreaRoute<FoldersApiController, Guid, string>("members/jobs/folders/api/{folderId}/rename", c => c.RenameFolder);
            AddJobAdToMobileFolder = context.MapAreaRoute<FoldersMobileController, Guid[]>("members/jobs/folders/mobile/addjob", c => c.AddJobAdToMobileFolder);

            // FlagLists.

            FlagList = context.MapAreaRoute<FlagListsController, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/flaglist", c => c.FlagList);
            PartialFlagList = context.MapAreaRoute<FlagListsController, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/flaglist/partial", c => c.PartialFlagList);

            // FlagLists API.

            ApiFlagJobAds = context.MapAreaRoute<FlagListsApiController, Guid[]>("members/jobs/flaglists/api/addjobads", c => c.AddJobAds);
            ApiUnflagJobAds = context.MapAreaRoute<FlagListsApiController, Guid[]>("members/jobs/flaglists/api/removejobads", c => c.RemoveJobAds);
            ApiUnflagAllJobAds = context.MapAreaRoute<FlagListsApiController>("members/jobs/flaglists/api/removealljobads", c => c.RemoveAllJobAds);
            ApiUnflagCurrentJobAds = context.MapAreaRoute<FlagListsApiController>("members/jobs/flaglists/api/removecurrentjobads", c => c.RemoveCurrentJobAds);

            // BlockLists.

            BlockList = context.MapAreaRoute<BlockListsController, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/blocklist", c => c.BlockList);
            PartialBlockList = context.MapAreaRoute<BlockListsController, JobAdSearchSortCriteria, JobAdsPresentationModel>("members/jobs/blocklist/partial", c => c.PartialBlockList);

            // BlockLists API.

            ApiBlockJobAds = context.MapAreaRoute<BlockListsApiController, Guid[]>("members/jobs/blocklists/api/blockJobAds", c => c.BlockJobAds);
            ApiUnblockJobAds = context.MapAreaRoute<BlockListsApiController, Guid[]>("members/jobs/blocklists/api/unblockJobAds", c => c.UnblockJobAds);
            ApiUnblockAllJobAds = context.MapAreaRoute<BlockListsApiController>("members/jobs/blocklists/api/unblockallJobAds", c => c.UnblockAllJobAds);

            // Notes API.

            ApiNotes = context.MapAreaRoute<NotesApiController, Guid>("members/jobs/notes/api", c => c.Notes);
            ApiNewNote = context.MapAreaRoute<NotesApiController, Guid[], string>("members/jobs/notes/api/new", c => c.NewNote);
            ApiEditNote = context.MapAreaRoute<NotesApiController, Guid, string>("members/jobs/notes/api/{noteId}/edit", c => c.EditNote);
            ApiDeleteNote = context.MapAreaRoute<NotesApiController, Guid>("members/jobs/notes/api/{noteId}/delete", c => c.DeleteNote);
            context.MapAreaRoute<NotesApiController, Guid>("members/jobs/notes/api/{noteId}", c => c.Note);

            // Only allow POSTs to these urls.

            ApiExternallyApplied = context.MapAreaRoute<JobAdsApiController, Guid>("members/jobs/api/{jobAdId}/externallyapplied", c => c.ExternallyApplied, new HttpMethodConstraint("POST"));
            context.MapJsonNotFoundRoute("members/jobs/api/{jobAdId}/externallyapplied");
            context.MapJsonNotFoundRoute("members/jobs/api/externallyapplied");

            ApiViewed = context.MapAreaRoute<JobAdsApiController, Guid>("members/jobs/api/{jobAdId}/viewed", c => c.Viewed, new HttpMethodConstraint("POST"));
            context.MapJsonNotFoundRoute("members/jobs/api/{jobAdId}/viewed");

            // Catch all old job ad url that needs to map to the job ad page.

            context.MapAreaRoute<BrowseJobAdsController, string>(false, "jobs/{locationSegment}/{industrySegment}/{jobAdId}", c => c.OldJobAd);

            context.MapRedirectRoute("ui/unregistered/SimilarJobs.aspx", Similar, new { jobAdId = new RedirectQueryString() });
            context.MapRedirectRoute("ui/unregistered/SimilarJobs.aspx", BrowseJobAds);
            context.MapRedirectRoute("ui/registered/networkers/SuggestedJobs.aspx", Suggested);

            context.MapRedirectRoute("ui/unregistered/SendJobToFriendPopupContents.aspx", BrowseJobAds);
        }
    }
}
