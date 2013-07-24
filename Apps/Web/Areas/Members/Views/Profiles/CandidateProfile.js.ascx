<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MemberModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Profiles"%>

<script type="text/javascript">
var currentRequest = null;

    var candidateProfileModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Status) %>;
    var candidateVisibilityModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Visibility) %>;

var candidateProfileKeys = {
    MemberStatusKeys : {
        PercentComplete : "<%= MemberStatusKeys.PercentComplete %>",
        PromptForResumeUpdate : "<%= MemberStatusKeys.PromptForResumeUpdate %>",
        Age : "<%= MemberStatusKeys.Age %>",
        MemberStatus : "<%= MemberStatusKeys.MemberStatus %>",
        IsDesiredJobComplete : "<%= MemberStatusKeys.IsDesiredJobComplete %>",
        IsDesiredSalaryComplete : "<%= MemberStatusKeys.IsDesiredSalaryComplete %>",
        IsAddressComplete : "<%= MemberStatusKeys.IsAddressComplete %>",
        IsEmailComplete : "<%= MemberStatusKeys.IsEmailComplete %>",
        IsPhoneComplete : "<%= MemberStatusKeys.IsPhoneComplete %>",
        IsStatusComplete : "<%= MemberStatusKeys.IsStatusComplete %>",
        IsObjectiveComplete : "<%= MemberStatusKeys.IsObjectiveComplete %>",
        IsIndustriesComplete : "<%= MemberStatusKeys.IsIndustriesComplete %>",
        IsJobsComplete : "<%= MemberStatusKeys.IsJobsComplete %>",
        IsSchoolsComplete : "<%= MemberStatusKeys.IsSchoolsComplete %>",
        IsRecentProfessionComplete : "<%= MemberStatusKeys.IsRecentProfessionComplete %>",
        IsRecentSeniorityComplete : "<%= MemberStatusKeys.IsRecentSeniorityComplete %>",
        IsHighestEducationComplete : "<%= MemberStatusKeys.IsHighestEducationComplete %>",
        IsVisaStatusComplete : "<%= MemberStatusKeys.IsVisaStatusComplete %>"
    },
    VisibilityKeys : {
        ShowResume : "<%= VisibilityKeys.ShowResume %>",
        ShowName : "<%= VisibilityKeys.ShowName %>",
        ShowPhoneNumbers : "<%= VisibilityKeys.ShowPhoneNumbers %>",
        ShowProfilePhoto : "<%= VisibilityKeys.ShowProfilePhoto %>",
        ShowRecentEmployers : "<%= VisibilityKeys.ShowRecentEmployers %>"
    },
    ContactDetailsKeys : {
        FirstName : "<%= ContactDetailsKeys.FirstName%>",
        LastName : "<%= ContactDetailsKeys.LastName%>",
        CountryId : "<%= ContactDetailsKeys.CountryId%>",
        Location : "<%= ContactDetailsKeys.Location%>",
        EmailAddress : "<%= ContactDetailsKeys.EmailAddress%>",
        SecondaryEmailAddress : "<%= ContactDetailsKeys.SecondaryEmailAddress%>",
        PhoneNumber : "<%= ContactDetailsKeys.PhoneNumber%>",
        PhoneNumberType : "<%= ContactDetailsKeys.PhoneNumberType%>",
        SecondaryPhoneNumber : "<%= ContactDetailsKeys.SecondaryPhoneNumber%>",
        SecondaryPhoneNumberType : "<%= ContactDetailsKeys.SecondaryPhoneNumberType%>",
        Citizenship : "<%= ContactDetailsKeys.Citizenship%>",
        VisaStatus : "<%= ContactDetailsKeys.VisaStatus%>",
        EthnicStatus : "<%= ContactDetailsKeys.EthnicStatus%>",
        Aboriginal : "<%= ContactDetailsKeys.Aboriginal%>",
        TorresIslander : "<%= ContactDetailsKeys.TorresIslander%>",
        Gender : "<%= ContactDetailsKeys.Gender%>",
        DateOfBirthMonth : "<%= ContactDetailsKeys.DateOfBirthMonth%>",
        DateOfBirthYear : "<%= ContactDetailsKeys.DateOfBirthYear%>",
        PhotoId : "<%= ContactDetailsKeys.PhotoId %>"
    },
    DesiredJobKeys : {
        DesiredJobTitle : "<%= DesiredJobKeys.DesiredJobTitle%>",
        DesiredJobTypes : "<%= DesiredJobKeys.DesiredJobTypes%>",
		Contract : "<%= DesiredJobKeys.Contract%>",
		FullTime : "<%= DesiredJobKeys.FullTime%>",
		JobShare : "<%= DesiredJobKeys.JobShare%>",
		PartTime : "<%= DesiredJobKeys.PartTime%>",
		Temp : "<%= DesiredJobKeys.Temp%>",
        Status : "<%= DesiredJobKeys.Status%>",
        DesiredSalaryLowerBound : "<%= DesiredJobKeys.DesiredSalaryLowerBound%>",
        DesiredSalaryRate : "<%= DesiredJobKeys.DesiredSalaryRate%>",
        IsSalaryNotVisible : "<%= DesiredJobKeys.IsSalaryNotVisible%>",
        EmailSuggestedJobs : "<%= DesiredJobKeys.SendSuggestedJobs%>",
        RelocationPreference : "<%= DesiredJobKeys.RelocationPreference%>",
        RelocationCountryIds : "<%= DesiredJobKeys.RelocationCountryIds%>",
        RelocationCountryLocationIds : "<%= DesiredJobKeys.RelocationCountryLocationIds%>"
    },
    CareerObjectivesKeys : {
        Objective : "<%= CareerObjectivesKeys.Objective%>",
        Summary : "<%= CareerObjectivesKeys.Summary %>",
        Skills : "<%= CareerObjectivesKeys.Skills %>"
    },
    EmploymentHistoryKeys : {
        RecentProfession : "<%= EmploymentHistoryKeys.RecentProfession%>",
        RecentSeniority : "<%= EmploymentHistoryKeys.RecentSeniority%>",
        IndustryIds : "<%= EmploymentHistoryKeys.IndustryIds%>",
        Id : "<%= EmploymentHistoryKeys.Id %>",
        StartDate : "<%= EmploymentHistoryKeys.StartDate%>",
        EndDate : "<%= EmploymentHistoryKeys.EndDate%>",
        Title : "<%= EmploymentHistoryKeys.Title%>",
        Company : "<%= EmploymentHistoryKeys.Company%>",
        Description : "<%= EmploymentHistoryKeys.Description%>",
        IsCurrent : "<%= EmploymentHistoryKeys.IsCurrent %>",
        Jobs : "<%= EmploymentHistoryKeys.Jobs %>",
        StartDateMonth : "<%= EmploymentHistoryKeys.StartDateMonth %>",
        StartDateYear : "<%= EmploymentHistoryKeys.StartDateYear %>",
        EndDateMonth : "<%= EmploymentHistoryKeys.EndDateMonth %>",
        EndDateYear : "<%= EmploymentHistoryKeys.EndDateYear %>"
    },
    EducationKeys : {
        HighestEducationLevel : "<%= EducationKeys.HighestEducationLevel%>",
        Id : "<%= EducationKeys.Id %>",
        EndDate : "<%= EducationKeys.EndDate %>",
        Degree : "<%= EducationKeys.Degree %>",
        Major : "<%= EducationKeys.Major%>",
        Institution : "<%= EducationKeys.Institution%>",
        City : "<%= EducationKeys.City%>",
        Description : "<%= EducationKeys.Description%>",
        Schools : "<%= EducationKeys.Schools %>",
        EndDateMonth : "<%= EducationKeys.EndDateMonth %>",
        EndDateYear : "<%= EducationKeys.EndDateYear %>",
        IsCurrent : "<%= EducationKeys.IsCurrent %>"
    },
    OtherKeys : {
        Courses : "<%= OtherKeys.Courses%>",
        Awards : "<%= OtherKeys.Awards%>",
        Professional : "<%= OtherKeys.Professional%>",
        Interests : "<%= OtherKeys.Interests%>",
        Affiliations : "<%= OtherKeys.Affiliations%>",
        Other : "<%= OtherKeys.Other%>",
        Referees : "<%= OtherKeys.Referees%>"
    }
}
</script>
