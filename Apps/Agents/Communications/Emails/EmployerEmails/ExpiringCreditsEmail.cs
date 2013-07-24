using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public abstract class ExpiringCreditsEmail
        : CreditsEmail
    {
        private readonly int _quantity;

        protected ExpiringCreditsEmail(ICommunicationUser to, ICommunicationUser accountManager, int quantity)
            : base(to, accountManager)
        {
            _quantity = quantity;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Quantity", _quantity);
        }
    }
}
