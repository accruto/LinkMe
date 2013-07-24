using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class SuggestedJobsController
        : JobAdSearchListController
    {
        private readonly IIndustriesQuery _industriesQuery;

        public SuggestedJobsController(IJobAdFoldersCommand jobAdFoldersCommand, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdsQuery jobAdsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery, IIndustriesQuery industriesQuery)
            : base(executeJobAdSearchCommand, jobAdsQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, jobAdFoldersQuery, jobAdFoldersCommand, jobAdBlockListsQuery, employersQuery)
        {
            _industriesQuery = industriesQuery;
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult SuggestedJobs(JobAdsPresentationModel presentation)
        {
            return View(GetSuggestedJobs(presentation));
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult SuggestedJobsPartial(JobAdsPresentationModel presentation)
        {
            return PartialView("JobAdList", GetSuggestedJobs(presentation));
        }

        private SuggestedJobsListModel GetSuggestedJobs(JobAdsPresentationModel presentation)
        {
            var model = SuggestedJobs<SuggestedJobsListModel>(CurrentMember, PreparePresentationModel(presentation));
            model.Industries = _industriesQuery.GetIndustries();
            return model;
        }
    }
}
