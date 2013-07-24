using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Apps.Api.Areas.Employers.Controllers.Search
{
    public class SearchApiController
        : CandidateListApiController
    {
        private readonly IMemberSearchesCommand _memberSearchesCommand;

        public SearchApiController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, IMemberSearchesCommand memberSearchesCommand)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _memberSearchesCommand = memberSearchesCommand;
        }

        [HttpGet]
        public ActionResult Search(MemberSearchCriteria criteria, Pagination pagination)
        {
            try
            {
                if (criteria.Location != null && !criteria.Location.IsFullyResolved)
                    throw new UserException("Location not recognised. Check your spelling or try specifying a postcode");

                criteria.PrepareCriteria();
                pagination = Prepare(pagination);

                // Search.

                var employer = CurrentEmployer;
                var model = Search(employer, criteria, pagination);
                SaveSearch(employer, criteria, model, pagination.Page.Value);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), JsonRequestBehavior.AllowGet);
        }

        private void SaveSearch(IHasId<Guid> employer, MemberSearchCriteria criteria, CandidatesResponseModel model, int page)
        {
            try
            {
                var userAgent = HttpContext.Request.UserAgent;
                
                //never log monitoring calls
                if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("LinkmeMonitoring"))
                    return;

                var app = ActivityContext.Channel.App;
                var execution = new MemberSearchExecution
                {
                    Context = "NewSearch",
                    Criteria = criteria.Clone(),
                    Results = new MemberSearchResults
                    {
                        MemberIds = page == 1 ? (from c in model.Candidates select c.Id).ToArray() : new Guid[0],
                        TotalMatches = model.TotalCandidates
                    },
                    SearcherId = employer == null ? (Guid?)null : employer.Id,
                    ChannelId = app.ChannelId,
                    AppId = app.Id,
                    StartTime = DateTime.Now,
                };

                _memberSearchesCommand.CreateMemberSearchExecution(execution);
            }
            catch (Exception)
            {
                // Never fail for this.
            }
        }
    }
}