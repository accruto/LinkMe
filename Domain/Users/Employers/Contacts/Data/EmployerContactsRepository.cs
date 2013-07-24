using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Users.Employers.Contacts.Data
{
    public class EmployerContactsRepository
        : Repository, IEmployerContactsRepository
    {
        private static readonly DataLoadOptions MessageLoadOptions = DataOptions.CreateLoadOptions<EmployerMemberMessageEntity>(m => m.EmployerMemberMessageAttachmentEntities);

        private static readonly Func<ContactsDataContext, Guid, Guid, MemberMessage> GetMessageQuery
            = CompiledQuery.Compile((ContactsDataContext dc, Guid employerId, Guid id)
                => (from m in dc.EmployerMemberMessageEntities
                    where m.employerId == employerId
                    && m.id == id
                    select m.Map()).SingleOrDefault());

        private static readonly Func<ContactsDataContext, Guid, Guid, IQueryable<MemberMessage>> GetMessagesQuery
            = CompiledQuery.Compile((ContactsDataContext dc, Guid employerId, Guid memberId)
                => from m in dc.EmployerMemberMessageEntities
                   where m.employerId == employerId
                   && m.memberId == memberId
                   select m.Map());

        private static readonly Func<ContactsDataContext, Guid, Guid?> GetMessageRepresentative
            = CompiledQuery.Compile((ContactsDataContext dc, Guid id)
                => (from m in dc.EmployerMemberMessageEntities
                    where m.id == id
                    select m.representativeId).SingleOrDefault());

        private static readonly Func<ContactsDataContext, Guid, Guid, EmployerMemberAttachmentEntity> GetEmployerMemberAttachmentEntity
            = CompiledQuery.Compile((ContactsDataContext dc, Guid employerId, Guid id)
                => (from m in dc.EmployerMemberAttachmentEntities
                    where m.employerId == employerId
                    && m.id == id
                    select m).SingleOrDefault());

        private static readonly Func<ContactsDataContext, Guid, Guid, MemberMessageAttachment> GetMessageAttachment
            = CompiledQuery.Compile((ContactsDataContext dc, Guid employerId, Guid id)
                => (from m in dc.EmployerMemberAttachmentEntities
                    where m.employerId == employerId
                    && m.id == id
                    select m.Map()).SingleOrDefault());

        private static readonly Func<ContactsDataContext, Guid, string, IQueryable<MemberMessageAttachment>> GetMessageAttachments
            = CompiledQuery.Compile((ContactsDataContext dc, Guid employerId, string ids)
                => from m in dc.EmployerMemberAttachmentEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on m.id equals i.value
                   where m.employerId == employerId
                   select m.Map());

        public EmployerContactsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IEmployerContactsRepository.CreateMessage(Guid employerId, Guid memberId, Guid? representativeId, Guid? unlockingId, MemberMessage message)
        {
            using (var dc = CreateContext())
            {
                dc.EmployerMemberMessageEntities.InsertOnSubmit(message.Map(employerId, memberId, representativeId, unlockingId));
                dc.SubmitChanges();
            }
        }

        void IEmployerContactsRepository.CreateMessageAttachment(Guid employerId, MemberMessageAttachment attachment)
        {
            using (var dc = CreateContext())
            {
                dc.EmployerMemberAttachmentEntities.InsertOnSubmit(attachment.Map(employerId));
                dc.SubmitChanges();
            }
        }

        void IEmployerContactsRepository.DeleteMessageAttachment(Guid employerId, Guid attachmentId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetEmployerMemberAttachmentEntity(dc, employerId, attachmentId);
                if (entity != null)
                {
                    dc.EmployerMemberAttachmentEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        MemberMessage IEmployerContactsRepository.GetMessage(Guid employerId, Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberMessage(dc, employerId, id);
            }
        }

        IList<MemberMessage> IEmployerContactsRepository.GetMessages(Guid employerId, Guid memberId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberMessages(dc, employerId, memberId).ToList();
            }
        }

        MemberMessageAttachment IEmployerContactsRepository.GetMessageAttachment(Guid employerId, Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMessageAttachment(dc, employerId, id);
            }
        }

        IList<MemberMessageAttachment> IEmployerContactsRepository.GetMessageAttachments(Guid employerId, IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMessageAttachments(dc, employerId, new SplitList<Guid>(ids).ToString()).ToList();
            }
        }

        Guid? IEmployerContactsRepository.GetMemberMessageRepresentative(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMessageRepresentative(dc, id);
            }
        }

        private static MemberMessage GetMemberMessage(ContactsDataContext dc, Guid employerId, Guid id)
        {
            dc.LoadOptions = MessageLoadOptions;
            return GetMessageQuery(dc, employerId, id);
        }

        private static IEnumerable<MemberMessage> GetMemberMessages(ContactsDataContext dc, Guid employerId, Guid memberId)
        {
            dc.LoadOptions = MessageLoadOptions;
            return GetMessagesQuery(dc, employerId, memberId);
        }

        private ContactsDataContext CreateContext()
        {
            return CreateContext(c => new ContactsDataContext(c));
        }
    }
}
