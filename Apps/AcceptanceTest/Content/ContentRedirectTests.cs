using System.Reflection;
using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Content
{
    [TestClass]
    public class ContentRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestBundleVersion()
        {
            // Bundle versions change across releases, which sometimes means that old versions are asked for.
            // These need to be redirected to the current version.

            var otherVersionUrl = new ReadOnlyApplicationUrl("~/bundle/12.7.3.3/css/site1.aspx?v=J_B3Ka2u_VXMW5uzq-yJCcXh4nB_AJAzw-ObqkEnyWE1");

            var version = StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly());
            var currentVersionUrl = new ReadOnlyApplicationUrl("~/bundle/" + version + "/css/site1.aspx?v=J_B3Ka2u_VXMW5uzq-yJCcXh4nB_AJAzw-ObqkEnyWE1");

            AssertNoRedirect(currentVersionUrl);
            AssertRedirect(otherVersionUrl, currentVersionUrl, currentVersionUrl);
        }

        [TestMethod]
        public void TestCombres()
        {
            // Old combres usage should be simply redirected to the home page.

            var url = new ReadOnlyApplicationUrl("~/combres.axd/siteMasterCssB/12.6.4.12");
            AssertRedirect(url, HomeUrl, HomeUrl);
        }
    }
}
