using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    [EnsureAuthorized(UserType.Member)]
    public class ApplicationsMobileController
        : JobAdListController
    {
        private readonly IMemberApplicationsQuery _memberApplicationsQuery;

        public ApplicationsMobileController(IJobAdsQuery jobAdsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IEmployersQuery employersQuery, IMemberApplicationsQuery memberApplicationsQuery)
            : base(jobAdsQuery, memberJobAdViewsQuery, jobAdFlagListsQuery, jobAdProcessingQuery, employersQuery)
        {
            _memberApplicationsQuery = memberApplicationsQuery;
        }

        public ActionResult Applications()
        {
            var pagination = PreparePagination(new Pagination());

            var member = CurrentMember;
            var applications = _memberApplicationsQuery.GetApplications(member.Id).OrderByDescending(a => a.CreatedTime).SelectRange(pagination.ToRange()).ToList();

            var jobAdIds = (from a in applications select a.PositionId).ToList();
            var model = new ApplicationsListMobileModel
            {
                Results = new JobAdListResultsMobileModel
                {
                    JobAdIds = jobAdIds,
                    JobAds = GetMemberJobAdViews(member, jobAdIds),
                },
            };

            return View(model);
        }
    }
}