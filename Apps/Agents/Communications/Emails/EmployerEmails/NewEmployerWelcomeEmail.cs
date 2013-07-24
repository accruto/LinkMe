using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class NewEmployerWelcomeEmail
        : EmployerEmail
    {
        private readonly string _loginId;
        private readonly string _password;
        private readonly int _candidates;

        public NewEmployerWelcomeEmail(ICommunicationUser to, string loginId, string password, int candidates)
            : base(to)
        {
            _loginId = loginId;
            _password = password;
            _candidates = candidates;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("LoginId", _loginId);
            properties.Add("Password", _password);
            properties.Add("Candidates", _candidates);
        }
    }
}