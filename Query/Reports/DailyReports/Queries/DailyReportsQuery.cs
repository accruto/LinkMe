using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Reports.Roles.Candidates.Queries;
using LinkMe.Query.Reports.Roles.Communications;
using LinkMe.Query.Reports.Roles.Communications.Queries;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Queries;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using LinkMe.Query.Reports.Roles.Networking.Queries;
using LinkMe.Query.Reports.Roles.Orders.Queries;
using LinkMe.Query.Reports.Roles.Registration.Queries;
using LinkMe.Query.Reports.Search.Queries;
using LinkMe.Query.Reports.Users.Employers.Queries;

namespace LinkMe.Query.Reports.DailyReports.Queries
{
    public class DailyReportsQuery
        : IDailyReportsQuery
    {
        private static readonly EventSource EventSource = new EventSource<DailyReportsQuery>();
        private readonly IResumeReportsQuery _resumeReportsQuery;
        private readonly IJobAdSearchReportsQuery _jobAdSearchReportsQuery;
        private readonly IMemberSearchReportsQuery _memberSearchReportsQuery;
        private readonly IJobAdReportsQuery _jobAdReportsQuery;
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery;
        private readonly INetworkingReportsQuery _networkingReportsQuery;
        private readonly IOrderReportsQuery _orderReportsQuery;
        private readonly ICommunicationReportsQuery _communicationReportsQuery;
        private readonly IRegistrationReportsQuery _registrationReportsQuery;
        private readonly IAccountReportsQuery _accountReportsQuery;
        private readonly IChannelsQuery _channelsQuery;
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IJobAdIntegrationReportsQuery _jobAdIntegrationReportsQuery;

        public DailyReportsQuery(IResumeReportsQuery resumeReportsQuery, IJobAdSearchReportsQuery jobAdSearchReportsQuery, IMemberSearchReportsQuery memberSearchReportsQuery, IJobAdReportsQuery jobAdReportsQuery, IEmployerMemberAccessReportsQuery employerMemberAccessReportsQuery, INetworkingReportsQuery networkingReportsQuery, IOrderReportsQuery orderReportsQuery, ICommunicationReportsQuery communicationReportsQuery, IRegistrationReportsQuery registrationReportsQuery, IAccountReportsQuery accountReportsQuery, IChannelsQuery channelsQuery, IIntegrationQuery integrationQuery, IJobAdIntegrationReportsQuery jobAdIntegrationReportsQuery)
        {
            _resumeReportsQuery = resumeReportsQuery;
            _jobAdSearchReportsQuery = jobAdSearchReportsQuery;
            _memberSearchReportsQuery = memberSearchReportsQuery;
            _jobAdReportsQuery = jobAdReportsQuery;
            _employerMemberAccessReportsQuery = employerMemberAccessReportsQuery;
            _networkingReportsQuery = networkingReportsQuery;
            _orderReportsQuery = orderReportsQuery;
            _communicationReportsQuery = communicationReportsQuery;
            _registrationReportsQuery = registrationReportsQuery;
            _accountReportsQuery = accountReportsQuery;
            _channelsQuery = channelsQuery;
            _integrationQuery = integrationQuery;
            _jobAdIntegrationReportsQuery = jobAdIntegrationReportsQuery;
        }

        DailyReport IDailyReportsQuery.GetDailyReport(DayRange day)
        {
            var week = new DateTimeRange(day.Start.Value.AddDays(-7), day.End.Value);
            var month = new DateTimeRange(day.Start.Value.AddMonths(-1), day.End.Value);

            var web = _channelsQuery.GetChannel("Web");
            var api = _channelsQuery.GetChannel("API");
            var channels = new[] { web, api };

            var userTypes = new[] { UserType.Member, UserType.Employer, UserType.Administrator, UserType.Custodian };

            return new DailyReport
                       {
                           Day = day,
                           OpenJobAds = _jobAdReportsQuery.GetOpenJobAds(),
                           ResumeSearchAlerts = _memberSearchReportsQuery.GetMemberSearchAlerts(),
                           JobSearchAlerts = _jobAdSearchReportsQuery.GetJobAdSearchAlerts(),
                           JobSearches = _jobAdSearchReportsQuery.GetJobAdSearches(day),
                           InternalJobApplications = _jobAdReportsQuery.GetInternalApplications(day),
                           ExternalJobApplications = _jobAdReportsQuery.GetExternalApplications(day),
                           InvitationsSent = _networkingReportsQuery.GetInvitationsSent(day),
                           InvitationsAccepted = _networkingReportsQuery.GetInvitationsAccepted(day),
                           AcceptanceRateLast48Hours = (int)_networkingReportsQuery.Get48HourInvitationAcceptancePercent(),
                           AcceptanceRatePreviousMonth = (int)_networkingReportsQuery.GetMonthlyInvitationAcceptancePercent(),

                           MemberReport = new MemberReport
                           {
                               Total = _accountReportsQuery.GetUsers(UserType.Member, day.End.Value),
                               Enabled = _accountReportsQuery.GetEnabledUsers(UserType.Member, day.End.Value),
                               Active = _accountReportsQuery.GetActiveUsers(UserType.Member, day.End.Value),
                               New = _accountReportsQuery.GetNewUsers(UserType.Member, day),
                           },

                           ResumeReport = new ResumeReport
                           {
                               Total = _resumeReportsQuery.GetResumes(day.End.Value),
                               Searchable = _resumeReportsQuery.GetSearchableResumes(day.End.Value),
                               New = _resumeReportsQuery.GetNewResumes(day),
                               Uploaded = _resumeReportsQuery.GetUploadedResumes(day),
                               Reloaded = _resumeReportsQuery.GetReloadedResumes(day),
                               Edited = _resumeReportsQuery.GetEditedResumes(day),
                               Updated = _resumeReportsQuery.GetUpdatedResumes(day),
                           },

                           // Logins.
                
                           DailyLogIns = (from u in userTypes select new { UserType = u, LogIns = _accountReportsQuery.GetLogIns(u, day) }).ToDictionary(x => x.UserType, x => x.LogIns),
                           WeeklyLogIns = (from u in userTypes select new { UserType = u, LogIns = _accountReportsQuery.GetLogIns(u, week) }).ToDictionary(x => x.UserType, x => x.LogIns),
                           MonthlyLogIns = (from u in userTypes select new { UserType = u, LogIns = _accountReportsQuery.GetLogIns(u, month) }).ToDictionary(x => x.UserType, x => x.LogIns),

                           // Member search reports.
                
                           MemberSearchReports = (from c in channels
                                                  select new
                                                  {
                                                      c.Name,
                                                      Report = new MemberSearchReport
                                                      {
                                                          TotalSearches = _memberSearchReportsQuery.GetMemberSearches(c, day),
                                                          FilterSearches = _memberSearchReportsQuery.GetFilterMemberSearches(c, day),
                                                          SavedSearches = _memberSearchReportsQuery.GetSavedMemberSearches(c, day),
                                                          AnonymousSearches = _memberSearchReportsQuery.GetAnonymousMemberSearches(c, day),
                                                      }
                                                  }).ToDictionary(x => x.Name, x => x.Report),

                           MemberViewingReports = (from c in channels
                                                   select new
                                                   {
                                                       c.Name,
                                                       Report = _employerMemberAccessReportsQuery.GetEmployerMemberViewingReport(c, day),
                                                   }).ToDictionary(x => x.Name, x => x.Report),

                           MemberAccessReports = (from c in channels
                                                  select new
                                                  {
                                                      c.Name,
                                                      Report = _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(c, day),
                                                  }).ToDictionary(x => x.Name, x => x.Report),

                           // Others.

                           OrderReports = _orderReportsQuery.GetOrderReports(day),
                           CommunciationReports = GetCommunicationReports(day),
                           PromotionCodeReports = _registrationReportsQuery.GetPromotionCodeReports(day).ToDictionary(x => x.Key, x => x.Value),
                           JobAdIntegrationReports = GetJobAdIntegrationReports(day),
                       };
        }

        private IDictionary<string, JobAdIntegrationReport> GetJobAdIntegrationReports(DateTimeRange day)
        {
            var reports = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(day);
            var integratorUsers = _integrationQuery.GetIntegratorUsers();

            return (from r in reports
                    select new
                    {
                        User = (from u in integratorUsers where u.Id == r.Key select u.LoginId).Single(),
                        Report = r.Value
                    }).ToDictionary(x => x.User, x => x.Report);
        }

        private Dictionary<string, CommunicationReport> GetCommunicationReports(DayRange day)
        {
            const string method = "GetCommunicationReports";
            try
            {
                return _communicationReportsQuery.GetCommunicationReport(day).ToDictionary(x => x.Key, x => x.Value);
            }
            catch (Exception ex)
            {
                // Just log.

                EventSource.Raise(Event.Error, method, "Cannot get the daily communication reports.", ex);
                return new Dictionary<string, CommunicationReport>();
            }
        }
    }
}