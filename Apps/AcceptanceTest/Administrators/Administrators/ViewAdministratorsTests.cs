using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Administrators
{
    [TestClass]
    public class ViewAdministratorsTests
        : AdministratorsTests
    {
        [TestMethod]
        public void TestAdministrators()
        {
            // Create some administrators.

            var administrators = new Administrator[3];
            for (var index = 0; index < administrators.Length; ++index)
                administrators[index] = CreateAdministrator(index);

            // Login.

            LogIn(administrators[0]);
            Get(GetAdministratorsUrl());

            AssertAdministrators(administrators);
        }

        private void AssertAdministrators(params Administrator[] administrators)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            Assert.AreEqual(administrators.Length, nodes.Count);

            for (var index = 0; index < administrators.Length; ++index)
            {
                var administrator = administrators[index];
                var node = nodes[index];

                // Login column

                var a = node.SelectSingleNode("td[position()=1]/a");
                Assert.AreEqual(administrator.FullName, a.InnerText);
                var url = GetAdministratorUrl(administrator);
                Assert.AreEqual(url.PathAndQuery.ToLower(), a.Attributes["href"].Value.ToLower());

                // Status column.

                Assert.AreEqual(administrator.IsEnabled ? "Enabled" : "Disabled", node.SelectSingleNode("td[position()=2]").InnerText);
            }
        }
    }
}