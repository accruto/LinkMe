using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class EmployerUsageEmail
        : EmployerEmail
    {
        private readonly int _newCandidates;

        public EmployerUsageEmail(ICommunicationUser employer, int newCandidates)
            : base(employer)
        {
            _newCandidates = newCandidates;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("NewCandidates", _newCandidates);
        }
    }
}