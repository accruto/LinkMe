using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class LocationTests
        : ViewsTests
    {
        [TestMethod]
        public void TestSuburb()
        {
            var member = CreateMember("Australia", "Norlane VIC 3214");
            TestCandidateUrls(member, () => AssertPersonalLocation(member.Address.Location));
        }

        [TestMethod]
        public void TestCountry()
        {
            var member = CreateMember("Australia", null);
            TestCandidateUrls(member, () => AssertPersonalLocation(member.Address.Location));
        }

        [TestMethod]
        public void TestUnresolvedLocation()
        {
            var member = CreateMember("India", "Mumbai");
            TestCandidateUrls(member, () => AssertPersonalLocation(member.Address.Location));
        }

        private Member CreateMember(string country, string location)
        {
            var member = CreateMember(0);
            member.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(country), location);
            _memberAccountsCommand.UpdateMember(member);
            return member;
        }

        private void AssertPersonalLocation(LocationReference location)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='personal_section resume_section']//table[@class='personal']/tr/td[position()=2]/span");
            AssertLocation(node, location);
        }

        private static void AssertLocation(HtmlNode node, LocationReference location)
        {
            Assert.IsNotNull(node);
            if (location.IsCountry)
            {
                Assert.AreEqual(1, node.ChildNodes.Count);
                Assert.AreEqual(HtmlNodeType.Text, node.ChildNodes[0].NodeType);
                Assert.AreEqual(location.Country.ToString(), node.ChildNodes[0].InnerText.Trim());
            }
            else
            {
                Assert.AreEqual(3, node.ChildNodes.Count);
                Assert.AreEqual(HtmlNodeType.Text, node.ChildNodes[0].NodeType);
                Assert.AreEqual(location.ToString(), node.ChildNodes[0].InnerText.Trim());
                Assert.AreEqual(HtmlNodeType.Element, node.ChildNodes[1].NodeType);
                Assert.AreEqual("br", node.ChildNodes[1].Name);
                Assert.AreEqual(HtmlNodeType.Text, node.ChildNodes[2].NodeType);
                Assert.AreEqual(location.Country.ToString(), node.ChildNodes[2].InnerText.Trim());
            }
        }
    }
}
