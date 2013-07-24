using System;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Commands;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Emails
{
    [TestClass]
    public class TinyUrlTests
        : WebTestClass
    {
        private readonly ITinyUrlCommand _tinyUrlCommand = Resolve<ITinyUrlCommand>();

        [TestMethod]
        public void TestTinyUrl()
        {
            var mapping = new TinyUrlMapping
            {
                TinyId = Guid.NewGuid(),
                Definition = "PasswordReminderEmail",
                LongUrl = new ReadOnlyApplicationUrl("~/terms"),
            };
            _tinyUrlCommand.CreateMappings(new[] { mapping });

            var url = new ReadOnlyApplicationUrl("~/url/" + mapping.TinyId.ToString("n"));
            AssertTinyUrl(url, mapping);
        }

        [TestMethod]
        public void TestTinyUrlBracket()
        {
            var mapping = new TinyUrlMapping
            {
                TinyId = Guid.NewGuid(),
                Definition = "PasswordReminderEmail",
                LongUrl = new ReadOnlyApplicationUrl("~/terms"),
            };
            _tinyUrlCommand.CreateMappings(new[] { mapping });

            var url = new ReadOnlyApplicationUrl("~/url/" + mapping.TinyId.ToString("n") + ">");
            AssertTinyUrl(url, mapping);
        }

        private void AssertTinyUrl(ReadOnlyUrl url, TinyUrlMapping mapping)
        {
            Get(url);

            var expectedUrl = mapping.LongUrl.AsNonReadOnly();
            expectedUrl.QueryString["utm_source"] = "linkme";
            expectedUrl.QueryString["utm_medium"] = "email";
            expectedUrl.QueryString["utm_campaign"] = mapping.Definition.ToLower();
            AssertUrl(expectedUrl);
        }
    }
}
