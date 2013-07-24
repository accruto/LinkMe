using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications
{
    public class ContactCandidateEmail
        : EmployerToMemberNotification
    {
        private readonly string _subject;
        private readonly string _content;
        private readonly IOrganisation _organisation;

        public ContactCandidateEmail(ICommunicationUser member, string employerEmailAddress, IEmployer employer, string subject, string content)
            : base(member, GetEmployer(employer, employerEmailAddress))
        {
            _subject = subject;
            _content = content;
            _organisation = employer.Organisation;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Subject", _subject);
            properties.Add("Content", _content);
            properties.Add("Organisation", _organisation, typeof(Organisation));
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}