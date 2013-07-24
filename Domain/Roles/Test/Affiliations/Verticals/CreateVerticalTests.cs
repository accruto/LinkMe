using System;
using System.Linq;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Affiliations.Verticals
{
    [TestClass]
    public class CreateVerticalTests
        : VerticalTests
    {
        [TestMethod]
        public void TestCreateVertical()
        {
            var vertical = new Vertical
            {
                Name = string.Format(VerticalNameFormat, 0),
                CountryId = 2,
                ExternalLoginUrl = "http://external.com",
                ExternalCookieDomain = ".external.com",
                Host = "external.linkme.com",
                RequiresExternalLogin = true,
                SecondaryHost = "external2.linkme.com",
                TertiaryHost = "external3.linkme.com",
                Url = "external",
            };

            _verticalsCommand.CreateVertical(vertical);
            Assert.AreNotEqual(Guid.Empty, vertical.Id);
            AssertVertical(vertical, _verticalsQuery.GetVertical(vertical.Id));
        }

        [TestMethod]
        public void TestIsEnabled()
        {
            var vertical0 = new Vertical { Name = string.Format(VerticalNameFormat, 0) };
            _verticalsCommand.CreateVertical(vertical0);
            Assert.IsFalse(vertical0.IsDeleted);
            AssertVertical(vertical0);
            AssertVerticals(vertical0);

            var vertical1 = new Vertical { Name = string.Format(VerticalNameFormat, 1), Host = "external1.linkme.com" };
            _verticalsCommand.CreateVertical(vertical1);
            Assert.IsFalse(vertical1.IsDeleted);
            AssertVertical(vertical1);
            AssertVerticals(vertical0, vertical1);

            var vertical2 = new Vertical { Name = string.Format(VerticalNameFormat, 2), Host = "external2.linkme.com" };
            _verticalsCommand.CreateVertical(vertical2);
            Assert.IsFalse(vertical2.IsDeleted);
            AssertVertical(vertical2);
            AssertVerticals(vertical0, vertical1, vertical2);

            // Disable it.

            vertical2.IsDeleted = true;
            _verticalsCommand.UpdateVertical(vertical2);
            Assert.IsTrue(vertical2.IsDeleted);
            AssertVertical(vertical2);
            AssertVerticals(vertical0, vertical1);
        }

        private void AssertVerticals(params Vertical[] expectedVerticals)
        {
            var verticals = _verticalsQuery.GetVerticals();
            Assert.AreEqual(expectedVerticals.Length, verticals.Count);

            foreach (var expectedVertical in expectedVerticals)
            {
                var vertical = (from v in verticals where v.Id == expectedVertical.Id select v).SingleOrDefault();
                Assert.IsNotNull(vertical);
                AssertVertical(expectedVertical, vertical);
            }
        }

        private void AssertVertical(Vertical expectedVertical)
        {
            var vertical = _verticalsCommand.GetVertical(expectedVertical.Id);
            Assert.IsNotNull(vertical);
            AssertVertical(expectedVertical, vertical);

            // Query by id.

            vertical = _verticalsQuery.GetVertical(expectedVertical.Id);
            if (!expectedVertical.IsDeleted)
                AssertVertical(expectedVertical, vertical);
            else
                Assert.IsNull(vertical);

            // Query by name.

            vertical = _verticalsQuery.GetVertical(expectedVertical.Name);
            if (!expectedVertical.IsDeleted)
                AssertVertical(expectedVertical, vertical);
            else
                Assert.IsNull(vertical);

            // Query by host.

            if (!string.IsNullOrEmpty(expectedVertical.Host))
            {
                vertical = _verticalsQuery.GetVerticalByHost(expectedVertical.Host);
                if (!expectedVertical.IsDeleted)
                    AssertVertical(expectedVertical, vertical);
                else
                    Assert.IsNull(vertical);
            }
        }

        private static void AssertVertical(Vertical expectedVertical, Vertical vertical)
        {
            Assert.AreEqual(expectedVertical.Id, vertical.Id);
            Assert.AreEqual(expectedVertical.Name, vertical.Name);
            Assert.AreEqual(expectedVertical.IsDeleted, vertical.IsDeleted);
            Assert.AreEqual(expectedVertical.CountryId, vertical.CountryId);
            Assert.AreEqual(expectedVertical.RequiresExternalLogin, vertical.RequiresExternalLogin);
            Assert.AreEqual(expectedVertical.ExternalLoginUrl, vertical.ExternalLoginUrl);
            Assert.AreEqual(expectedVertical.ExternalCookieDomain, vertical.ExternalCookieDomain);
            Assert.AreEqual(expectedVertical.Host, vertical.Host);
            Assert.AreEqual(expectedVertical.SecondaryHost, vertical.SecondaryHost);
            Assert.AreEqual(expectedVertical.TertiaryHost, vertical.TertiaryHost);
            Assert.AreEqual(expectedVertical.Url, vertical.Url);
        }
    }
}