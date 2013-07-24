using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails.EmployerClientEmails
{
    public abstract class EmployerClientEmail
        : EmployerEmail
    {
        protected EmployerClientEmail(ICommunicationUser to, ICommunicationUser accountManager)
            : base(to, GetFromUser(accountManager))
        {
            if (accountManager == null)
                throw new ArgumentNullException("accountManager");

            AccountManager = accountManager;
        }

        protected ICommunicationUser AccountManager { get; set; }

        private static ICommunicationUser GetFromUser(ICommunicationUser accountManager)
        {
            // If the account manager's account is disabled send from the client services address instead.

            return accountManager.IsEnabled ? accountManager : null;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("accountManagerName", AccountManager.FullName);
        }
    }
}