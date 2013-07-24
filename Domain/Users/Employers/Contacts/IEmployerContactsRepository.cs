using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Employers.Contacts
{
    public interface IEmployerContactsRepository
    {
        void CreateMessage(Guid employerId, Guid memberId, Guid? representativeId, Guid? unlockingId, MemberMessage message);
        void CreateMessageAttachment(Guid employerId, MemberMessageAttachment attachment);
        void DeleteMessageAttachment(Guid employerId, Guid id);

        MemberMessage GetMessage(Guid employerId, Guid id);
        IList<MemberMessage> GetMessages(Guid employerId, Guid memberId);
        MemberMessageAttachment GetMessageAttachment(Guid employerId, Guid id);
        IList<MemberMessageAttachment> GetMessageAttachments(Guid employerId, IEnumerable<Guid> ids);

        Guid? GetMemberMessageRepresentative(Guid id);
    }
}
