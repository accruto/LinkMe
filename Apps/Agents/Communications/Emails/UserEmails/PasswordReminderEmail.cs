using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.UserEmails
{
    public class PasswordReminderEmail
        : UserEmail
    {
        private readonly string _loginId;
        private readonly string _password;

        public PasswordReminderEmail(ICommunicationUser to, string loginId, string password)
            : base(to)
        {
            _loginId = loginId;
            _password = password;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("LoginId", _loginId);
            properties.Add("Password", _password);
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}