using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    [EnsureAuthorized(UserType.Member)]
    public class SuggestedJobsMobileController
        : JobAdListController
    {
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;

        public SuggestedJobsMobileController(IJobAdsQuery jobAdsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IEmployersQuery employersQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand)
            : base(jobAdsQuery, memberJobAdViewsQuery, jobAdFlagListsQuery, jobAdProcessingQuery, employersQuery)
        {
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
        }

        public ActionResult SuggestedJobs()
        {
            var pagination = PreparePagination(new Pagination());
            var execution = _executeJobAdSearchCommand.SearchSuggested(CurrentMember, null, pagination.ToRange());

            var jobAdIds = execution.Results.JobAdIds.SelectRange(pagination.ToRange()).ToList();
            var model = new SuggestedJobsListMobileModel
            {
                Results = new JobAdListResultsMobileModel
                {
                    JobAdIds = jobAdIds,
                    JobAds = GetMemberJobAdViews(CurrentMember, jobAdIds),
                },
            };

            return View(model);
        }
    }
}