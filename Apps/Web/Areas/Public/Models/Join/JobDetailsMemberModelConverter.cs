using System;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;

namespace LinkMe.Web.Areas.Public.Models.Join
{
    public class JobDetailsMemberModelConverter
        : Converter<JobDetailsMemberModel>
    {
        public override void Convert(JobDetailsMemberModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override JobDetailsMemberModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new JobDetailsMemberModel
            {
                JobTitle = values.GetStringValue("JobTitle"),
                JobCompany = values.GetStringValue("JobCompany"),
                IndustryIds = values.GetGuidArrayValue("IndustryIds"),
                RecentProfession = values.GetValue<Profession>("RecentProfession"),
                RecentSeniority = values.GetValue<Seniority>("RecentSeniority"),
                HighestEducationLevel = values.GetValue<EducationLevel>("HighestEducationLevel"),
                DesiredJobTitle = values.GetStringValue("DesiredJobTitle"),
                DesiredJobTypes = values.GetFlagsValue<JobTypes>() ?? Defaults.DesiredJobTypes,
                EthnicStatus = values.GetFlagsValue<EthnicStatus>() ?? Defaults.EthnicStatus,
                Gender = values.GetValue<Gender>("Gender") ?? Defaults.Gender,
                DateOfBirth = new PartialDateConverter(true).Deconvert("DateOfBirth", values),
                Citizenship = values.GetStringValue("Citizenship"),
                VisaStatus = values.GetValue<VisaStatus>("VisaStatus"),
                RelocationPreference = values.GetValue<RelocationPreference>("RelocationPreference") ?? Defaults.RelocationPreference,
                RelocationCountryIds = values.GetIntArrayValue("RelocationCountryIds"),
                RelocationCountryLocationIds = values.GetIntArrayValue("RelocationCountryLocationIds"),
                SecondaryEmailAddress = values.GetStringValue("SecondaryEmailAddress"),
                SecondaryPhoneNumber = values.GetStringValue("SecondaryPhoneNumber"),
                SecondaryPhoneNumberType = values.GetValue<PhoneNumberType>("SecondaryPhoneNumberType") ?? Defaults.SecondaryPhoneNumberType,
            };
        }
    }
}
