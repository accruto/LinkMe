using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Controllers.JobAds;
using LinkMe.Web.Areas.Employers.Models;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public static class JobAdsRoutes
    {
        public static RouteReference JobAds { get; private set; }

        public static RouteReference JobAd { get; private set; }
        public static RouteReference Preview { get; private set; }
        public static RouteReference Account { get; private set; }
        public static RouteReference Payment { get; private set; }
        public static RouteReference Receipt { get; private set; }

        public static RouteReference ApiJobAds { get; private set; }
        public static RouteReference ApiShortlistCandidatesForJobAd { get; private set; }
        public static RouteReference ApiRejectCandidatesForJobAd { get; private set; }
        public static RouteReference ApiRemoveCandidatesFromJobAd { get; private set; }
        public static RouteReference ApiUndoShortlistCandidatesForJobAd { get; private set; }
        public static RouteReference ApiUndoRejectCandidatesForJobAd { get; private set; }
        public static RouteReference ApiUndoRemoveCandidatesFromJobAd { get; private set; }

        public static RouteReference PartialManageJobAdCandidates { get; private set; }
        public static RouteReference ManageJobAdCandidates { get; private set; }
        public static RouteReference ManageCandidates { get; private set; }

        public static RouteReference SuggestedCandidates { get; private set; }
        public static RouteReference PartialSuggestedCandidates { get; private set; }

        public static RouteReference Logo { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            JobAds = context.MapAreaRoute<JobAdsController>("employers/candidates/jobads", c => c.JobAds);

            JobAd = context.MapAreaRoute<JobAdController, Guid?>("employers/jobads/jobad", c => c.JobAd);
            Preview = context.MapAreaRoute<JobAdController, Guid, JobAdFeaturePack?>("employers/jobads/jobad/preview", c => c.Preview);
            Account = context.MapAreaRoute<JobAdController>("employers/jobads/jobad/account", c => c.Account);
            Payment = context.MapAreaRoute<JobAdController, Guid, JobAdFeaturePack>("employers/jobads/jobad/payment", c => c.Payment);
            Receipt = context.MapAreaRoute<JobAdController, Guid, Guid>("employers/jobads/jobad/receipt", c => c.Receipt);

            context.MapRedirectRoute("ui/registered/employers/EmployerNewJobAd.aspx", JobAd);

            Logo = context.MapAreaRoute<JobAdFilesController, Guid>("employers/jobads/logos/{fileId}", c => c.Logo);

            // JobAds API.

            ApiJobAds = context.MapAreaRoute<JobAdsApiController>("employers/candidates/jobads/api", c => c.JobAds);
            ApiShortlistCandidatesForJobAd = context.MapAreaRoute<JobAdsApiController, Guid, Guid[]>("employers/candidates/jobads/api/{jobAdId}/shortlistcandidates", c => c.ShortlistCandidates);
            ApiRejectCandidatesForJobAd = context.MapAreaRoute<JobAdsApiController, Guid, Guid[]>("employers/candidates/jobads/api/{jobAdId}/rejectcandidates", c => c.RejectCandidates);
            ApiRemoveCandidatesFromJobAd = context.MapAreaRoute<JobAdsApiController, Guid, Guid[]>("employers/candidates/jobads/api/{jobAdId}/removecandidates", c => c.RemoveCandidates);

            ApiUndoShortlistCandidatesForJobAd = context.MapAreaRoute<JobAdsApiController, Guid, Guid[], ApplicantStatus?>("employers/candidates/jobads/api/{jobAdId}/undoshortlistcandidates", c => c.UndoShortlistCandidates);
            ApiUndoRejectCandidatesForJobAd = context.MapAreaRoute<JobAdsApiController, Guid, Guid[], ApplicantStatus>("employers/candidates/jobads/api/{jobAdId}/undorejectcandidates", c => c.UndoRejectCandidates);
            ApiUndoRemoveCandidatesFromJobAd = context.MapAreaRoute<JobAdsApiController, Guid, Guid[]>("employers/candidates/jobads/api/{jobAdId}/undoremovecandidates", c => c.UndoRemoveCandidates);

            // JobAds.

            PartialManageJobAdCandidates = context.MapAreaRoute<ManageCandidatesController, ApplicantStatus, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/manage/partial", c => c.Partial);
            ManageJobAdCandidates = context.MapAreaRoute<ManageCandidatesController, Guid, ApplicantStatus?, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/manage/{jobAdId}", c => c.Manage);
            ManageCandidates = context.MapAreaRoute<ManageCandidatesController>("employers/candidates/manage", c => c.ManageCandidates);

            context.MapAreaRoute<SuggestedCandidatesController, string>("employers/candidates/suggested", c => c.ExternalSuggestedCandidates);
            SuggestedCandidates = context.MapAreaRoute<SuggestedCandidatesController, Guid, CandidatesPresentationModel>("employers/candidates/suggested/{jobAdId}", c => c.SuggestedCandidates);
            PartialSuggestedCandidates = context.MapAreaRoute<SuggestedCandidatesController, MemberSearchCriteria, CandidatesPresentationModel>("employers/candidates/suggested/partial", c => c.PartialSuggestedCandidates);

            context.MapRedirectRoute("ui/registered/employers/CandidateSuggestion.aspx", SuggestedCandidates);
        }
    }
}
