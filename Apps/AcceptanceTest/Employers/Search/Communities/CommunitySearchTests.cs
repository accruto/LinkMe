using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Communities
{
    [TestClass]
    public class CommunitySearchTests
        : CommunityTests
    {
        [TestMethod]
        public void TestSearchByCommunity()
        {
            // Create communities and members.

            var community0 = CreateCommunity(0);
            var community1 = CreateCommunity(1);

            var index = 0;
            var members0 = CreateMembers(ref index, community0, 2);
            var members1 = CreateMembers(ref index, community1, 3);
            var members2 = CreateMembers(ref index, null, 4);

            // Search for community 0.

            AssertSearch(community0, members0);

            // Search for community 1.

            AssertSearch(community1, members1);

            // Search with no comunnity, should get everyone.

            AssertSearch(null, members0.Concat(members1).Concat(members2).ToArray());
        }
    }
}