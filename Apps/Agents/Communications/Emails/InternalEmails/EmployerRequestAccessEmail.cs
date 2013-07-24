using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public class EmployerRequestAccessEmail
        : EmployerServicesEmail
    {
        private readonly string _contactName;
        private readonly string _contactPhone;
        private readonly string _contactEmail;
        private readonly string _companyName;
        private readonly string _message;

        public EmployerRequestAccessEmail(string contactName, string contactPhone, string contactEmail, string companyName, string message)
        {
            _contactName = contactName;
            _contactPhone = contactPhone;
            _contactEmail = contactEmail;
            _companyName = companyName;
            _message = message;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("ContactName", _contactName);
            properties.Add("ContactPhone", _contactPhone);
            properties.Add("ContactEmail", _contactEmail);
            properties.Add("CompanyName", _companyName);
            properties.Add("Message", _message);
        }
    }
}