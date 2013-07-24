using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public class EmailDomainCheckerTest
    {
        private static readonly string[] AllowedTestDomains = new[] { "test.linkme.net.au" };

        [TestMethod]
        public void AllowedDomainsTest()
        {
            // The only allowed emails are now @test.linkme.net.au

            var communication = new Communication
            {
                To = new EmailUser("allowed@test.linkme.net.au, <allowed@test.linkme.net.au>")
            };
            EmailDomainChecker.Check(communication, AllowedTestDomains);

            communication = new Communication
            {
                To = new EmailUser("Allowed Domain <allowed@test.linkme.net.au> ")
            };
            EmailDomainChecker.Check(communication, AllowedTestDomains);
        }

        [TestMethod, ExpectedException(typeof(EmailDomainNotAllowedException))]
        public void DisallowedSimpleTest()
        {
            var communication = new Communication {To = new EmailUser("disallowed@some.real.domain")};
            EmailDomainChecker.Check(communication, AllowedTestDomains);
        }

        [TestMethod, ExpectedException(typeof(EmailDomainNotAllowedException))]
        public void DisallowedExtraCharsTest()
        {
            var communication = new Communication {To = new EmailUser("<disallowed@some.real.domain>")};
            EmailDomainChecker.Check(communication, AllowedTestDomains);
        }
    }
}