using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class CandidateResumesEmail
        : EmployerEmail
    {
        private readonly EmployerMemberViews _views;

        public CandidateResumesEmail(ICommunicationUser to, EmployerMemberViews views)
            : base(to)
        {
            _views = views;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Members", _views);
        }
    }
}
