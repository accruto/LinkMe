using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public class ContactUsEmail
        : ServicesEmail
    {
        private readonly string _content;
        private readonly string _phoneNumber;
        private readonly Subcategory _subcategory;

        public ContactUsEmail(string emailAddress, string name, UserType userType, string phoneNumber, Subcategory subcategory, string content)
            : base(GetUser(emailAddress, name, null, userType))
        {
            _content = content;
            _phoneNumber = phoneNumber;
            _subcategory = subcategory;
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Content", _content);
            properties.Add("PhoneNumber", _phoneNumber);
            properties.Add("EnquiryType", _subcategory != null ? _subcategory.Name : "Report a site issue");
        }
    }
}