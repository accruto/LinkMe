using System;
using System.Collections.Generic;

namespace LinkMe.Framework.Communications
{
    public class Email
    {
        private readonly Guid _id;
        private readonly EmailRecipient _from;
        private readonly EmailRecipient _to;
        private readonly IList<EmailRecipient> _cc;
        private readonly IList<EmailRecipient> _bcc;
        private readonly EmailRecipient _returnPath;

        public Email(Guid id, EmailRecipient from, EmailRecipient to, IList<EmailRecipient> cc, IList<EmailRecipient> bcc, EmailRecipient returnPath)
        {
            _id = id;
            _from = from;
            _to = to;
            _cc = cc;
            _bcc = bcc;
            _returnPath = returnPath;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public EmailRecipient From
        {
            get { return _from; }
        }

        public EmailRecipient To
        {
            get { return _to; }
        }

        public IList<EmailRecipient> Cc
        {
            get { return _cc; }
        }

        public IList<EmailRecipient> Bcc
        {
            get { return _bcc; }
        }

        public EmailRecipient Return
        {
            get { return _returnPath; }
        }

        public string Subject { get; set; }
        public IList<CommunicationView> Views { get; set; }
        public IList<CommunicationAttachment> Attachments { get; set; }
    }
}