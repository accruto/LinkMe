using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class SeoTests
        : ViewsTests
    {
        [TestMethod]
        public void TestNoIndex()
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            
            var url = GetCandidateUrl(member, candidate);
            Get(url);
            AssertMetaTag("robots", "noindex");

            url = GetCandidatesUrl(member.Id);
            Get(url);
            AssertMetaTag("robots", "noindex");
        }

        private void AssertMetaTag(string name, string content)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("/html/head/meta");
            Assert.IsNotNull(nodes);
            foreach (var node in nodes)
            {
                if (node.Attributes["name"].Value == name && node.Attributes["content"].Value == content)
                    return;
            }

            Assert.Fail("Count not find meta tag.");
        }
    }
}
