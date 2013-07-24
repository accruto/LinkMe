using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.Contacts.Queries
{
    public class MemberContactsQuery
        : IMemberContactsQuery
    {
        private readonly IMemberContactsRepository _repository;
        private readonly int _invitationAccessDays;

        public MemberContactsQuery(IMemberContactsRepository repository, int invitationAccessDays)
        {
            _repository = repository;
            _invitationAccessDays = invitationAccessDays;
        }

        Guid? IMemberContactsQuery.GetRepresentativeContact(Guid fromId)
        {
            return _repository.GetRepresentativeContact(fromId);
        }

        IDictionary<Guid, Guid?> IMemberContactsQuery.GetRepresentativeContacts(IEnumerable<Guid> fromIds)
        {
            return _repository.GetRepresentativeContacts(fromIds);
        }

        IList<Guid> IMemberContactsQuery.GetRepresenteeContacts(Guid fromId)
        {
            return _repository.GetRepresenteeContacts(fromId);
        }

        IList<Guid> IMemberContactsQuery.GetRepresenteeContacts(Guid fromId, char nameStartsWith)
        {
            return _repository.GetRepresenteeContacts(fromId, nameStartsWith);
        }

        bool IMemberContactsQuery.AreFirstDegreeContacts(Guid fromId, Guid toId)
        {
            return _repository.AreFirstDegreeContacts(fromId, toId);
        }

        Guid? IMemberContactsQuery.GetFirstDegreeContact(Guid fromId, string emailAddress)
        {
            return _repository.GetFirstDegreeContact(fromId, emailAddress);
        }

        IList<Guid> IMemberContactsQuery.GetFirstDegreeContacts(Guid fromId)
        {
            return _repository.GetFirstDegreeContacts(fromId);
        }

        IList<Guid> IMemberContactsQuery.GetFirstDegreeContacts(Guid fromId, string fullName, bool exactMatch)
        {
            return _repository.GetFirstDegreeContacts(fromId, fullName, exactMatch);
        }

        IList<Guid> IMemberContactsQuery.GetFirstDegreeContacts(Guid fromId, char nameStartsWith)
        {
            return _repository.GetFirstDegreeContacts(fromId, nameStartsWith);
        }

        IList<Guid> IMemberContactsQuery.GetFirstDegreeContacts(Guid fromId, bool withPhoto)
        {
            return _repository.GetFirstDegreeContacts(fromId, withPhoto);
        }

        IList<Guid> IMemberContactsQuery.GetFirstDegreeContacts(Guid fromId, PersonalVisibility visibility)
        {
            return _repository.GetFirstDegreeContacts(fromId, visibility);
        }

        bool IMemberContactsQuery.AreSecondDegreeContacts(Guid fromId, Guid toId)
        {
            return _repository.AreSecondDegreeContacts(fromId, toId);
        }

        IDictionary<Guid, IList<Guid>> IMemberContactsQuery.GetSecondDegreeContacts(Guid fromId, int minFirstDegreeContacts)
        {
            return _repository.GetSecondDegreesContacts(fromId, minFirstDegreeContacts);
        }

        Guid? IMemberContactsQuery.GetContact(Guid fromId, string emailAddress)
        {
            var minLastSentTime = DateTime.Now.AddDays(-1 * _invitationAccessDays);
            return _repository.GetContact(fromId, emailAddress, minLastSentTime);
        }

        IList<Guid> IMemberContactsQuery.GetContacts(IEnumerable<Guid> toIds, string fullName, bool exactMatch)
        {
            return _repository.GetContacts(toIds, fullName, exactMatch);
        }

        IList<Guid> IMemberContactsQuery.GetContacts(Guid fromId, IEnumerable<Guid> toIds)
        {
            var minLastSentTime = DateTime.Now.AddDays(-1 * _invitationAccessDays);
            return _repository.GetContacts(fromId, toIds, minLastSentTime);
        }

        IList<Guid> IMemberContactsQuery.GetContacts(Guid fromId, string fullName, bool exactMatch)
        {
            var minLastSentTime = DateTime.Now.AddDays(-1 * _invitationAccessDays);
            return _repository.GetContacts(fromId, fullName, exactMatch, minLastSentTime);
        }
    }
}