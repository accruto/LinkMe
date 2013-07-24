using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Queries;
using Linkme.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Employers.Contacts.Commands
{
    public class EmployerMemberContactsCommand
        : IEmployerMemberContactsCommand
    {
        private readonly IEmployerContactsRepository _repository;
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand;
        private readonly IMemberContactsQuery _memberContactsQuery;
        private readonly IFilesCommand _filesCommand;
        private readonly IFilesQuery _filesQuery;

        public EmployerMemberContactsCommand(IEmployerContactsRepository repository, IEmployerMemberViewsCommand employerMemberViewsCommand, IMemberContactsQuery memberContactsQuery, IFilesCommand filesCommand, IFilesQuery filesQuery)
        {
            _repository = repository;
            _employerMemberViewsCommand = employerMemberViewsCommand;
            _memberContactsQuery = memberContactsQuery;
            _filesCommand = filesCommand;
            _filesQuery = filesQuery;
        }

        MemberMessageAttachment IEmployerMemberContactsCommand.CreateMessageAttachment(IEmployer employer, IEnumerable<Guid> existingAttachmentIds, FileContents fileContents, string fileName)
        {
            // Check first.

            Validate(employer.Id, existingAttachmentIds, fileContents, fileName);

            // Save the file.

            var fileReference = _filesCommand.SaveFile(FileType.Attachment, fileContents, fileName);

            // Create the attachment to be saved.

            var attachment = new MemberMessageAttachment {FileReferenceId = fileReference.Id};
            attachment.Prepare();
            attachment.Validate();
            _repository.CreateMessageAttachment(employer.Id, attachment);

            return attachment;
        }

        void IEmployerMemberContactsCommand.DeleteMessageAttachment(IEmployer employer, Guid id)
        {
            // There is currently no way to delete a file reference and its associated file.

            _repository.DeleteMessageAttachment(employer.Id, id);
        }

        void IEmployerMemberContactsCommand.ContactMember(ChannelApp app, IEmployer employer, ProfessionalView view, ContactMemberMessage messageTemplate)
        {
            var cleaner = new MemberMessageCleaner();
            var cleanedBody = cleaner.CleanBody(messageTemplate.Body);
            var cleanedSubject = cleaner.CleanSubject(messageTemplate.Subject);

            // Messages are being sent so exercise a credit.

            var access = _employerMemberViewsCommand.AccessMember(app, employer, view, MemberAccessReason.MessageSent);

            // Save the message.

            if (access != null)
            {
                var handlers = MessageCreated;
                var representativeId = _memberContactsQuery.GetRepresentativeContact(view.Id);
                ContactMember(messageTemplate, employer, view, cleanedSubject, cleanedBody, representativeId, access, handlers);
            }
        }

        void IEmployerMemberContactsCommand.CheckCanContactMember(ChannelApp app, IEmployer employer, ProfessionalView view)
        {
            _employerMemberViewsCommand.CheckCanAccessMember(app, employer, view, MemberAccessReason.MessageSent);
        }

        void IEmployerMemberContactsCommand.ContactMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, ContactMemberMessage messageTemplate)
        {
            var cleaner = new MemberMessageCleaner();
            var cleanedBody = cleaner.CleanBody(messageTemplate.Body);
            var cleanedSubject = cleaner.CleanSubject(messageTemplate.Subject);

            // Try to access all the members.

            var accesses = _employerMemberViewsCommand.AccessMembers(app, employer, views, MemberAccessReason.MessageSent);

            // Get the member and the representative if there is one.

            var representativeIds = _memberContactsQuery.GetRepresentativeContacts(from m in views select m.Id);

            // Iterate through them all exercising credits when needed.

            var handlers = MessageCreated;
            foreach (var view in views)
            {
                var viewId = view.Id;
                var representativeId = representativeIds[view.Id];
                var access = (from a in accesses where a.MemberId == viewId select a).SingleOrDefault();
                ContactMember(messageTemplate, employer, view, cleanedSubject, cleanedBody, representativeId, access, handlers);
            }
        }

        void IEmployerMemberContactsCommand.CheckCanContactMembers(ChannelApp app, IEmployer employer, ProfessionalViews views)
        {
            _employerMemberViewsCommand.CheckCanAccessMembers(app, employer, views, MemberAccessReason.MessageSent);
        }

        void IEmployerMemberContactsCommand.RejectMember(IEmployer employer, ProfessionalView view, RejectionMemberMessage messageTemplate)
        {
            var cleaner = new MemberMessageCleaner();
            var cleanedBody = cleaner.CleanBody(messageTemplate.Body);
            var cleanedSubject = cleaner.CleanSubject(messageTemplate.Subject);

            // Save the message.
            var handlers = MessageCreated;
            var representativeId = _memberContactsQuery.GetRepresentativeContact(view.Id);
            ContactMember(messageTemplate, employer, view, cleanedSubject, cleanedBody, representativeId, null, handlers);
        }

        void IEmployerMemberContactsCommand.RejectMembers(IEmployer employer, ProfessionalViews views, RejectionMemberMessage messageTemplate)
        {
            var cleaner = new MemberMessageCleaner();
            var cleanedBody = cleaner.CleanBody(messageTemplate.Body);
            var cleanedSubject = cleaner.CleanSubject(messageTemplate.Subject);

            // Get the member and the representative if there is one.

            var representativeIds = _memberContactsQuery.GetRepresentativeContacts(from m in views select m.Id);

            // Iterate through them all exercising credits when needed.

            var handlers = MessageCreated;
            foreach (var view in views)
            {
                var representativeId = representativeIds[view.Id];
                ContactMember(messageTemplate, employer, view, cleanedSubject, cleanedBody, representativeId, null, handlers);
            }
        }

        private void ContactMember(MemberMessage messageTemplate, IHasId<Guid> employer, IHasId<Guid> view, string cleanedSubject, string cleanedBody, Guid? representativeId, MemberAccess access, EventHandler<MessageCreatedEventArgs> handlers)
        {
            var message = messageTemplate is ContactMemberMessage
                ? CreateMessage<ContactMemberMessage>(messageTemplate, cleanedSubject, cleanedBody)
                : CreateMessage<RejectionMemberMessage>(messageTemplate, cleanedSubject, cleanedBody);

            _repository.CreateMessage(employer.Id, view.Id, representativeId, access == null ? (Guid?) null : access.Id, message);

            if (handlers != null)
                handlers(this, new MessageCreatedEventArgs(employer.Id, view.Id, representativeId, message));
        }

        [Publishes(PublishedEvents.MessageCreated)]
        public event EventHandler<MessageCreatedEventArgs> MessageCreated;

        private static MemberMessage CreateMessage<T>(MemberMessage messageTemplate, string cleanedSubject, string cleanedBody) where T : MemberMessage, new()
        {
            var message =
            new T
                {
                    Subject = cleanedSubject,
                    Body = cleanedBody,
                    AttachmentIds =
                        messageTemplate.AttachmentIds == null
                            ? null
                            : (from a in messageTemplate.AttachmentIds select a).ToList(),
                    From = messageTemplate.From,
                    SendCopy = messageTemplate.SendCopy,
                };
            
            message.Prepare();
            message.Validate();

            return message;
        }

        private void Validate(Guid employerId, IEnumerable<Guid> existingAttachmentIds, FileContents fileContents, string fileName)
        {
            // Must have appropriate file extension.

            var extension = Path.GetExtension(fileName).ToLower();
            if (!Constants.ValidFileExtensions.Contains(extension))
                throw new InvalidFileNameException { FileName = fileName, ValidFileExtensions = Constants.ValidFileExtensions };

            // Must not be too large.

            if (fileContents.Length > Constants.MaxAttachmentFileSize)
                throw new FileTooLargeException { MaxFileSize = Constants.MaxAttachmentFileSize };

            // The total mmust not be too large.

            var attachments = _repository.GetMessageAttachments(employerId, existingAttachmentIds);
            if (attachments.Count > 0)
            {
                var fileReferences = _filesQuery.GetFileReferences(from a in attachments select a.FileReferenceId, new Range());
                var totalSize = attachments.Sum(a => (from f in fileReferences where f.Id == a.FileReferenceId select f).Single().FileData.ContentLength);
                if (totalSize + fileContents.Length > Constants.MaxAttachmentTotalFileSize)
                    throw new TotalFilesTooLargeException { MaxTotalFileSize = Constants.MaxAttachmentTotalFileSize };
            }
        }
    }
}