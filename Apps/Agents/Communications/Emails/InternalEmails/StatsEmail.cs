using System.Collections;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;
using LinkMe.Query.Reports.DailyReports;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public class StatsEmail
        : AllStaffEmail
    {
        private readonly DailyReport _dailyReport;

        public StatsEmail(DailyReport dailyReport)
        {
            _dailyReport = dailyReport;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);

            properties.Add("ReportDate", _dailyReport.Day.Start.Value);

            properties.Add("MemberReport", _dailyReport.MemberReport);
            properties.Add("ResumeReport", _dailyReport.ResumeReport);

            properties.Add("OpenJobAds", _dailyReport.OpenJobAds);
            properties.Add("JobSearches", _dailyReport.JobSearches);
            properties.Add("JobApplications", _dailyReport.InternalJobApplications);
            properties.Add("ExternalJobRedirects", _dailyReport.ExternalJobApplications);
            properties.Add("WebMemberSearchBreakdownReport", _dailyReport.MemberSearchReports["Web"]);
            properties.Add("WebMemberViewingBreakdownReport", _dailyReport.MemberViewingReports["Web"]);
            properties.Add("WebMemberAccessBreakdownReport", _dailyReport.MemberAccessReports["Web"]);
            properties.Add("ApiMemberSearchBreakdownReport", _dailyReport.MemberSearchReports["API"]);
            properties.Add("ApiMemberViewingBreakdownReport", _dailyReport.MemberViewingReports["API"]);
            properties.Add("ApiMemberAccessBreakdownReport", _dailyReport.MemberAccessReports["API"]);
            properties.Add("ResumeSearchAlerts", _dailyReport.ResumeSearchAlerts);
            properties.Add("JobSearchAlerts", _dailyReport.JobSearchAlerts);
            properties.Add("InvitationsSent", _dailyReport.InvitationsSent);
            properties.Add("InvitationsAccepted", _dailyReport.InvitationsAccepted);

            // Daily

            properties.Add("LoggedInUniqueDaily", _dailyReport.DailyLogIns.Values.Sum());
            properties.Add("LoggedInUniquePercentageDaily", GetPercentage(_dailyReport.DailyLogIns.Values.Sum()));
            properties.Add("LoggedInUniqueMembersDaily", _dailyReport.DailyLogIns[UserType.Member]);
            properties.Add("LoggedInUniqueEmployersDaily", _dailyReport.DailyLogIns[UserType.Employer]);
            properties.Add("LoggedInUniqueAdministratorsDaily", _dailyReport.DailyLogIns[UserType.Administrator]);
            properties.Add("LoggedInUniqueCustodiansDaily", _dailyReport.DailyLogIns[UserType.Custodian]);

            // Weekly

            properties.Add("LoggedInUniqueWeekly", _dailyReport.WeeklyLogIns.Values.Sum());
            properties.Add("LoggedInUniqueWeeklyPercentage", GetPercentage(_dailyReport.WeeklyLogIns.Values.Sum()));
            properties.Add("LoggedInUniqueMembersWeekly", _dailyReport.WeeklyLogIns[UserType.Member]);
            properties.Add("LoggedInUniqueEmployersWeekly", _dailyReport.WeeklyLogIns[UserType.Employer]);
            properties.Add("LoggedInUniqueAdministratorsWeekly", _dailyReport.WeeklyLogIns[UserType.Administrator]);
            properties.Add("LoggedInUniqueCustodiansWeekly", _dailyReport.WeeklyLogIns[UserType.Custodian]);

            // Monthly

            properties.Add("LoggedInUniqueMonthly", _dailyReport.MonthlyLogIns.Values.Sum());
            properties.Add("LoggedInUniqueMonthlyPercentage", GetPercentage(_dailyReport.MonthlyLogIns.Values.Sum()));
            properties.Add("LoggedInUniqueMembersMonthly", _dailyReport.MonthlyLogIns[UserType.Member]);
            properties.Add("LoggedInUniqueEmployersMonthly", _dailyReport.MonthlyLogIns[UserType.Employer]);
            properties.Add("LoggedInUniqueAdministratorsMonthly", _dailyReport.MonthlyLogIns[UserType.Administrator]);
            properties.Add("LoggedInUniqueCustodiansMonthly", _dailyReport.MonthlyLogIns[UserType.Custodian]);

            properties.Add("AcceptanceRateLast48Hours", _dailyReport.AcceptanceRateLast48Hours);
            properties.Add("AcceptanceRatePreviousMonth", _dailyReport.AcceptanceRatePreviousMonth);

            properties.Add("OrderReports", _dailyReport.OrderReports, typeof(IList));
            properties.Add("CommunicationReports", new SortedList((IDictionary)_dailyReport.CommunciationReports));
            properties.Add("PromotionCodeReports", new SortedList((IDictionary)_dailyReport.PromotionCodeReports));
            properties.Add("JobAdIntegrationReports", new SortedList((IDictionary)_dailyReport.JobAdIntegrationReports));
        }

        private int GetPercentage(int number)
        {
            return _dailyReport.MemberReport.Total == 0
                ? 0
                : (number * 100) / _dailyReport.MemberReport.Total;
        }
    }
}