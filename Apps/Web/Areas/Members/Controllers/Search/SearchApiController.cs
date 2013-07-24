using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.Search;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Members.Controllers.Search
{
    public class SearchApiController
        : MembersApiController
    {
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand;
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;

        private readonly JavaScriptConverter[] _converters = new[] { new MemberJobAdViewJavaScriptConverter() };

        public SearchApiController(IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdSearchesCommand jobAdSearchesCommand, IMemberJobAdViewsQuery memberJobAdViewsQuery)
        {
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
            _jobAdSearchesCommand = jobAdSearchesCommand;
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
        }

        protected override JavaScriptConverter[] GetConverters()
        {
            return _converters;
        }

        [HttpGet]
        public ActionResult Search(JobAdSearchCriteria criteria, JobAdsPresentationModel presentation)
        {
            try
            {
                presentation = PreparePresentationModel(presentation);
                criteria.PrepareCriteria();

                var execution = _executeJobAdSearchCommand.Search(CurrentMember, criteria, presentation.Pagination.ToRange());

                SaveSearch(SearchContext.Filter, CurrentMember, execution.Results, criteria, presentation.Pagination.Page.Value);
                MemberContext.CurrentSearch = new JobAdSearchNavigation(criteria, presentation);

                // Be sure to maintain the order of the search.

                var views = _memberJobAdViewsQuery.GetMemberJobAdViews(CurrentMember, execution.Results.JobAdIds).ToDictionary(v => v.Id, v => v);
                var jobAds = (from i in execution.Results.JobAdIds
                              select views[i]).ToList();

                var model = new JsonJobAdsResponseModel
                {
                    TotalJobAds = execution.Results.TotalMatches,
                    IndustryHits = execution.Results.IndustryHits.ToDictionary(i => i.Key.ToString(), i => i.Value),
                    JobTypeHits = GetEnumHitsDictionary(execution.Results.JobTypeHits, JobTypes.All, JobTypes.None),
                    JobAds = jobAds,
                    CriteriaHtml = criteria.GetCriteriaHtml(),
                    Hash = criteria.GetHash(),
                    QueryStringForGa = new AdSenseQueryGenerator(new JobAdSearchCriteriaAdSenseConverter()).GenerateAdSenseQuery(criteria)
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private static IDictionary<string, int> GetEnumHitsDictionary<T>(IEnumerable<KeyValuePair<T, int>> hitList, params T[] ignore)
        {
            // The list may not have counts for all values so ensure they are set to 0.

            var hits = new Dictionary<string, int>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if (!ignore.Contains((T)value))
                    hits[value.ToString()] = 0;
            }

            foreach (var hit in hitList)
            {
                if (!ignore.Contains(hit.Key))
                    hits[hit.Key.ToString()] = hit.Value;
            }

            return hits;
        }

        private static JobAdsPresentationModel PreparePresentationModel(JobAdsPresentationModel model)
        {
            // Ensure that the pagination values are always set.

            if (model == null)
                model = new JobAdsPresentationModel();
            if (model.Pagination == null)
                model.Pagination = new Pagination();
            if (model.Pagination.Page == null)
                model.Pagination.Page = 1;
            if (model.Pagination.Items == null)
                model.Pagination.Items = Reference.DefaultItemsPerPage;
            model.ItemsPerPage = Reference.ItemsPerPage;
            model.DefaultItemsPerPage = Reference.DefaultItemsPerPage;

            return model;
        }

        private void SaveSearch(SearchContext context, IHasId<Guid> member, JobAdSearchResults results, JobAdSearchCriteria criteria, int page)
        {
            try
            {
                var userAgent = HttpContext.Request.UserAgent;

                //never log monitoring calls
                if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("LinkmeMonitoring"))
                    return;

                var app = ActivityContext.Channel.App;
                var execution = new JobAdSearchExecution
                {
                    Context = context.ToString(),
                    Criteria = criteria.Clone(),
                    Results = new JobAdSearchResults
                    {
                        JobAdIds = page == 1 ? results.JobAdIds : new List<Guid>(),
                        TotalMatches = results.TotalMatches
                    },
                    SearcherId = member == null ? (Guid?)null : member.Id,
                    SearchId = null,
                    ChannelId = app.ChannelId,
                    AppId = app.Id,
                    StartTime = DateTime.Now,
                };

                _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);
            }
            catch (Exception)
            {
                // Never fail for this.
            }
        }
    }
}
