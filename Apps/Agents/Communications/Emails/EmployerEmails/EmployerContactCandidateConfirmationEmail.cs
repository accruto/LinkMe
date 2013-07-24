using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class EmployerContactCandidateConfirmationEmail
        : EmployerEmail
    {
        private readonly Guid _resumeId;
        private readonly string _candidateName;
        private readonly string _subject;
        private readonly string _content;

        public EmployerContactCandidateConfirmationEmail(string toEmailAddress, IEmployer to, Guid resumeId, string candidateName, string subject, string content)
            : base(GetEmployer(to, toEmailAddress))
        {
            _resumeId = resumeId;
            _candidateName = candidateName;
            _subject = subject;
            _content = content;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Subject", _subject);
            properties.Add("Content", _content);
            properties.Add("MemberDisplayName", _candidateName);
            properties.Add("ResumeId", _resumeId);
            properties.Add("Date", DateTime.Now.ToShortDateString());
        }
    }
}