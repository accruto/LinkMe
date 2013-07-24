using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Linkme.Domain.Users.Employers.Contacts;

namespace LinkMe.Domain.Users.Employers.Contacts.Data
{
    internal enum MemberMessageType
    {
        Contact = 1,
        Rejection = 2,
    }

    internal static class Mappings
    {
        public static EmployerMemberMessageEntity Map(this MemberMessage message, Guid employerId, Guid memberId, Guid? representativeId, Guid? accessId)
        {
            return new EmployerMemberMessageEntity
            {
                id = message.Id,
                time = message.CreatedTime,
                employerId = employerId,
                memberId = memberId,
                representativeId = representativeId,
                contactId = accessId,
                subject = message.Subject,
                body = message.Body,
                fromEmailAddress = message.From,
                sendCopyToEmployer = message.SendCopy,
                messageType = message is ContactMemberMessage ? (int)MemberMessageType.Contact : (int)MemberMessageType.Rejection ,
                EmployerMemberMessageAttachmentEntities = message is ContactMemberMessage ? message.AttachmentIds.Map() : null,
            };
        }

        public static MemberMessage Map(this EmployerMemberMessageEntity entity)
        {
            return (MemberMessageType)entity.messageType == MemberMessageType.Contact
                ? (MemberMessage)new ContactMemberMessage
                  {
                      Id = entity.id,
                      CreatedTime = entity.time,
                      Subject = entity.subject,
                      Body = entity.body,
                      From = entity.fromEmailAddress,
                      SendCopy = entity.sendCopyToEmployer,
                      AttachmentIds = entity.EmployerMemberMessageAttachmentEntities.Map(),
                  }
                : new RejectionMemberMessage
                  {
                      Id = entity.id,
                      CreatedTime = entity.time,
                      Subject = entity.subject,
                      Body = entity.body,
                      From = entity.fromEmailAddress,
                      SendCopy = entity.sendCopyToEmployer,
                  };
        }

        public static EmployerMemberAttachmentEntity Map(this MemberMessageAttachment attachment, Guid employerId)
        {
            return new EmployerMemberAttachmentEntity
            {
                id = attachment.Id,
                uploadedTime = attachment.UploadedTime,
                employerId = employerId,
                fileReferenceId = attachment.FileReferenceId,
            };
        }

        public static MemberMessageAttachment Map(this EmployerMemberAttachmentEntity entity)
        {
            return new MemberMessageAttachment
            {
                Id = entity.id,
                UploadedTime = entity.uploadedTime,
                FileReferenceId = entity.fileReferenceId,
            };
        }

        private static EntitySet<EmployerMemberMessageAttachmentEntity> Map(this IEnumerable<Guid> attachmentIds)
        {
            if (attachmentIds == null)
                return null;

            var set = new EntitySet<EmployerMemberMessageAttachmentEntity>();
            set.AddRange(from a in attachmentIds select new EmployerMemberMessageAttachmentEntity { attachmentId = a });
            return set;
        }

        private static IList<Guid> Map(this IEnumerable<EmployerMemberMessageAttachmentEntity> entities)
        {
            return entities == null
                ? null
                : (from e in entities select e.attachmentId).ToList();
        }
    }
}
