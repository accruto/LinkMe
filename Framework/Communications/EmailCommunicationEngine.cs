using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Communications
{
    public class EmailCommunicationEngine
        : ICommunicationEngine
    {
        private static readonly EventSource EventSource = new EventSource<EmailCommunicationEngine>();
        private readonly IEmailClient _emailClient;
        private readonly string[] _allowedTestDomains;

        public EmailCommunicationEngine(IEmailClient emailClient)
        {
            _emailClient = emailClient;
            _allowedTestDomains = new[] { "test.linkme.net.au" };
        }

        void ICommunicationEngine.Send(Communication communication)
        {
            const string method = "Send";

            try
            {
                Validate(communication);
                SendEmail(CreateEmail(communication));
            }
            catch (EmailDomainNotAllowedException ex)
            {
                // Just log, and don't attempt to send.

                EventSource.Raise(Event.Warning, method, "Not sending email '" + communication.Subject + "'.", ex);
                return;
            }
        }

        #region Create

        private static Email CreateEmail(Communication communication)
        {
            // Merge the email with the task.

            var from = communication.From;
            var to = communication.To;

            var cc = new List<EmailRecipient>();
            if (communication.Copy != null)
            {
                foreach (var copy in communication.Copy)
                    cc.Add(new EmailRecipient(copy.EmailAddress, copy.FullName, copy.FirstName, copy.LastName));
            }

            var bcc = new List<EmailRecipient>();
            if (communication.BlindCopy != null)
            {
                foreach (var copy in communication.BlindCopy)
                    bcc.Add(new EmailRecipient(copy.EmailAddress, copy.FullName, copy.FirstName, copy.LastName));
            }

            var returnPath = communication.Return;
            var returnPathAddress = returnPath == null ? null : new EmailRecipient(returnPath.EmailAddress, returnPath.FullName, returnPath.FirstName, returnPath.LastName);

            return new Email(communication.Id, new EmailRecipient(from.EmailAddress, from.FullName, from.FirstName, from.LastName), new EmailRecipient(to.EmailAddress, to.FullName, to.FirstName, to.LastName), cc, bcc, returnPathAddress)
            {
                Subject = communication.Subject,
                Views = communication.Views,
                Attachments = communication.Attachments,
            };
        }

        #endregion

        #region Send

        private void SendEmail(Email email)
        {
            if (email == null)
                throw new ArgumentNullException("email");
            if (email.To == null && email.Cc == null)
                throw new ArgumentException("The email has no To or Cc address.", "email");

            // Send it.

            using (var message = CreateMessage(email))
            {
                // Attachments.

                AddAttachments(message, email);

                // Subject and body.

                SetSubject(message, email.Subject);
                SetViews(message, email.Views);

                // Send it.

                _emailClient.Send(message);
            }
        }

        private static string CleanSubject(string subject)
        {
            // The subject must not contain newline characters - replace them with spaces. Also limit it to
            // 300 characters.

            return subject == null ? null : TextUtil.TruncateForDisplay(HtmlUtil.StripHtmlTags(subject.Replace('\r', ' ').Replace('\n', ' ')), 300);
        }

        private static MailMessage CreateMessage(Email email)
        {
            var message = new MailMessage();

            // Need to potentially split out multiple email addresses for To.

            var addresses = email.To.Address.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (addresses.Length > 0)
            {
                if (!string.IsNullOrEmpty(email.To.DisplayName))
                {
                    message.To.Add(new MailAddress(addresses[0], email.To.DisplayName));

                    for (var index = 1; index < addresses.Length; index++)
                        message.To.Add(addresses[index]);
                }
                else
                {
                    foreach (var address in addresses)
                        message.To.Add(address);
                }
            }

            // The email will actually come from the return address, ie From and Return-Path headers will be set to this
            // so that delivery errors etc will bounce to that address where they can be processed.
            // The ReplyTo field will be set so that if a user hits reply the email goes to the right person.

            message.From = new MailAddress(email.Return.Address, email.From.DisplayName);
            message.ReplyToList.Add(new MailAddress(email.From.Address, email.From.DisplayName));

            // Set Cc.

            if (email.Cc != null)
            {
                foreach (var emailAddress in email.Cc)
                    message.CC.Add(new MailAddress(emailAddress.Address, emailAddress.DisplayName));
            }

            // Set Bcc.

            if (email.Bcc != null)
            {
                foreach (var emailAddress in email.Bcc)
                    message.Bcc.Add(new MailAddress(emailAddress.Address, emailAddress.DisplayName));
            }

            // Add a specific header for identifying the message if needed.

            message.Headers.Add("X-LinkMe-Id", email.Id.ToString("n"));
            return message;
        }

        private static void AddAttachments(MailMessage message, Email email)
        {
            if (email.Attachments == null)
                return;

            foreach (var attachment in email.Attachments)
            {
                if (attachment is FileAttachment)
                {
                    var fileAttachment = (FileAttachment)attachment;
                    message.Attachments.Add(new Attachment(fileAttachment.FileName, fileAttachment.MediaType));
                }
                else if (attachment is ContentAttachment)
                {
                    var contentAttachment = (ContentAttachment)attachment;
                    message.Attachments.Add(new Attachment(contentAttachment.ContentStream, contentAttachment.Name, contentAttachment.MediaType));
                }
            }
        }

        private static void SetSubject(MailMessage message, string subject)
        {
            message.Subject = CleanSubject(subject);
        }

        private static void SetViews(MailMessage message, IEnumerable<CommunicationView> views)
        {
            // Add all views, making sure the HTML version comes last as the preferred view to show.

            foreach (var view in views.Where(v => v.MimeType != MediaTypeNames.Text.Html))
                SetView(view, message);
            foreach (var view in views.Where(v => v.MimeType == MediaTypeNames.Text.Html))
                SetView(view, message);
        }

        private static void SetView(CommunicationView view, MailMessage message)
        {
            var alternateView = AlternateView.CreateAlternateViewFromString(view.Body, null, view.MimeType);
            alternateView.TransferEncoding = TransferEncoding.SevenBit; // .NET 2.0 impelementation of the default QuotedPrintable doesn't comply with RFC 

            // Add any resources.

            if (view.Resources != null)
            {
                foreach (var resource in view.Resources)
                {
                    var linkedResource = new LinkedResource(resource.ContentStream, resource.MediaType)
                    {
                        ContentId = resource.Id,
                        TransferEncoding = TransferEncoding.Base64
                    };
                    alternateView.LinkedResources.Add(linkedResource);
                }
            }

            message.AlternateViews.Add(alternateView);
        }

        #endregion

        private void Validate(Communication communication)
        {
            if (string.IsNullOrEmpty(communication.To.EmailAddress) && communication.Copy.Count == 0 && communication.BlindCopy.Count == 0)
                throw new ArgumentException("The mail message has no recipients specified.");
            EmailDomainChecker.Check(communication, _allowedTestDomains);
        }
    }
}