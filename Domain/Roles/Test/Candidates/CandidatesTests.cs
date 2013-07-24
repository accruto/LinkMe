using System;
using LinkMe.Domain.Roles.Candidates.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates
{
    [TestClass]
    public class CandidatesTests
        : TestClass
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();

        [TestMethod]
        public void TestUnknownCandidate()
        {
            Assert.IsNull(_candidatesQuery.GetCandidate(Guid.NewGuid()));
            Assert.AreEqual(0, _candidatesQuery.GetCandidates(new[] { Guid.NewGuid() }).Count);
        }
    }
}
