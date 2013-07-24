using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using LinkMe.Framework.Communications;

namespace LinkMe.Domain.Roles.Test.Communications.Mocks
{
    public class MockEmailResource
    {
        public string ContentId { get; set; }
        public string MediaType { get; set; }
    }

    public class MockEmailView
    {
        public string Body { get; set; }
        public string MediaType { get; set; }
        public IList<MockEmailResource> Resources { get; set; }
    }

    public class MockEmailAttachment
    {
        public string Name { get; set; }
        public string MediaType { get; set; }
    }

    /// <summary>
    /// This class exists to capture the details about what was sent within the MailMessage.
    /// The reason for this class is that MailMessage itself must be disposed because of
    /// attachments, and so it cannot be kept around.  All that is stored at the moment for
    /// the attachments is the file name.
    /// </summary>
    [Serializable]
    public class MockEmail
    {
        private readonly IList<EmailRecipient> _to;
        private readonly EmailRecipient _from;
        private readonly IList<EmailRecipient> _cc;
        private readonly IList<EmailRecipient> _bcc;
        private readonly EmailRecipient _sender;
        private readonly EmailRecipient _replyTo;
        private readonly Encoding _subjectEncoding;
        private readonly string _subject;
        private readonly bool _isBodyHtml;
        private readonly Encoding _bodyEncoding;
        private readonly string _body;
        private readonly IList<MockEmailView> _alternateViews;
        private readonly IList<MockEmailAttachment> _attachments;

        #region Constructor

        internal MockEmail(MailMessage message)
        {
            // Addresses

            _to = new List<EmailRecipient>();
            foreach (var address in message.To)
                _to.Add(new EmailRecipient(address.Address, address.DisplayName));

            _from = new EmailRecipient(message.From.Address, message.From.DisplayName);

            _cc = new List<EmailRecipient>();
            foreach (var address in message.CC)
                _cc.Add(new EmailRecipient(address.Address, address.DisplayName));

            _bcc = new List<EmailRecipient>();
            foreach (var address in message.Bcc)
                _bcc.Add(new EmailRecipient(address.Address, address.DisplayName));

            if (message.Sender != null)
                _sender = new EmailRecipient(message.Sender.Address, message.Sender.DisplayName);
            if (message.ReplyToList.Count > 0)
                _replyTo = new EmailRecipient(message.ReplyToList[0].Address, message.ReplyToList[0].DisplayName);

            // Subject.

            _subjectEncoding = message.SubjectEncoding;
            _subject = message.Subject;

            // Body.

            _isBodyHtml = message.IsBodyHtml;
            _bodyEncoding = message.BodyEncoding;
            _body = message.Body;

            // Alternate views.

            _alternateViews = new List<MockEmailView>();
            foreach (var alternateView in message.AlternateViews)
            {
                var reader = new StreamReader(alternateView.ContentStream);
                MockEmailView view;

                if (alternateView.LinkedResources.Count == 0)
                {
                    view = new MockEmailView { Body = reader.ReadToEnd(), MediaType = alternateView.ContentType.MediaType };
                }
                else
                {
                    var resources = new List<MockEmailResource>();
                    foreach (var linkedResource in alternateView.LinkedResources)
                        resources.Add(new MockEmailResource { ContentId = linkedResource.ContentId, MediaType = linkedResource.ContentType.MediaType });
                    view = new MockEmailView { Body = reader.ReadToEnd(), MediaType = alternateView.ContentType.MediaType, Resources = resources };
                }

                _alternateViews.Add(view);
            }

            // Attachments.

            _attachments = new List<MockEmailAttachment>();
            foreach (var attachment in message.Attachments)
                _attachments.Add(new MockEmailAttachment { Name = attachment.Name, MediaType = attachment.ContentType.MediaType });
        }

        #endregion

        #region Properties

        public IList<EmailRecipient> To
        {
            get { return _to; }
        }

        public EmailRecipient From
        {
            get { return _from; }
        }

        public IList<EmailRecipient> Cc
        {
            get { return _cc; }
        }

        public IList<EmailRecipient> Bcc
        {
            get { return _bcc; }
        }

        public EmailRecipient Sender
        {
            get { return _sender; }
        }

        public EmailRecipient ReplyTo
        {
            get { return _replyTo; }
        }

        public Encoding SubjectEncoding
        {
            get { return _subjectEncoding; }
        }

        public string Subject
        {
            get { return _subject; }
        }

        public bool IsBodyHtml
        {
            get { return _isBodyHtml; }
        }

        public Encoding BodyEncoding
        {
            get { return _bodyEncoding; }
        }

        public string Body
        {
            get { return _body; }
        }

        public IList<MockEmailAttachment> Attachments
        {
            get { return _attachments; }
        }

        public IList<MockEmailView> AlternateViews
        {
            get { return _alternateViews; }
        }

        #endregion
    }
}