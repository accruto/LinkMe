namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class MemberStatusKeys
    {
        public const string PercentComplete = "PercentComplete";
        public const string Age = "Age";
        public const string PromptForResumeUpdate = "PromptForResumeUpdate";
        public const string MemberStatus = "MemberStatus";

        public const string IsDesiredJobComplete = "IsDesiredJobComplete";
        public const string IsDesiredSalaryComplete = "IsDesiredSalaryComplete";
        public const string IsAddressComplete = "IsAddressComplete";
        public const string IsEmailComplete = "IsEmailAddressComplete";
        public const string IsPhoneComplete = "IsPhoneNumberComplete";
        public const string IsStatusComplete = "IsStatusComplete";
        public const string IsObjectiveComplete = "IsObjectiveComplete";
        public const string IsIndustriesComplete = "IsIndustriesComplete";
        public const string IsJobsComplete = "IsJobsComplete";
        public const string IsSchoolsComplete = "IsSchoolsComplete";
        public const string IsRecentProfessionComplete = "IsRecentProfessionComplete";
        public const string IsRecentSeniorityComplete = "IsRecentSeniorityComplete";
        public const string IsHighestEducationComplete = "IsHighestEducationLevelComplete";
        public const string IsVisaStatusComplete = "IsVisaStatusComplete";
    }

    public class VisibilityKeys
    {
        public const string ShowResume = "ShowResume";
        public const string ShowName = "ShowName";
        public const string ShowPhoneNumbers = "ShowPhoneNumbers";
        public const string ShowProfilePhoto = "ShowProfilePhoto";
        public const string ShowRecentEmployers = "ShowRecentEmployers";
    }

    public class ContactDetailsKeys
    {
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string CountryId = "CountryId";
        public const string Location = "Location";
        public const string EmailAddress = "EmailAddress";
        public const string SecondaryEmailAddress = "SecondaryEmailAddress";
        public const string PhoneNumber = "PhoneNumber";
        public const string PhoneNumberType = "PhoneNumberType";
        public const string SecondaryPhoneNumber = "SecondaryPhoneNumber";
        public const string SecondaryPhoneNumberType = "SecondaryPhoneNumberType";
        public const string Citizenship = "Citizenship";
        public const string VisaStatus = "VisaStatus";
        public const string EthnicStatus = "EthnicStatus";
        public const string Aboriginal = "Aboriginal";
        public const string TorresIslander = "TorresIslander";
        public const string Gender = "Gender";
        public const string DateOfBirth = "DateOfBirth";
        public const string DateOfBirthMonth = "DateOfBirthMonth";
        public const string DateOfBirthYear = "DateOfBirthYear";
        public const string PhotoId = "PhotoId";
    }

    public class DesiredJobKeys
    {
        public const string DesiredJobTitle = "DesiredJobTitle";
        public const string DesiredJobTypes = "DesiredJobTypes";
        public const string Status = "Status";
        public const string Contract = "Contract";
        public const string FullTime = "FullTime";
        public const string JobShare = "JobShare";
        public const string PartTime = "PartTime";
        public const string Temp = "Temp";
        public const string DesiredSalaryLowerBound = "DesiredSalaryLowerBound";
        public const string DesiredSalaryRate = "DesiredSalaryRate";
        public const string IsSalaryNotVisible = "IsSalaryNotVisible";
        public const string SendSuggestedJobs = "SendSuggestedJobs";
        public const string RelocationPreference = "RelocationPreference";
        public const string RelocationCountryIds = "RelocationCountryIds";
        public const string RelocationCountryLocationIds = "RelocationCountryLocationIds";
    }

    public class CareerObjectivesKeys
    {
        public const string Objective = "Objective";
        public const string Summary = "Summary";
        public const string Skills = "Skills";
    }

    public class EmploymentHistoryKeys
    {
        public const string Id = "Id";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string Company = "Company";
        public const string Title = "Title";
        public const string Description = "Description";
        public const string IndustryIds = "IndustryIds";
        public const string RecentProfession = "RecentProfession";
        public const string RecentSeniority = "RecentSeniority";
        public const string IsCurrent = "IsCurrent";
        public const string Jobs = "Jobs";
        public const string StartDateMonth = "StartDateMonth";
        public const string StartDateYear = "StartDateYear";
        public const string EndDateMonth = "EndDateMonth";
        public const string EndDateYear = "EndDateYear";
    }

    public class EducationKeys
    {
        public const string HighestEducationLevel = "HighestEducationLevel";
        public const string Id = "Id";
        public const string EndDate = "EndDate";
        public const string Degree = "Degree";
        public const string Major = "Major";
        public const string Institution = "Institution";
        public const string City = "City";
        public const string Description = "Description";
        public const string Schools = "Schools";
        public const string EndDateMonth = "EndDateMonth";
        public const string EndDateYear = "EndDateYear";
        public const string IsCurrent = "IsCurrent";
    }

    public class OtherKeys
    {
        public const string Courses = "Courses";
        public const string Awards = "Awards";
        public const string Professional = "Professional";
        public const string Interests = "Interests";
        public const string Affiliations = "Affiliations";
        public const string Other = "Other";
        public const string Referees = "Referees";
    }
}