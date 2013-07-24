using System;
using System.Collections.Generic;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using Linkme.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Domain.Users.Employers.Contacts.Commands
{
    public interface IEmployerMemberContactsCommand
    {
        MemberMessageAttachment CreateMessageAttachment(IEmployer employer, IEnumerable<Guid> existingAttachmentIds, FileContents fileContents, string fileName);
        void DeleteMessageAttachment(IEmployer employer, Guid id);

        void ContactMember(ChannelApp app, IEmployer employer, ProfessionalView view, ContactMemberMessage message);
        void CheckCanContactMember(ChannelApp app, IEmployer employer, ProfessionalView view);
        void ContactMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, ContactMemberMessage message);
        void CheckCanContactMembers(ChannelApp app, IEmployer employer, ProfessionalViews views);

        void RejectMember(IEmployer employer, ProfessionalView view, RejectionMemberMessage message);
        void RejectMembers(IEmployer employer, ProfessionalViews views, RejectionMemberMessage message);
    }
}