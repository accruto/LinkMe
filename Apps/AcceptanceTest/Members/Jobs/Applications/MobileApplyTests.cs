using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications
{
    [TestClass]
    public abstract class MobileApplyTests
        : ApplyTests
    {
        [TestInitialize]
        public void MobileApplyTestInitialize()
        {
            Browser.UseMobileUserAgent = true;
        }

        protected void AssertView(Guid jobAdId)
        {
            // There should be 2 of them.

            var buttons = Browser.CurrentHtml.DocumentNode.SelectNodes("//a[@class='button applynow']");
            Assert.IsNotNull(buttons);
            Assert.AreEqual(2, buttons.Count);
            foreach (var button in buttons)
                Assert.IsTrue(GetApplyUrl(jobAdId).PathAndQuery.Equals(button.Attributes["href"].Value, StringComparison.InvariantCultureIgnoreCase));
        }

        protected void Apply(Guid jobAdId, bool useProfile)
        {
            Get(GetApplyUrl(jobAdId));
        }
    }
}
