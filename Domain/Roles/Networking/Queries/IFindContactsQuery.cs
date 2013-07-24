using System.Collections.Generic;
using LinkMe.Framework.Communications;

namespace LinkMe.Domain.Roles.Networking.Queries
{
    public interface IFindContactsQuery
    {
        IList<ICommunicationRecipient> FindContacts();
    }
}
