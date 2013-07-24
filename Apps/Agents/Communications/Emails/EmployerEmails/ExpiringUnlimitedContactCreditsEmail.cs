using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class ExpiringUnlimitedContactCreditsEmail
        : CreditsEmail
    {
        private readonly ICommunicationUser _accountManager;

        public ExpiringUnlimitedContactCreditsEmail(ICommunicationUser to, ICommunicationUser accountManager)
            : base(to, accountManager)
        {
            _accountManager = accountManager;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("AccountManager", _accountManager);
        }
    }
}
