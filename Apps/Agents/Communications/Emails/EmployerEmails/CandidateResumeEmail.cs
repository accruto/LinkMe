using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class CandidateResumeEmail
        : EmployerEmail
    {
        private readonly EmployerMemberView _view;

        public CandidateResumeEmail(ICommunicationUser to, EmployerMemberView view)
            : base(to)
        {
            _view = view;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("MemberName", _view.FullName ?? "[Name hidden]");
            properties.Add("Member", _view);
        }
    }
}