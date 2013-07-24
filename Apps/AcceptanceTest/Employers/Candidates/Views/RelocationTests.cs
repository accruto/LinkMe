using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class RelocationTests
        : ViewsTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();

        private const string Victoria = "Victoria";
        private const string NewSouthWales = "New South Wales";

        [TestMethod]
        public void TestNone()
        {
            var member = CreateMember();
            TestCandidateUrls(member, () => AssertRelocations());
        }

        [TestMethod]
        public void TestOne()
        {
            var victoria = _locationQuery.ResolveLocation(Australia, Victoria);
            var member = CreateMember(victoria);
            TestCandidateUrls(member, () => AssertRelocations(victoria));
        }

        [TestMethod]
        public void TestTwo()
        {
            var victoria = _locationQuery.ResolveLocation(Australia, Victoria);
            var newSouthWales = _locationQuery.ResolveLocation(Australia, NewSouthWales);

            var member = CreateMember(newSouthWales, victoria);
            TestCandidateUrls(member, () => AssertRelocations(newSouthWales, victoria));
        }

        [TestMethod]
        public void TestCountry()
        {
            var australia = _locationQuery.ResolveLocation(Australia, null);

            var member = CreateMember(australia);
            TestCandidateUrls(member, () => AssertRelocations(australia));
        }

        private void AssertRelocations(params LocationReference[] relocationLocations)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//table[@class='personal']/tr[position()=3]//span[@class='description']");
            Assert.IsNotNull(node);

            if (relocationLocations == null || relocationLocations.Length == 0)
            {
                Assert.AreEqual("Willingness : No", node.InnerText.Trim());
            }
            else
            {
                // Join the texts together to compare because of the possible variations in HTML etc.

                var text = string.Join(" ", (from s in node.InnerText.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries) where !string.IsNullOrEmpty(s.Trim()) select s.Trim()).ToArray());
                var expectedText = string.Join(" ", new[] { "Willingness : Yes", "To : " + string.Join(", ", (from l in relocationLocations select l.IsCountry ? l.Country.ToString() : l.ToString()).ToArray()) });
                Assert.AreEqual(TextUtil.StripToAlphaNumeric(expectedText), TextUtil.StripToAlphaNumeric(text));
            }
        }

        private Member CreateMember(params LocationReference[] relocationLocations)
        {
            var member = CreateMember(0);

            if (relocationLocations != null && relocationLocations.Length > 0)
            {
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                candidate.RelocationPreference = RelocationPreference.Yes;
                candidate.RelocationLocations = relocationLocations;
                _candidatesCommand.UpdateCandidate(candidate);
            }

            return member;
        }
    }
}