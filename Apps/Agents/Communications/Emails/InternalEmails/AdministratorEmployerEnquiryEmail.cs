using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public class AdministratorEmployerEnquiryEmail
        : EmployerServicesEmail
    {
        private readonly Community _community;
        private readonly IList<ICommunicationUser> _copy;
        private readonly AffiliationEnquiry _enquiry;

        public AdministratorEmployerEnquiryEmail(Community community, IEnumerable<IUser> administrators, AffiliationEnquiry enquiry)
        {
            _community = community;
            _enquiry = enquiry;

            if (administrators != null && administrators.Count() > 0)
                _copy = administrators.Cast<ICommunicationUser>().ToList();
        }

        public AdministratorEmployerEnquiryEmail(Community community, IEnumerable<EmailRecipient> administrators, AffiliationEnquiry enquiry)
        {
            _community = community;
            _enquiry = enquiry;

            if (administrators != null && administrators.Count() > 0)
                _copy = (from a in administrators select new UnregisteredAdministrator { EmailAddress = new EmailAddress { Address = a.Address, IsVerified = true }, FirstName = a.FirstName, LastName = a.LastName }).Cast<ICommunicationUser>().ToList();
        }

        public override IList<ICommunicationUser> Copy
        {
            get { return _copy; }
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Community", _community);
            properties.Add("Enquiry", _enquiry);
        }
    }
}