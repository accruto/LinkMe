using System;
using System.Web.Mvc;
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

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class SimilarJobsController
        : JobAdSearchListController
    {
        private readonly IIndustriesQuery _industriesQuery;

        public SimilarJobsController(IJobAdFoldersCommand jobAdFoldersCommand, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdsQuery jobAdsQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery, IIndustriesQuery industriesQuery)
            : base(executeJobAdSearchCommand, jobAdsQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, jobAdFoldersQuery, jobAdFoldersCommand, jobAdBlockListsQuery, employersQuery)
        {
            _industriesQuery = industriesQuery;
        }

        public ActionResult SimilarJobs(Guid jobAdId, JobAdsPresentationModel presentation)
        {
            return View(GetSimilarJobs(CurrentMember, jobAdId, presentation));
        }

        public ActionResult SimilarJobsPartial(Guid jobAdId, JobAdsPresentationModel presentation)
        {
            return PartialView("JobAdList", GetSimilarJobs(CurrentMember, jobAdId, presentation));
        }

        private SimilarJobsListModel GetSimilarJobs(IMember member, Guid jobAdId, JobAdsPresentationModel presentation)
        {
            var model = SimilarJobs<SimilarJobsListModel>(member, jobAdId, PreparePresentationModel(presentation));
            model.Industries = _industriesQuery.GetIndustries();
            model.JobAdId = jobAdId;
            return model;
        }
    }
}
