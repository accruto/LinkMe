using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Mocks
{
    public static class MockEmailTestExtensions
    {
        private const int MaxLineLength = 78;
        private static string _rootPath;

        private static readonly string[] ExternalUrls =
            {
                "http://test.external.com",
                "http://www.autopeople.com.au",
                "mailto:",
            };

        public static void AssertNoEmailSent(this IMockEmailServer emailServer)
        {
            Assert.AreEqual(0, emailServer.GetEmails().Count);
        }

        public static MockEmail AssertEmailSent(this IMockEmailServer emailServer)
        {
            var messages = emailServer.GetEmails();
            Assert.AreEqual(1, messages.Count);
            var email = messages[0];
            emailServer.ClearEmails();
            return email;
        }

        public static MockEmail[] AssertEmailsSent(this IMockEmailServer emailServer, int count)
        {
            var messages = emailServer.GetEmails();
            Assert.AreEqual(count, messages.Count);
            var emails = new MockEmail[count];
            messages.CopyTo(emails, 0);
            emailServer.ClearEmails();
            return (from e in emails select e).ToArray();
        }

        public static string RootPath
        {
            get { return _rootPath; }
            set { _rootPath = value; }
        }

        #region Address

        public static void AssertAddresses(this MockEmail email, ICommunicationUser from, EmailRecipient ret, ICommunicationUser to)
        {
            email.AssertAddresses(GetEmailRecipient(from), ret, GetEmailRecipient(to));
        }

        public static void AssertAddresses(this MockEmail email, EmailRecipient from, EmailRecipient ret, ICommunicationUser to)
        {
            email.AssertAddresses(from, ret, GetEmailRecipient(to));
        }

        public static void AssertAddresses(this MockEmail email, ICommunicationUser from, EmailRecipient ret, EmailRecipient to)
        {
            email.AssertAddresses(GetEmailRecipient(from), ret, to, null, null);
        }

        public static void AssertAddresses(this MockEmail email, ICommunicationUser from, EmailRecipient ret, string toAddress)
        {
            email.AssertAddresses(GetEmailRecipient(from), ret, new EmailRecipient(toAddress));
        }

        public static void AssertAddresses(this MockEmail email, EmailRecipient from, EmailRecipient ret, EmailRecipient to)
        {
            AssertAddresses(email, from, ret, new[] { to }, false, null, false, null);
        }

        public static void AssertAddresses(this MockEmail email, RegisteredUser from, EmailRecipient ret, EmailRecipient to, EmailRecipient[] cc, EmailRecipient bcc)
        {
            AssertAddresses(email, GetEmailRecipient(from), ret, new[] { to }, true, cc, true, bcc);
        }

        public static void AssertAddresses(this MockEmail email, EmailRecipient from, EmailRecipient ret, EmailRecipient to, EmailRecipient[] cc, EmailRecipient bcc)
        {
            AssertAddresses(email, from, ret, new[] { to }, true, cc, true, bcc);
        }

        public static void AssertAddresses(this MockEmail email, EmailRecipient from, EmailRecipient ret, ICommunicationUser to, ICommunicationUser[] cc, ICommunicationUser bcc)
        {
            AssertAddresses(email, from, ret, GetEmailRecipient(to), cc == null ? null : (from c in cc select GetEmailRecipient(c)).ToArray(), bcc == null ? null : GetEmailRecipient(bcc));
        }

        private static void AssertAddresses(MockEmail email, EmailRecipient from, EmailRecipient ret, EmailRecipient[] to, bool assertCc, EmailRecipient[] cc, bool assertBcc, EmailRecipient bcc)
        {
            // From. Address should actually be the return address and the ReplyTo field is set to what it is the from address.

            Assert.AreEqual(ret.Address, email.From.Address, "'From' email address differs");
            Assert.AreEqual(from.DisplayName, email.From.DisplayName, "'From' display name differs");

            Assert.AreEqual(from.Address, email.ReplyTo.Address, "'ReplyTo' email address differs");
            Assert.AreEqual(from.DisplayName, email.ReplyTo.DisplayName, "'ReplyTo' display name differs");

            // To.

            Assert.AreEqual(to.Length, email.To.Count);
            for (int index = 0; index < to.Length; ++index)
            {
                Assert.AreEqual(to[index].Address, email.To[index].Address, "'To' email address differs");
                Assert.AreEqual(to[index].DisplayName, email.To[index].DisplayName, "'To' display name differs");
            }

            // Cc.

            if (assertCc)
            {
                if (cc == null)
                {
                    Assert.AreEqual(0, email.Cc.Count);
                }
                else
                {
                    Assert.AreEqual(cc.Length, email.Cc.Count);
                    for (var index = 0; index < cc.Length; ++index)
                    {
                        Assert.AreEqual(cc[index].Address, email.Cc[index].Address, "'Cc' email address differs");
                        Assert.AreEqual(cc[index].DisplayName, email.Cc[index].DisplayName, "'Cc' display name differs");
                    }
                }
            }

            // Bcc.

            if (assertBcc)
            {
                if (bcc == null)
                {
                    Assert.AreEqual(0, email.Bcc.Count);
                }
                else
                {
                    Assert.AreEqual(1, email.Bcc.Count);
                    Assert.AreEqual(bcc.Address, email.Bcc[0].Address, "'Bcc' email address differs");
                    Assert.AreEqual(bcc.DisplayName, email.Bcc[0].DisplayName, "'Bcc' display name differs");
                }
            }

            // Sender.

            Assert.IsNull(email.Sender);
        }

        #endregion

        public static void AssertSubject(this MockEmail email, string subject)
        {
            Assert.IsNull(email.SubjectEncoding);
            Assert.AreEqual(subject, email.Subject);
        }

        #region Body

        public static void AssertBodyChecks(this MockEmail email)
        {
            email.AssertBodyChecks(null, true, null);
        }

        public static void AssertBodyChecks(this MockEmail email, string rootPath, bool checkLineLengths, string[] longLines)
        {
            Assert.IsTrue(email.IsBodyHtml);
            AssertBodyIsAsciiEncoding(email);
            AssertIsXml(email.Body);
            AssertTinyUrls(email.Body, rootPath);
            AssertLineLengths(email, checkLineLengths, longLines);
        }

        public static void AssertBody(this MockEmail email, string body)
        {
            Assert.AreEqual(body, email.Body);
        }

        public static void AssertBodyContains(this MockEmail email, string text)
        {
            AssertContains(email.Body, text);
        }

        public static void AssertBodyDoesNotContain(this MockEmail email, string text)
        {
            AssertDoesNotContain(email.Body, text);
        }

        #endregion

        #region Views

        public static MockEmailView GetView(this MockEmail email, string mimeType)
        {
            var view = email.AlternateViews.SingleOrDefault(v => v.MediaType == mimeType);
            if (view == null)
                Assert.Fail("'{0}' view is not found in the email.", mimeType);
            return view;
        }

        public static IList<ReadOnlyUrl> GetLinks(this MockEmailView view)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(view.Body);
            return (from a in htmlDoc.DocumentNode.SelectNodes("//a")
                    where !a.Attributes["href"].Value.StartsWith("mailto:")
                    select new ReadOnlyUrl(a.Attributes["href"].Value)).ToList();
        }

        public static IList<ReadOnlyUrl> GetPlainTextLinks(this MockEmailView view)
        {
            // Need to parse out directly.

            var list = new List<ReadOnlyUrl>();

            var body = view.Body;
            var pos = body.IndexOf("http", 0);
            while (pos != -1)
            {
                var endPos = body.IndexOfAny(new[] { ' ', '\r', '\n', '\t' }, pos);
                var link = endPos == -1 ? body.Substring(pos) : body.Substring(pos, endPos - pos);
                list.Add(new ReadOnlyUrl(link));

                pos = endPos == -1 ? -1 : body.IndexOf("http", endPos);
            }

            return list;
        }

        public static IList<ReadOnlyUrl> GetImageLinks(this MockEmailView view)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(view.Body);
            return (from a in htmlDoc.DocumentNode.SelectNodes("//img")
                    select new ReadOnlyUrl(a.Attributes["src"].Value)).ToList();
        }

        public static void AssertViewCount(this MockEmail email, int count)
        {
            Assert.AreEqual(count, email.AlternateViews.Count);
        }

        public static void AssertViewChecks(this MockEmail email, string mimeType)
        {
            email.AssertViewChecks(mimeType, null, true, null);
        }

        public static void AssertViewChecks(this MockEmail email, string mimeType, string rootPath, bool checkLineLengths, string[] longLines)
        {
            var view = GetView(email, mimeType);
            Assert.IsNotNull(view);

            if (mimeType == MediaTypeNames.Text.Html)
                AssertIsXml(view.Body);

            AssertTinyUrls(view.Body, rootPath);
            AssertLineLengths(email, checkLineLengths, longLines);
        }

        public static void AssertView(this MockEmail email, string mimeType, string body)
        {
            Assert.AreEqual(body, GetView(email, mimeType).Body);
        }

        public static void AssertViewContains(this MockEmail email, string mimeType, string text)
        {
            AssertContains(GetView(email, mimeType).Body, text);
        }

        public static void AssertViewDoesNotContain(this MockEmail email, string mimeType, string text)
        {
            AssertDoesNotContain(GetView(email, mimeType).Body, text);
        }

        #endregion

        #region HTML Views

        public static MockEmailView GetHtmlView(this MockEmail email)
        {
            return email.GetView(MediaTypeNames.Text.Html);
        }

        public static void AssertHtmlViewChecks(this MockEmail email)
        {
            email.AssertViewChecks(MediaTypeNames.Text.Html, null, true, null);
        }

        public static void AssertHtmlViewChecks(this MockEmail email, string rootPath, bool checkLineLengths, string[] longLines)
        {
            email.AssertViewChecks(MediaTypeNames.Text.Html, rootPath, checkLineLengths, longLines);
        }

        public static void AssertHtmlView(this MockEmail email, string body)
        {
            email.AssertView(MediaTypeNames.Text.Html, body);
        }

        public static void AssertHtmlViewContains(this MockEmail email, string text)
        {
            email.AssertViewContains(MediaTypeNames.Text.Html, text);
        }

        public static void AssertHtmlViewDoesNotContain(this MockEmail email, string text)
        {
            email.AssertViewDoesNotContain(MediaTypeNames.Text.Html, text);
        }

        #endregion

        #region Text Views

        public static MockEmailView GetPlainTextView(this MockEmail email)
        {
            return email.GetView(MediaTypeNames.Text.Plain);
        }

        #endregion

        #region Attachments

        public static void AssertNoAttachments(this MockEmail email)
        {
            Assert.AreEqual(0, email.Attachments.Count);
        }

        public static void AssertAttachment(this MockEmail email, string fileName, string mediaType)
        {
            email.AssertAttachments(new[] { new MockEmailAttachment { Name = fileName, MediaType = mediaType } });
        }

        public static void AssertAttachments(this MockEmail email, int count)
        {
            Assert.AreEqual(count, email.Attachments.Count);
        }

        public static void AssertAttachments(this MockEmail email, params MockEmailAttachment[] expectedAttachments)
        {
            Assert.AreEqual(expectedAttachments.Length, email.Attachments.Count);
            for (var index = 0; index < expectedAttachments.Length; ++index)
            {
                var expectedAttachment = expectedAttachments[index];
                var attachment = (from a in email.Attachments where a.Name == expectedAttachment.Name select a).Single();
                Assert.AreEqual(expectedAttachment.MediaType, attachment.MediaType);
            }
        }

        #endregion

        #region Private Methods

        private static EmailRecipient GetEmailRecipient(ICommunicationRecipient user)
        {
            return new EmailRecipient(user.EmailAddress, user.FullName);
        }

        private static void AssertContains(string message, string text)
        {
            if (!string.IsNullOrEmpty(text))
                Assert.IsTrue(message.Contains(text), "The message does not contain '" + text + "'");
        }

        private static void AssertDoesNotContain(string message, string text)
        {
            if (!string.IsNullOrEmpty(text))
                Assert.IsFalse(message.Contains(text), "The message contains '" + text + "'");
        }

        private static void AssertBodyIsAsciiEncoding(MockEmail email)
        {
            if (email.BodyEncoding != null && !(email.BodyEncoding is UTF8Encoding))
            {
                // Try to find the special character causing this problem.

                var message = "The email body encoding is not ASCII - did you paste some special character into the email template?";

                var charIndex = GetFirstNonAsciiCharIndex(email.Body);
                if (charIndex != -1)
                {
                    message += string.Format("\r\nThe suspected character is '{0}' (0x{1:x}) at index {2}: \"{3}\"",
                                             email.Body[charIndex], (int)email.Body[charIndex], charIndex,
                                             TextUtil.TruncateForDisplay(email.Body.Substring(charIndex, 101), 100));
                }

                Assert.Fail(message);
            }
        }

        private static void AssertIsXml(string body)
        {
            // Check that the email is well formed XML by trying to load it.

            var document = new XmlDocument();
            document.LoadXml(body);
        }

        private static void AssertTinyUrls(string body, string rootPath)
        {
            var document = new XmlDocument();
            document.LoadXml(body);

            foreach (XmlNode xmlNode in document.SelectNodes("//a/@href"))
            {
                var url = xmlNode.InnerText;

                // Ignore external urls.

                if (ExternalUrls.Any(url.StartsWith))
                    continue;

                var path = string.Empty;
                if (string.IsNullOrEmpty(rootPath))
                    rootPath = _rootPath;
                if (url.StartsWith(rootPath))
                    path = url.Substring(rootPath.Length);
                else
                    Assert.Fail("The url '" + url + "' does not start with the root url.");

                // There should be no query string.

                if (path.IndexOf('?') != -1)
                    Assert.Fail("The url '" + url + "' contains a query string when it shouldn't.");

                // Must be of form: ~/url/<guid>

                var parts = path.Split('/');
                if (parts.Length != 2 || parts[0] != "url")
                    Assert.Fail("The url '" + url + "' is not of the required form: '~/url/<guid>'.");

                // The last part needs to be guid.

                try
                {
                    new Guid(parts[1]);
                }
                catch (Exception)
                {
                    Assert.Fail("The url '" + path + "' is not of the required form: '~/url/<guid>'.");
                }
            }
        }

        private static void AssertLineLengths(MockEmail email, bool checkLineLengths, IEnumerable<string> longLines)
        {
            string line;
            var reader = new StringReader(email.Body);
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim().Replace("&amp;", "&");

                // Ignore link, div and img.

                if (!line.StartsWith("<link href=\"") && !line.StartsWith("<div ") && !line.StartsWith("<img src=\""))
                {
                    if (line.StartsWith("<a "))
                    {
                        // Always check that the href does not exceed the maximum length.

                        int pos = line.IndexOf("href=\"");
                        if (pos != -1)
                        {
                            line = line.Substring(pos + "href=\"".Length);
                            pos = line.IndexOf("\"");
                            if (pos != -1)
                            {
                                line = line.Substring(0, pos);
                                Assert.IsTrue(line.Length < MaxLineLength + 1, "The '" + line + "' href exceeds the maximum length " + MaxLineLength + ".");
                            }
                        }
                    }
                    else
                    {
                        if (checkLineLengths)
                        {
                            // Check that the line is known to be long.

                            bool ok = false;
                            if (longLines != null)
                            {
                                foreach (string longLine in longLines)
                                {
                                    if (line.StartsWith(longLine))
                                    {
                                        ok = true;
                                        break;
                                    }
                                }
                            }

                            if (!ok)
                                Assert.IsTrue(line.Length < MaxLineLength + 1, "The '" + line + "' line exceeds the maximum length " + MaxLineLength + ".");
                        }
                    }
                }
            }
        }

        private static int GetFirstNonAsciiCharIndex(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                int intval = text[i];
                if ((intval < 0x20 && intval != 0xA && intval != 0xD) || intval >= 0x7F)
                    return i;
            }

            return -1;
        }

        #endregion
    }
}