using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Communities
{
    [TestClass]
    public class NotLoggedInAdvancedSearchTests
        : AdvancedSearchTests
    {
        protected override Member CreateMember(int index, Community community)
        {
            return null;
        }
    }
}