using System;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;

namespace LinkMe.Web.Areas.Public.Models.Join
{
    public class PersonalDetailsMemberModelConverter
        : Converter<PersonalDetailsMemberModel>
    {
        public override void Convert(PersonalDetailsMemberModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override PersonalDetailsMemberModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new PersonalDetailsMemberModel
            {
                FirstName = values.GetStringValue("FirstName"),
                LastName = values.GetStringValue("LastName"),
                EmailAddress = values.GetStringValue("EmailAddress"),
                CountryId = values.GetIntValue("CountryId") ?? 0,
                Location = values.GetStringValue("Location"),
                PhoneNumber = values.GetStringValue("PhoneNumber"),
                PhoneNumberType = values.GetValue<PhoneNumberType>("PhoneNumberType") ?? Defaults.PrimaryPhoneNumberType,
                Status = values.GetValue<CandidateStatus>("Status"),
                SalaryLowerBound = values.GetDecimalValue("SalaryLowerBound"),
                SalaryRate = values.GetValue<SalaryRate>("SalaryRate") ?? Defaults.DesiredSalaryRate,
                Visibility = BindVisibility(values),
            };
        }

        private static ProfessionalVisibility BindVisibility(IGetValues values)
        {
            var visibility = ProfessionalVisibility.None;

            var resumeVisibility = values.GetBooleanValue("ResumeVisibility");
            if (resumeVisibility != null && resumeVisibility.Value)
            {
                visibility = visibility.SetFlag(ProfessionalVisibility.Resume);

                var nameVisibility = values.GetBooleanValue("NameVisibility");
                var phoneNumbersVisibility = values.GetBooleanValue("PhoneNumbersVisibility");
                var recentEmployersVisibility = values.GetBooleanValue("RecentEmployersVisibility");

                if (nameVisibility != null && nameVisibility.Value)
                    visibility = visibility.SetFlag(ProfessionalVisibility.Name);
                if (phoneNumbersVisibility != null && phoneNumbersVisibility.Value)
                    visibility = visibility.SetFlag(ProfessionalVisibility.PhoneNumbers);
                if (recentEmployersVisibility != null && recentEmployersVisibility.Value)
                    visibility = visibility.SetFlag(ProfessionalVisibility.RecentEmployers);
            }

            // Salary is back to front.

            var notSalaryVisibility = values.GetBooleanValue("NotSalaryVisibility");
            if (notSalaryVisibility == null || !notSalaryVisibility.Value)
                visibility = visibility.SetFlag(ProfessionalVisibility.Salary);

            return visibility;
        }
    }
}
