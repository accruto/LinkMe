using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public abstract class CreditsEmail
        : EmployerEmail
    {
        private readonly IList<ICommunicationUser> _blindCopy;

        protected CreditsEmail(ICommunicationUser to, ICommunicationUser accountManager)
            : base(to)
        {
            if (accountManager != null)
                _blindCopy = new List<ICommunicationUser> { accountManager };
        }

        public override IList<ICommunicationUser> BlindCopy
        {
            get { return _blindCopy; }
        }
    }
}
