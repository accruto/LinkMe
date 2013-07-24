using LinkMe.Apps.Agents.Users.Employers;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Accounts
{
    public class EmployerJoinModelConverter
        : Converter<EmployerJoinModel>
    {
        public override void Convert(EmployerJoinModel obj, ISetValues values)
        {
            values.SetValue("LoginId", obj.LoginId);
            values.SetValue("Password", obj.Password);
            values.SetValue("FirstName", obj.FirstName);
            values.SetValue("LastName", obj.LastName);
            values.SetValue("EmailAddress", obj.EmailAddress);
            values.SetValue("PhoneNumber", obj.PhoneNumber);
            values.SetValue("OrganisationName", obj.OrganisationName);
            values.SetValue("Location", obj.Location);
            values.SetValue("SubRole", obj.SubRole);
        }

        public override EmployerJoinModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new EmployerJoinModel
            {
                LoginId = values.GetStringValue("LoginId"),
                Password = values.GetStringValue("Password"),
                FirstName = values.GetStringValue("FirstName"),
                LastName = values.GetStringValue("LastName"),
                EmailAddress = values.GetStringValue("EmailAddress"),
                PhoneNumber = values.GetStringValue("PhoneNumber"),
                OrganisationName = values.GetStringValue("OrganisationName"),
                Location = values.GetStringValue("Location"),
                SubRole = values.GetValue<EmployerSubRole>("SubRole") ?? Defaults.SubRole,
            };
        }
    }
}
