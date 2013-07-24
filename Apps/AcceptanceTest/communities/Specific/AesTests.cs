using LinkMe.Apps.Agents.Test.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class AesTests
        : SpecificCommunityTests
    {
        protected override TestCommunity GetTestCommunity()
        {
            return TestCommunity.Aes;
        }
    }
}
