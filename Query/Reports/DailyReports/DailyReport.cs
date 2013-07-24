using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Reports.Roles.Communications;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Orders;
using LinkMe.Query.Reports.Roles.Registration;
using LinkMe.Query.Reports.Users.Employers;

namespace LinkMe.Query.Reports.DailyReports
{
    public class MemberReport
    {
        public int Total { get; set; }
        public int Enabled { get; set; }
        public int Active { get; set; }
        public int New { get; set; }
    }

    public class ResumeReport
    {
        public int Total { get; set; }
        public int Searchable { get; set; }
        public int New { get; set; }
        public int Uploaded { get; set; }
        public int Reloaded { get; set; }
        public int Edited { get; set; }
        public int Updated { get; set; }
    }

    public class MemberSearchReport
    {
        public int TotalSearches { get; set; }
        public int FilterSearches { get; set; }
        public int SavedSearches { get; set; }
        public int AnonymousSearches { get; set; }
    }

    public class DailyReport
    {
        public DayRange Day;

        public MemberReport MemberReport { get; set; }
        public ResumeReport ResumeReport { get; set; }

        public int OpenJobAds;
        public int JobSearches;
        public int InternalJobApplications;
        public int ExternalJobApplications;
        public int ResumeSearchAlerts;
        public int JobSearchAlerts;
        public int InvitationsSent;
        public int InvitationsAccepted;

        public IDictionary<UserType, int> DailyLogIns;
        public IDictionary<UserType, int> WeeklyLogIns;
        public IDictionary<UserType, int> MonthlyLogIns;

        public int AcceptanceRatePreviousMonth;
        public int AcceptanceRateLast48Hours;

        public IDictionary<string, MemberSearchReport> MemberSearchReports { get; set; }
        public IDictionary<string, EmployerMemberViewingReport> MemberViewingReports { get; set; }
        public IDictionary<string, EmployerMemberAccessReport> MemberAccessReports { get; set; }

        public IList<OrderReport> OrderReports { get; set; }
        public IDictionary<string, CommunicationReport> CommunciationReports { get; set; }
        public IDictionary<string, PromotionCodeReport> PromotionCodeReports { get; set; }
        public IDictionary<string, JobAdIntegrationReport> JobAdIntegrationReports { get; set; } 
    }
}
