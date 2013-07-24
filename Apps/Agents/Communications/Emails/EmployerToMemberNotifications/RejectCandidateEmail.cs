using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications
{
    public class RejectCandidateEmail
        : EmployerToMemberNotification
    {
        private readonly string _subject;
        private readonly string _content;

        public RejectCandidateEmail(ICommunicationUser to, string fromEmailAddress, IEmployer from, string subject, string content)
            : base(to, GetEmployer(from, fromEmailAddress))
        {
            _subject = subject;
            _content = content;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Subject", _subject);
            properties.Add("Content", _content);
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}