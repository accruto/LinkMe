using System;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class ContactDetailsMemberModelConverter
        : Converter<ContactDetailsMemberModel>
    {
        public override void Convert(ContactDetailsMemberModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override ContactDetailsMemberModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new ContactDetailsMemberModel
            {
                Citizenship = values.GetStringValue(ContactDetailsKeys.Citizenship),
                CountryId = values.GetIntValue(ContactDetailsKeys.CountryId),
                DateOfBirth = new PartialDateConverter(true).Deconvert(ContactDetailsKeys.DateOfBirth, values),
                EmailAddress = values.GetStringValue(ContactDetailsKeys.EmailAddress),
                SecondaryEmailAddress = values.GetStringValue(ContactDetailsKeys.SecondaryEmailAddress),
                EthnicStatus = values.GetFlagsValue<EthnicStatus>() ?? Defaults.EthnicStatus,
                FirstName = values.GetStringValue(ContactDetailsKeys.FirstName),
                Gender = values.GetValue<Gender>(ContactDetailsKeys.Gender) ?? Defaults.Gender,
                LastName = values.GetStringValue(ContactDetailsKeys.LastName),
                Location = values.GetStringValue(ContactDetailsKeys.Location),
                PhoneNumber = values.GetStringValue(ContactDetailsKeys.PhoneNumber),
                PhoneNumberType = values.GetValue<PhoneNumberType>(ContactDetailsKeys.PhoneNumberType) ?? Defaults.PrimaryPhoneNumberType,
                SecondaryPhoneNumber = values.GetStringValue(ContactDetailsKeys.SecondaryPhoneNumber),
                SecondaryPhoneNumberType = values.GetValue<PhoneNumberType>(ContactDetailsKeys.SecondaryPhoneNumberType) ?? Defaults.SecondaryPhoneNumberType,
                VisaStatus = values.GetValue<VisaStatus>(ContactDetailsKeys.VisaStatus),
            };
        }
    }
}