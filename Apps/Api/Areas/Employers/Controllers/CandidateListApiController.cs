using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Apps.Api.Areas.Employers.Controllers
{
    public abstract class CandidateListApiController
        : ApiController
    {
        protected readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        protected readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private static readonly EventSource EventSource = new EventSource<CandidateListApiController>();

        private const int DefaultItemsPerPage = 25;

        private readonly JavaScriptConverter[] _converters = new[] { new CandidateModelJavaScriptConverter() };

        protected CandidateListApiController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery)
        {
            _executeMemberSearchCommand = executeMemberSearchCommand;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _memberStatusQuery = memberStatusQuery;
        }

        protected override JavaScriptConverter[] GetConverters()
        {
            return _converters;
        }

        protected static Pagination Prepare(Pagination pagination)
        {
            if (pagination == null)
                pagination = new Pagination();
            if (pagination.Page == null)
                pagination.Page = 1;
            if (pagination.Items == null)
                pagination.Items = DefaultItemsPerPage;
            return pagination;
        }

        protected CandidatesResponseModel Search(IEmployer employer, MemberSearchCriteria criteria, Pagination pagination)
        {
            return Search(employer, e => _executeMemberSearchCommand.Search(e, criteria, pagination.ToRange()));
        }

        protected CandidatesResponseModel Search(IEmployer employer, Func<IEmployer, MemberSearchExecution> search)
        {
            const string method = "API Search";
            // Search.

            var execution = search(employer);

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Results retrieved.", Event.Arg("total hits", execution.Results.TotalMatches), Event.Arg("result count", execution.Results.MemberIds.Count));

            // Process results.

            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, execution.Results.MemberIds);

            #region Log
            if (EventSource.IsEnabled(Event.Trace))
            {
                EventSource.Raise(Event.Trace, method, "Results parsed.",
                                  Event.Arg("total results parsed", views.Count()));
            }
            #endregion

            return new CandidatesResponseModel
            {
                TotalCandidates = execution.Results.TotalMatches,
                Candidates = (from id in execution.Results.MemberIds select GetCandidateModel(views[id])).ToList(),
            };
        }

        protected CandidateModel GetCandidateModel(EmployerMemberView view)
        {
            const string method = "CandidateModel Creation";
            
            #region Log
            Stopwatch searchTime = null;
            if (EventSource.IsEnabled(Event.Trace))
            {
                searchTime = Stopwatch.StartNew();
            }
            #endregion


            // Only supply the phone numbers if the candidate has them and if they have already been accessed.

            var canContactByPhone = view.CanContactByPhone();
            var phoneNumbers = canContactByPhone == CanContactStatus.YesWithoutCredit && view.HasBeenAccessed
                ? view.PhoneNumbers.Where(p => !string.IsNullOrEmpty(p.Number)).Select(p => p.Number).ToList()
                : null;

            // Always convert salary to yearly rate for display.

            var salary = view.DesiredSalary == null ? null : view.DesiredSalary.ToRate(SalaryRate.Year);

            var model = new CandidateModel
            {
                Id = view.Id,

                CanContact = view.CanContact(),
                CanContactByPhone = canContactByPhone,
                HasBeenViewed = view.HasBeenViewed,
                HasBeenAccessed = view.HasBeenAccessed,
                IsInMobileFolder = view.IsInMobileFolder,

                FullName = view.GetCandidateTitle(),
                PhoneNumbers = phoneNumbers,

                LastUpdatedTime = _memberStatusQuery.GetLastUpdatedTime(view, view, view.Resume).ToUniversalTime(),
                Status = view.Status,
                Location = view.Address.Location.ToString(),
                DesiredSalary = salary == null ? null : new SalaryModel { LowerBound = salary.LowerBound, UpperBound = salary.UpperBound },
                DesiredJobTitle = view.DesiredJobTitle,
                DesiredJobTypes = view.DesiredJobTypes,

                Jobs = view.Resume == null || view.Resume.Jobs == null || view.Resume.Jobs.Count == 0
                    ? null
                    : view.Resume.Jobs.Select(j => new JobModel
                    {
                        Title = j.Title,
                        StartDate = j.Dates == null ? null : j.Dates.Start,
                        EndDate = j.Dates == null ? null : j.Dates.End,
                        IsCurrent = j.IsCurrent,
                        Company = j.Company,
                    }).ToList(),
                Summary = view.Resume == null ? null : view.Resume.Summary,
            };

            #region Log
            if (EventSource.IsEnabled(Event.Trace))
            {
                searchTime.Stop();
                EventSource.Raise(Event.Trace, method, "Model Created.",
                    Event.Arg("member id", view.Id),
                    Event.Arg("model creation time (ms)", searchTime.ElapsedMilliseconds));
            }
            #endregion

            return model;
        }
    }
}
