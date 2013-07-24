using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;

namespace LinkMe.Apps.Agents.Users.Members
{
    public static class Defaults
    {
        public static readonly CandidateStatus CandidateStatus = CandidateStatus.ActivelyLooking;
        public const PhoneNumberType PrimaryPhoneNumberType = PhoneNumberType.Mobile;
        public const PhoneNumberType SecondaryPhoneNumberType = PhoneNumberType.Mobile;
        public static readonly EthnicStatus EthnicStatus = EthnicStatus.None;
        public static readonly Gender Gender = Gender.Unspecified;
        public static readonly SalaryRate DesiredSalaryRate = SalaryRate.Year;
        public const bool SalaryVisibility = true;
        public const JobTypes DesiredJobTypes = JobTypes.None;
        public static RelocationPreference RelocationPreference = RelocationPreference.No;
        public const bool SendSuggestedJobs = true;
    }
}
