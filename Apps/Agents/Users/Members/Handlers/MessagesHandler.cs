using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Employers.Contacts.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public class MessagesHandler
        : IMessagesHandler
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly IMembersQuery _membersQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IEmployerMemberContactsQuery _employerMemberContactsQuery;
        private readonly IFilesQuery _filesQuery;

        public MessagesHandler(IEmailsCommand emailsCommand, IMembersQuery membersQuery, IEmployersQuery employersQuery, IEmployerMemberViewsQuery employerMemberViewsQuery, IEmployerMemberContactsQuery employerMemberContactsQuery, IFilesQuery filesQuery)
        {
            _emailsCommand = emailsCommand;
            _membersQuery = membersQuery;
            _employersQuery = employersQuery;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _employerMemberContactsQuery = employerMemberContactsQuery;
            _filesQuery = filesQuery;
        }

        void IMessagesHandler.OnMessageSent(Guid fromId, Guid toId, Guid? representativeId, MemberMessage message)
        {
            var member = _membersQuery.GetMember(toId);
            var employer = _employersQuery.GetEmployer(fromId);

            IList<FileReference> fileReferences = null;
            if (message.AttachmentIds != null && message.AttachmentIds.Count > 0)
            {
                var messageAttachments = _employerMemberContactsQuery.GetMessageAttachments(employer, message.AttachmentIds);
                fileReferences = _filesQuery.GetFileReferences(from a in messageAttachments select a.FileReferenceId, new Range());
            }

            // Create the email to send to the member and send it.

            TemplateEmail email;
            if (representativeId == null)
            {
                email = new ContactCandidateEmail(member, message.From, employer, message.Subject, GetMemberBody(message.Body, member));
            }
            else
            {
                var representative = _membersQuery.GetMember(representativeId.Value);
                email = new RepresentativeContactCandidateEmail(representative, message.From, employer, member, message.Subject, GetMemberBody(message.Body, member));
            }

            AddAttachments(email, fileReferences);
            _emailsCommand.TrySend(email);

            // Create the email to the employer and send it.

            if (message.SendCopy)
            {
                // Need the view.

                var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
                var confirmationEmail = new EmployerContactCandidateConfirmationEmail(message.From, employer, member.Id, view.GetFullNameDisplayText(), message.Subject, GetEmployerBody(message.Body, view));
                AddAttachments(confirmationEmail, fileReferences);
                _emailsCommand.TrySend(confirmationEmail);
            }
        }

        private static string GetEmployerBody(string body, IRegisteredUser member)
        {
            return body == null
                ? null
                : body.Replace("<%= To.FirstName %>", member.FirstName ?? "[Candidate first name]").Replace("<%= To.LastName %>", member.LastName ?? "[Candidate last name]");
        }

        private static string GetMemberBody(string body, IRegisteredUser member)
        {
            return body == null
                ? null
                : body.Replace("<%= To.FirstName %>", member.FirstName).Replace("<%= To.LastName %>", member.LastName);
        }

        private void AddAttachments(TemplateEmail email, ICollection<FileReference> fileReferences)
        {
            if (fileReferences != null && fileReferences.Count > 0)
            {
                var attachments = new List<CommunicationAttachment>();
                foreach (var fileReference in fileReferences)
                {
                    var stream = _filesQuery.OpenFile(fileReference);
                    attachments.Add(new ContentAttachment(stream, fileReference.FileName, MediaType.GetMediaTypeFromExtension(Path.GetExtension(fileReference.FileName), MediaType.Text)));
                }

                email.AddAttachments(attachments);
            }
        }
    }
}
