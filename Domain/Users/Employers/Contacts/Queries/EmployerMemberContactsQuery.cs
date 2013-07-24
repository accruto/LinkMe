using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Contacts.Queries
{
    public class EmployerMemberContactsQuery
        : IEmployerMemberContactsQuery
    {
        private readonly IEmployerContactsRepository _repository;

        public EmployerMemberContactsQuery(IEmployerContactsRepository repository)
        {
            _repository = repository;
        }

        MemberMessage IEmployerMemberContactsQuery.GetMessage(IEmployer employer, Guid id)
        {
            return employer == null
                ? null
                : _repository.GetMessage(employer.Id, id);
        }

        IList<MemberMessage> IEmployerMemberContactsQuery.GetMessages(IEmployer employer, Guid memberId)
        {
            return employer == null
                ? new List<MemberMessage>()
                : _repository.GetMessages(employer.Id, memberId);
        }

        MemberMessageAttachment IEmployerMemberContactsQuery.GetMessageAttachment(IEmployer employer, Guid id)
        {
            return employer == null
                ? null
                : _repository.GetMessageAttachment(employer.Id, id);
        }

        IList<MemberMessageAttachment> IEmployerMemberContactsQuery.GetMessageAttachments(IEmployer employer, IEnumerable<Guid> ids)
        {
            return employer == null
                ? new List<MemberMessageAttachment>()
                : _repository.GetMessageAttachments(employer.Id, ids);
        }
    }
}