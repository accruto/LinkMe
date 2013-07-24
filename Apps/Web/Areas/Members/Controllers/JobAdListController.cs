using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;
using LinkMe.Web.Areas.Members.Models;
using Reference = LinkMe.Web.Areas.Members.Models.Reference;

namespace LinkMe.Web.Areas.Members.Controllers
{
    //This class should not be implemented directly; use JobAdSearchListController or JobAdSortListController
    public abstract class JobAdListController
        : MembersController
    {
        protected readonly IJobAdsQuery _jobAdsQuery;
        protected readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;
        protected readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;
        protected readonly IJobAdProcessingQuery _jobAdProcessingQuery;
        protected readonly IEmployersQuery _employersQuery;

        protected JobAdListController(IJobAdsQuery jobAdsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IEmployersQuery employersQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
            _jobAdFlagListsQuery = jobAdFlagListsQuery;
            _jobAdProcessingQuery = jobAdProcessingQuery;
            _employersQuery = employersQuery;
        }

        protected static JobAdsPresentationModel PreparePresentationModel(JobAdsPresentationModel model)
        {
            // Ensure that the pagination values are always set.

            if (model == null)
                model = new JobAdsPresentationModel();
            model.Pagination = PreparePagination(model.Pagination);
            model.ItemsPerPage = Reference.ItemsPerPage;
            model.DefaultItemsPerPage = Reference.DefaultItemsPerPage;
            return model;
        }

        protected static Pagination PreparePagination(Pagination pagination)
        {
            // Ensure that the pagination values are always set.

            if (pagination == null)
                pagination = new Pagination();
            if (pagination.Page == null)
                pagination.Page = 1;
            if (pagination.Items == null)
                pagination.Items = Reference.DefaultItemsPerPage;
            return pagination;
        }

        protected TListModel CreateEmptyList<TListModel>(IMember member, JobAdSearchCriteria criteria, JobAdsPresentationModel presentation, JobAdListType listType)
            where TListModel : JobAdSearchListModel, new()
        {
            var model = new TListModel
            {
                ListType = listType,
                Criteria = criteria,
                Presentation = presentation,
                Results = new JobAdListResultsModel
                {
                    TotalJobAds = 0,
                    IndustryHits = new Dictionary<string, int>(),
                    JobTypeHits = GetEnumHitsDictionary(new List<KeyValuePair<JobTypes, int>>(), JobTypes.All, JobTypes.None),
                    JobAdIds = new List<Guid>(),
                    JobAds = new Dictionary<Guid, MemberJobAdView>(),
                },
            };

            return model;
        }

        protected TListModel CreateEmptyList<TListModel>(IMember member, JobAdSortCriteria criteria, JobAdsPresentationModel presentation, JobAdListType listType)
            where TListModel : JobAdSortListModel, new()
        {
            var model = new TListModel
            {
                Criteria = criteria,
                Presentation = presentation,
                ListType = listType,
                Results = new JobAdListResultsModel
                {
                    TotalJobAds = 0,
                    IndustryHits = new Dictionary<string, int>(),
                    JobTypeHits = GetEnumHitsDictionary(new List<KeyValuePair<JobTypes, int>>(), JobTypes.All, JobTypes.None),
                    JobAdIds = new List<Guid>(),
                    JobAds = new Dictionary<Guid, MemberJobAdView>(),
                },
            };

            return model;
        }

        private static IDictionary<string, int> GetEnumHitsDictionary<T>(IEnumerable<KeyValuePair<T, int>> hitList, params T[] ignore)
        {
            // The list may not have counts for all values so ensure they are set to 0.

            var hits = new Dictionary<string, int>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if (!ignore.Contains((T) value))
                    hits[value.ToString()] = 0;
            }

            foreach (var hit in hitList)
            {
                if (!ignore.Contains(hit.Key))
                    hits[hit.Key.ToString()] = hit.Value;
            }

            return hits;
        }

        protected IDictionary<Guid, MemberJobAdView> GetMemberJobAdViews(IMember member, IEnumerable<Guid> jobAdIds)
        {
            return _memberJobAdViewsQuery.GetMemberJobAdViews(member, jobAdIds).ToDictionary(j => j.Id, j => j);
        }
    }
}
