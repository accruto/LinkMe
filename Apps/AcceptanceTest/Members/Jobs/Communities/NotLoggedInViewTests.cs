using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Communities
{
    [TestClass]
    public class NotLoggedInViewTests
        : ViewTests
    {
        protected override Member CreateMember(int index, Community community)
        {
            return null;
        }
    }
}