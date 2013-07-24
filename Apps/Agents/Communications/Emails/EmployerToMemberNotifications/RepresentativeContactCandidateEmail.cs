using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications
{
    public class RepresentativeContactCandidateEmail
        : EmployerToMemberNotification
    {
        private readonly IMember _representee;
        private readonly string _subject;
        private readonly string _content;
        private readonly IOrganisation _organisation;

        public RepresentativeContactCandidateEmail(ICommunicationUser member, string employerEmailAddress, IEmployer employer, IMember representee, string subject, string content)
            : base(member, GetEmployer(employer, employerEmailAddress))
        {
            _representee = representee;
            _subject = subject;
            _content = content;
            _organisation = employer.Organisation;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Representee", _representee, typeof(ICommunicationUser));
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