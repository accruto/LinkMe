using System;
using System.Linq;
using LinkMe.Domain.Users.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members
{
    [TestClass]
    public class MembersQueryTests
        : TestClass
    {
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();

        [TestMethod]
        public void TestIdList()
        {
            // No exceptions should be thrown.

            for (var index = 1; index <= 5; ++index)
                _membersQuery.GetMembers(from id in Enumerable.Range(0, index * 1000) select Guid.NewGuid());
        }
    }
}
