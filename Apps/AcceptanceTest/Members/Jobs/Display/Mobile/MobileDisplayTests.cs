using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Mobile
{
    [TestClass]
    public class MobileDisplayTests
        : DisplayTests
    {
        [TestInitialize]
        public void MobileDisplayTestInitialize()
        {
            Browser.UseMobileUserAgent = true;
        }
    }
}
