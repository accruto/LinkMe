using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communities
{
    [TestClass]
    public class CustodiansTests
        : CommunitiesTests
    {
        [TestMethod]
        public void TestNoCustodians()
        {
            // Create a community.

            var community = CreateCommunity();

            // Login.

            var administrator = CreateAdministrator(1);
            LogIn(administrator);
            Get(GetCustodiansUrl(community));

            AssertPageContains("There are no custodians associated with this community.");
        }

        [TestMethod]
        public void TestCustodians()
        {
            // Create a community.

            var community = CreateCommunity();

            // Create some custodians.

            var custodians = new Custodian[3];
            for (var index = 0; index < custodians.Length; ++index)
                custodians[index] = CreateCustodian(index, community);

            // Login.

            var administrator = CreateAdministrator(4);
            LogIn(administrator);
            Get(GetCustodiansUrl(community));

            AssertPageDoesNotContain("There are no custodians associated with this community.");
            AssertCustodians(custodians);
        }

        private void AssertCustodians(params Custodian[] custodians)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            Assert.AreEqual(custodians.Length, nodes.Count);

            for (var index = 0; index < custodians.Length; ++index)
            {
                var custodian = custodians[index];
                var node = nodes[index];

                // Login column

                var a = node.SelectSingleNode("td[position()=1]/a");
                Assert.AreEqual(custodian.FullName, a.InnerText);
                var url = GetCustodianUrl(custodian);
                Assert.AreEqual(url.PathAndQuery.ToLower(), a.Attributes["href"].Value.ToLower());

                // Status column.

                Assert.AreEqual(custodian.IsEnabled ? "Enabled" : "Disabled", node.SelectSingleNode("td[position()=2]").InnerText);
            }
        }

    }
}
