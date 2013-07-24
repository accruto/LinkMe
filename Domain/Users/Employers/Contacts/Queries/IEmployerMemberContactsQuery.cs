using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Contacts.Queries
{
    public interface IEmployerMemberContactsQuery
    {
        MemberMessage GetMessage(IEmployer employer, Guid id);
        IList<MemberMessage> GetMessages(IEmployer employer, Guid memberId);

        MemberMessageAttachment GetMessageAttachment(IEmployer employer, Guid id);
        IList<MemberMessageAttachment> GetMessageAttachments(IEmployer employer, IEnumerable<Guid> ids);
    }
}