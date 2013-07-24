using System;
using System.Web.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    [EnsureAuthorized(UserType.Member)]
    public class FoldersMobileController
        : FoldersController
    {
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand;

        public FoldersMobileController(IExecuteJobAdSortCommand executeJobAdSortCommand, IJobAdFoldersCommand jobAdFoldersCommand, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IJobAdsQuery jobAdsQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery, IIndustriesQuery industriesQuery, IMemberJobAdListsCommand memberJobAdListsCommand)
            : base(executeJobAdSortCommand, jobAdFoldersCommand, jobAdFoldersQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, communitiesQuery, verticalsQuery, jobAdsQuery, jobAdBlockListsQuery, employersQuery, industriesQuery)
        {
            _memberJobAdListsCommand = memberJobAdListsCommand;
        }

        public ActionResult MobileFolder(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var member = CurrentMember;
            var folder = _jobAdFoldersQuery.GetMobileFolder(member);
            if (folder == null)
                return NotFound("folder");

            var model = FolderResults(folder.Id, sortCriteria, presentation);
            if (model == null)
                return NotFound("folder", "id", folder.Id);

            MemberContext.CurrentJobAds = new FolderNavigation(folder.Id, presentation);
            return View(model);
        }

        public ActionResult PartialMobileFolder(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var member = CurrentMember;
            var folder = _jobAdFoldersQuery.GetMobileFolder(member);
            if (folder == null)
                return NotFound("folder");

            var model = FolderResults(folder.Id, sortCriteria, presentation);
            if (model == null)
                return NotFound("folder", "id", folder.Id);

            MemberContext.CurrentJobAds = new FolderNavigation(folder.Id, presentation);
            return PartialView("JobAdList", model);
        }

        [EnsureAuthorized(UserType.Member, Reason = "AddJobAd")]
        public ActionResult AddJobAdToMobileFolder(Guid[] jobAdIds)
        {
            // Look for the folder.

            var member = CurrentMember;
            var folder = _jobAdFoldersQuery.GetMobileFolder(member);
            if (folder == null)
                return NotFound("folder");

            // Add job ads.

            _memberJobAdListsCommand.AddJobAdsToFolder(member, folder, jobAdIds);
            return RedirectToReturnUrlWithConfirmation("SHOW ADDED NOTIFICATION");
        }
    }
}