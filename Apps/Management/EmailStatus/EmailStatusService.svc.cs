using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Win32;
using LinkMe.Interop.Cdo;

namespace LinkMe.Apps.Management.EmailStatus
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmailStatusService 
        : IEmailStatusService
    {
        private static readonly EventSource<EmailStatusService> Logger = new EventSource<EmailStatusService>();
        private static readonly int[] TransientReplyCodes = new[]{ 552, 554 };
        private static readonly char[] SubfieldSeparators = new[] {';'};
        private static readonly char[] SaidSeparators = new[] {' ', '-', '\r', '\n'};
        private static readonly char[] SmtpSeparators = new[] {' ', '-', '\r', '\n'};
        private const string Yahoo = "@yahoo.com";

        private readonly IEmailVerificationsCommand _emailVerificationsCommand;

        public EmailStatusService(IEmailVerificationsCommand emailVerificationsCommand)
        {
            _emailVerificationsCommand = emailVerificationsCommand;
        }

        #region Implementation of IEmailStatusService

        void IEmailStatusService.AddNotification(Stream httpStream)
        {
            const string method = "AddNotification";

            try
            {
                SetHttpStatusCode(HttpStatusCode.NoContent);

                // Parse top-level message.

                var memoryStream = new MemoryStream();
                StreamUtil.CopyStream(httpStream, memoryStream); // CDO will need random access to the stream

                var rootMessage = new MessageClass();
                rootMessage.OpenObject(new ReadOnlyIStream(memoryStream), "IStream");
                if (rootMessage.ContentMediaType == "test/octet-stream")
                {
                    SetHttpStatusCode(HttpStatusCode.Accepted); // just testing
                    return;
                }

                var deliveryStatusPart = GetDeliveryStatusPart(rootMessage);
                if (deliveryStatusPart == null)
                    return;

                // Analyze message/delivery-status.

                var mtaMessage = GetInnerMessage(deliveryStatusPart);
                var recipientMessage = GetInnerMessage(mtaMessage);
                var recipientField = (string)recipientMessage.Fields["urn:schemas:mailheader:final-recipient"].Value;
                if (recipientField == null)
                {
                    #region Log
                    Log(Event.Warning, method, "Final-Recipient field is missing. Notofication ignored.", null, rootMessage);
                    #endregion
                    return;
                }

                var emailAddress = GetEmailAddress(recipientField);
                if (string.IsNullOrEmpty(emailAddress))
                {
                    #region Log
                    Log(Event.Warning, method, "Final-Recipient is invalid. Notification ignored.", null, rootMessage, Event.Arg("Final-Recipient", recipientField));
                    #endregion
                    return;
                }

                var statusField = (string)recipientMessage.Fields["urn:schemas:mailheader:status"].Value;
                if (statusField == null)
                {
                    #region Log
                    Log(Event.Warning, method, "Status field is missing. Notification ignored.", null, rootMessage);
                    #endregion
                    return;
                }

                if (GetStatusClass(statusField) != "5")
                {
                    #region Log
                    Log(Event.Trace, method, "Not a permanent error. Notification ignored.", null, rootMessage, Event.Arg("Status", statusField));
                    #endregion
                    return;
                }

                var diagnosticField = (string)recipientMessage.Fields["urn:schemas:mailheader:diagnostic-code"].Value;
                if (diagnosticField == null)
                {
                    #region Log
                    Log(Event.Information, method, "Diagnostic-Code field is missing. Notification will be processed.", null, rootMessage);
                    #endregion
                }
                else
                {
                    var replyCode = GetReplyCode(diagnosticField);
                    if (replyCode == null)
                    {
                        #region Log
                        Log(Event.Warning, method, "Unable to interpret the Diagnostic-Code field. Notification will be processed.", null, rootMessage, Event.Arg("Diagnostic-Code", diagnosticField));
                        #endregion
                    }
                    else if (TransientReplyCodes.Contains(replyCode.Value))
                    {
                        // Yahoo uses transient codes for permanent errors
                        if (!emailAddress.Contains(Yahoo))
                        {
                            #region Log
                            Log(Event.Information, method,"Reply code indicates transient condition. Notification ignored.", null, rootMessage,Event.Arg("Diagnostic-Code", diagnosticField));
                            #endregion

                            return;
                        }
                    }
                }

                // Update member record.

                #region Log
                Log(Event.Information, method, "Setting EmailBounced flag for the user.", null, rootMessage,
                    Event.Arg("emailAddress", emailAddress), Event.Arg("diagnosticField", diagnosticField));
                #endregion

                _emailVerificationsCommand.UnverifyEmailAddress(emailAddress, diagnosticField);
            }
            catch (Exception e)
            {
                Logger.Raise(Event.Error, method, "Unexpected exception.", e, new StandardErrorHandler());
                throw;
            }
        }

        #endregion

        private static void SetHttpStatusCode(HttpStatusCode statusCode)
        {
            var context = WebOperationContext.Current;
            if (context != null)
                context.OutgoingResponse.StatusCode = statusCode;
        }

        private static IBodyPart GetDeliveryStatusPart(MessageClass message)
        {
            const string method = "GetStatusPart";

            if (!message.ContentMediaType.Equals("multipart/report", StringComparison.InvariantCultureIgnoreCase))
            {
                #region Log
                Log(Event.Trace, method, "Unrelated Content-Type.", null, message);
                #endregion
                return null;
            }

            if (message.GetFieldParameter("urn:schemas:mailheader:content-type", "report-type") != "delivery-status")
            {
                #region Log
                Log(Event.Information, method, "Unknown report-type.", null, message);
                #endregion
                return null;
            }

            var statusPart = message.BodyParts.Cast<IBodyPart>()
                .FirstOrDefault(p => p.ContentMediaType.Equals("message/delivery-status", StringComparison.InvariantCultureIgnoreCase));

            if (statusPart == null)
            {
                #region Log
                Log(Event.Error, method, "The message/delivery-status part not found.", null, message);
                #endregion
                return null;
            }

            return statusPart;
        }

        private static MessageClass GetInnerMessage(IBodyPart part)
        {
            var innerMessage = new MessageClass();
            innerMessage.OpenObject(part.GetDecodedContentStream(), "_Stream");
            return innerMessage;
        }

        private static string GetEmailAddress(string recipientField)
        {
            var subfields = recipientField.Split(';');
            if (subfields.Length != 2 || !subfields[0].Trim().Equals("rfc822", StringComparison.InvariantCultureIgnoreCase))
                return null;

            return subfields[1].Trim();
        }

        private static string GetStatusClass(string statusField)
        {
            var dotIndex = statusField.IndexOf('.');
            return (dotIndex == -1) ? statusField : statusField.Substring(0, dotIndex);
        }

        private static int? GetReplyCode(string diagnosticField)
        {
            // Extract diagnostic type.

            var tokens = diagnosticField.Split(SubfieldSeparators, 2);
            if (tokens.Length < 2)
                return null;

            var diagnosticType = tokens[0].Trim();

            // Extract reply code.

            if (diagnosticType.Equals("X-Postfix", StringComparison.InvariantCultureIgnoreCase))
            {
                tokens = tokens[1].Split(SaidSeparators, StringSplitOptions.RemoveEmptyEntries);
                var saidPos = Array.FindIndex(tokens,
                    t => t.Equals("said:", StringComparison.InvariantCultureIgnoreCase));

                if (saidPos == -1 || saidPos + 1 == tokens.Length)
                    return null;

                int replyCode;
                return int.TryParse(tokens[saidPos + 1], out replyCode) ? (int?)replyCode : null;
            }

            if (diagnosticType.Equals("smtp", StringComparison.InvariantCultureIgnoreCase))
            {
                tokens = tokens[1].Split(SmtpSeparators, 2, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 1)
                    return null;

                int replyCode;
                return int.TryParse(tokens[0], out replyCode) ? (int?)replyCode : null;
            }

            return null;
        }

        private static void Log(Event level, string method, string text, Exception exception, IMessage message, params EventArg[] args)
        {
            if (Logger.IsEnabled(level))
            {
                var baseArgs = new[]
                {
                    Event.Arg("Date", message.ReceivedTime),
                    Event.Arg("From", message.From), 
                    Event.Arg("To", message.To), 
                    Event.Arg("Subject", message.Subject),
                    Event.Arg("Content-Type", message.Fields["urn:schemas:mailheader:content-type"].Value),
                    Event.Arg("Message-Id", message.Fields["urn:schemas:mailheader:message-id"].Value)
                };

                Logger.Raise(level, method, text, exception, new StandardErrorHandler(), baseArgs.Concat(args).ToArray());
            }
        }
    }
}
