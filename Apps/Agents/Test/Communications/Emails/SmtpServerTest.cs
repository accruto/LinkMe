using System;
using System.Net.Mail;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public class SmtpServerTest
    {
        private const string InvalidMailServer = "0.0.0.0"; // This fails faster than a name.
        private const string ValidMailServer = "SLEEPY";

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInvalidMessageValidMailServer()
        {
            IEmailClient smtpServer = new SmtpEmailClient(ValidMailServer);
            var message = new MailMessage {Body = "body", Subject = "subject"};
            message.To.Add("testemail@test.linkme.net.au");
            smtpServer.Send(message);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestValidMessageInvalidMailServer()
        {
            IEmailClient smtpServer = new SmtpEmailClient(InvalidMailServer);
            var message = new MailMessage
            {
                Body = "body",
                Subject = "subject",
                From = new MailAddress("linkme1@test.linkme.net.au")
            };
            message.To.Add(new MailAddress("linkme2@test.linkme.net.au"));
            smtpServer.Send(message);
        }
    }
}