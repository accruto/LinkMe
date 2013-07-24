using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.Contacts
{
    public interface IMemberContactsRepository
    {
        Guid? GetRepresentativeContact(Guid fromId);
        IDictionary<Guid, Guid?> GetRepresentativeContacts(IEnumerable<Guid> fromIds);
        IList<Guid> GetRepresenteeContacts(Guid fromId);
        IList<Guid> GetRepresenteeContacts(Guid fromId, char nameStartsWith);

        bool AreFirstDegreeContacts(Guid fromId, Guid toId);
        Guid? GetFirstDegreeContact(Guid fromId, string emailAddress);
        IList<Guid> GetFirstDegreeContacts(Guid fromId);
        IList<Guid> GetFirstDegreeContacts(Guid fromId, string fullName, bool exactMatch);
        IList<Guid> GetFirstDegreeContacts(Guid fromId, char nameStartsWith);
        IList<Guid> GetFirstDegreeContacts(Guid fromId, bool withPhoto);
        IList<Guid> GetFirstDegreeContacts(Guid fromId, PersonalVisibility visibility);

        bool AreSecondDegreeContacts(Guid fromId, Guid toId);
        IDictionary<Guid, IList<Guid>> GetSecondDegreesContacts(Guid fromId, int minFirstDegreeContacts);

        Guid? GetContact(Guid fromId, string emailAddress, DateTime minLastSentTime);
        IList<Guid> GetContacts(IEnumerable<Guid> toIds, string fullName, bool exactMatch);
        IList<Guid> GetContacts(Guid fromId, IEnumerable<Guid> toIds, DateTime minLastSentTime);
        IList<Guid> GetContacts(Guid fromId, string fullName, bool exactMatch, DateTime minLastSentTime);
    }
}