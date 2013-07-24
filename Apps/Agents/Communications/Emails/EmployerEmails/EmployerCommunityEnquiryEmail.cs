using System;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class EmployerCommunityEnquiryEmail
        : EmployerEmail
    {
        private readonly Community _community;
        private readonly AffiliationEnquiry _enquiry;

        public EmployerCommunityEnquiryEmail(Community community, AffiliationEnquiry enquiry)
            : base(GetUnregisteredEmployer(enquiry.EmailAddress, enquiry.FirstName, enquiry.LastName))
        {
            _community = community;
            _enquiry = enquiry;
        }

        public override Guid? AffiliateId
        {
            get { return _community == null ? (Guid?)null : _community.Id; }
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Community", _community);
            properties.Add("Enquiry", _enquiry);
        }
    }
}