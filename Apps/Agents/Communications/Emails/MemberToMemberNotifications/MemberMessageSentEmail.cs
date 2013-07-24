using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications
{
    public class MemberMessageSentEmail
        : MemberToMemberNotification
    {
        private readonly Guid _threadId;
        private readonly Guid _messageId;
        private readonly string _messageSubject;
        private readonly string _messageText;

        public MemberMessageSentEmail(ICommunicationUser to, ICommunicationUser from, Guid threadId, Guid messageId, string messageSubject, string messageText)
            : base(to, from)
        {
            _threadId = threadId;
            _messageId = messageId;
            _messageSubject = messageSubject;
            _messageText = messageText;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("ThreadId", _threadId);
            properties.Add("MessageId", _messageId);
            properties.Add("MessageSubject", _messageSubject);
            properties.Add("MessageText", _messageText);
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}