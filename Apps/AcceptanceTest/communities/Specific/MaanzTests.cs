using LinkMe.Apps.Agents.Test.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class MaanzTests
        : SpecificCommunityTests
    {
        protected override TestCommunity GetTestCommunity()
        {
            return TestCommunity.Maanz;
        }
    }
}
