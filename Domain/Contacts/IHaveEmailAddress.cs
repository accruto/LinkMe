using System.Collections.Generic;

namespace LinkMe.Domain.Contacts
{
    public interface IHaveEmailAddress
    {
        EmailAddress EmailAddress { get; set; }
    }

    public interface IHaveEmailAddresses
    {
        IList<EmailAddress> EmailAddresses { get; set; }
    }
}
