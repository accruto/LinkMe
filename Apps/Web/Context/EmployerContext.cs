using System;
using System.Web;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Models;

namespace LinkMe.Web.Context
{
    public abstract class CandidatesNavigation
        : Navigation
    {
        public DetailLevel? DetailLevel { get; private set; }

        protected CandidatesNavigation(CandidatesPresentationModel presentation)
            : base(presentation)
        {
            DetailLevel = presentation == null ? (DetailLevel?) null : presentation.DetailLevel;
        }
    }

    public class MemberSearchNavigation
        : CandidatesNavigation
    {
        private readonly MemberSearchCriteria _criteria;

        public MemberSearchNavigation(MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
            : base(presentation)
        {
            _criteria = criteria.Clone();
        }

        public MemberSearchCriteria Criteria
        {
            get { return _criteria.Clone(); }
        }
    }

    public class FolderNavigation
        : CandidatesNavigation
    {
        public Guid FolderId { get; private set; }

        public FolderNavigation(Guid folderId, CandidatesPresentationModel presentation)
            : base(presentation)
        {
            FolderId = folderId;
        }
    }

    public class JobAdNavigation
        : CandidatesNavigation
    {
        public Guid JobAdId { get; private set; }

        public JobAdNavigation(Guid jobAdId, CandidatesPresentationModel presentation)
            : base(presentation)
        {
            JobAdId = jobAdId;
        }
    }

    public class SuggestedCandidatesNavigation
        : MemberSearchNavigation
    {
        public Guid JobAdId { get; private set; }
        public string JobAdTitle { get; private set; }
        public JobAdStatus JobAdStatus { get; private set; }

        public SuggestedCandidatesNavigation(Guid jobAdId, string jobAdTitle, JobAdStatus jobAdStatus, MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
            : base(criteria, presentation)
        {
            JobAdId = jobAdId;
            JobAdTitle = jobAdTitle;
            JobAdStatus = jobAdStatus;
        }
    }

    public class ManageCandidatesNavigation
        : CandidatesNavigation
    {
        public Guid JobAdId { get; private set; }
        public JobAdStatus JobAdStatus { get; private set; }
        public ApplicantStatus ApplicantStatus { get; private set; }

        public ManageCandidatesNavigation(Guid jobAdId, JobAdStatus jobAdStatus, ApplicantStatus applicantStatus, CandidatesPresentationModel presentation)
            : base(presentation)
        {
            JobAdId = jobAdId;
            JobAdStatus = jobAdStatus;
            ApplicantStatus = applicantStatus;
        }
    }

    public class BrowseCandidatesNavigation
        : CandidatesNavigation
    {
        public IUrlNamedLocation Location{ get; private set; }
        public Salary SalaryBand { get; private set; }

        public BrowseCandidatesNavigation(IUrlNamedLocation location, Salary salaryBand, CandidatesPresentationModel presentation)
            : base(presentation)
        {
            Location = location;
            SalaryBand = salaryBand;
        }
    }

    public class FlagListNavigation
        : CandidatesNavigation
    {
        public FlagListNavigation(CandidatesPresentationModel presentation)
            : base(presentation)
        {
        }
    }

    public class BlockListNavigation
        : CandidatesNavigation
    {
        public BlockListType BlockListType { get; private set; }

        public BlockListNavigation(BlockListType blockListType, CandidatesPresentationModel presentation)
            : base(presentation)
        {
            BlockListType = blockListType;
        }
    }

    public class EmployerContext
        : UserContext
    {
        private static class SessionKeys
        {
            public static readonly string CurrentSearch = typeof(EmployerContext).FullName + ".CurrentSearch";
            public static readonly string IsNewSearch = typeof(EmployerContext).FullName + ".IsNewSearch";
            public static readonly string Searches = typeof(EmployerContext).FullName + ".Searches";
            public static readonly string Viewings = typeof(EmployerContext).FullName + ".Viewings";
            public static readonly string CanShowHelpPrompt = typeof(EmployerContext).FullName + ".CanShowHelpPrompt";
            public static readonly string ShownHelpPrompt = typeof(EmployerContext).FullName + ".ShownHelpPrompt";
            public static readonly string CurrentCandidates = typeof(EmployerContext).FullName + ".CurrentCandidates";
            public static readonly string SavedSearchName = typeof(EmployerContext).FullName + ".SavedSearchName";
        }

        private static class ProfileKeys
        {
            public const string EmployerProfile = "EmployerProfile";
        }

        public EmployerContext(HttpContextBase context)
            : base(context)
        {
        }

        public EmployerContext(HttpContext context)
            : base(context)
        {
        }

        public MemberSearchNavigation CurrentSearch
        {
            set
            {
                Session.Set(SessionKeys.CurrentSearch, value);
                CurrentCandidates = value;
                IsNewSearch = false;
            }
            get
            {
                return Session.Get<MemberSearchNavigation>(SessionKeys.CurrentSearch);
            }
        }

        public bool IsNewSearch
        {
            set { Session.Set(SessionKeys.IsNewSearch, value); }
            get { return Session.GetBoolean(SessionKeys.IsNewSearch); }
        }

        public int Searches
        {
            set { Session.Set(SessionKeys.Searches, value); }
            get { return Session.GetInt32(SessionKeys.Searches); }
        }

        public int Viewings
        {
            set { Session.Set(SessionKeys.Viewings, value); }
            get { return Session.GetInt32(SessionKeys.Viewings); }
        }

        public bool CanShowHelpPrompt
        {
            set { Session.Set(SessionKeys.CanShowHelpPrompt, value); }
            get { return Session.GetBoolean(SessionKeys.CanShowHelpPrompt); }
        }

        public bool ShownHelpPrompt
        {
            set { Session.Set(SessionKeys.ShownHelpPrompt, value); }
            get { return Session.GetBoolean(SessionKeys.ShownHelpPrompt); }
        }

        public string SavedSearchName
        {
            set { Session.Set(SessionKeys.SavedSearchName, value); }
            get { return Session.Get<string>(SessionKeys.SavedSearchName); }
        }

        public CandidatesNavigation CurrentCandidates
        {
            set { Session.Set(SessionKeys.CurrentCandidates, value); }
            get { return Session.Get<CandidatesNavigation>(SessionKeys.CurrentCandidates); }
        }

        public void HideUpdatedTermsReminder()
        {
            var employerProfile = Profile.EmployerProfile;
            if (!employerProfile.UpdatedTermsReminder.Hide)
            {
                employerProfile.UpdatedTermsReminder.Hide = true;
                Profile.EmployerProfile = employerProfile;
            }
        }

        public bool ShowUpdatedTermsReminder()
        {
            return ShowUpdatedTermsReminder(ProfileKeys.EmployerProfile, UserType.Employer);
        }

        public bool IsCreditReminderHidden
        {
            get { return Profile.EmployerProfile.HideCreditReminder; }
        }

        public bool IsBulkCreditReminderHidden
        {
            get { return Profile.EmployerProfile.HideBulkCreditReminder; }
        }

        public void HideCreditReminder()
        {
            var employerProfile = Profile.EmployerProfile;
            employerProfile.HideCreditReminder = true;
            Profile.EmployerProfile = employerProfile;
        }

        public void HideBulkCreditReminder()
        {
            var employerProfile = Profile.EmployerProfile;
            employerProfile.HideBulkCreditReminder = true;
            Profile.EmployerProfile = employerProfile;
        }
    }

    public static class EmployerContextExtensions
    {
        public static EmployerContext GetEmployerContext(this HttpContextBase context)
        {
            return new EmployerContext(context);
        }
    }
}