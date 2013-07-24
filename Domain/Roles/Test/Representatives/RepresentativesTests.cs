using System;
using System.Linq;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Representatives.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Representatives
{
    [TestClass]
    public class RepresentativesTests
        : TestClass
    {
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();
        private readonly IRepresentativesQuery _representativesQuery = Resolve<IRepresentativesQuery>();

        [TestMethod]
        public void TestNoRepresentative()
        {
            AssertRepresentative(Guid.NewGuid());
        }

        [TestMethod]
        public void TestNoRepresentees()
        {
            AssertRepresentees(Guid.NewGuid());
        }

        [TestMethod]
        public void TestCreateRepresentative()
        {
            var representeeId = Guid.NewGuid();
            var representativeId = Guid.NewGuid();

            // Create.

            _representativesCommand.CreateRepresentative(representeeId, representativeId);
            AssertRepresentative(representeeId, representativeId);
            AssertRepresentees(representativeId, representeeId);
        }

        [TestMethod]
        public void TestDeleteRepresentative()
        {
            var representeeId = Guid.NewGuid();
            var representativeId = Guid.NewGuid();

            // Create.

            _representativesCommand.CreateRepresentative(representeeId, representativeId);
            AssertRepresentative(representeeId, representativeId);
            AssertRepresentees(representativeId, representeeId);

            // Delete.

            _representativesCommand.DeleteRepresentative(representeeId, representativeId);
            AssertRepresentative(representeeId);
            AssertRepresentees(representativeId);
        }

        [TestMethod]
        public void TestChangeRepresentative()
        {
            var representeeId = Guid.NewGuid();
            var representativeId = Guid.NewGuid();
            var newRepresentativeId = Guid.NewGuid();

            // Create.

            _representativesCommand.CreateRepresentative(representeeId, representativeId);
            AssertRepresentative(representeeId, representativeId);
            AssertRepresentees(representativeId, representeeId);
            AssertRepresentees(newRepresentativeId);

            // Change.

            _representativesCommand.CreateRepresentative(representeeId, newRepresentativeId);
            AssertRepresentative(representeeId, newRepresentativeId);
            AssertRepresentees(representativeId);
            AssertRepresentees(newRepresentativeId, representeeId);
        }

        [TestMethod]
        public void TestMultipleRepresentatives()
        {
            var representeeId1 = Guid.NewGuid();
            var representeeId2 = Guid.NewGuid();
            var representeeId3 = Guid.NewGuid();
            var representativeId1 = Guid.NewGuid();
            var representativeId2 = Guid.NewGuid();

            AssertRepresentative(representeeId1);
            AssertRepresentative(representeeId2);
            AssertRepresentative(representeeId3);
            AssertRepresentees(representativeId1);
            AssertRepresentees(representativeId2);

            // Create.

            _representativesCommand.CreateRepresentative(representeeId1, representativeId1);
            AssertRepresentative(representeeId1, representativeId1);
            AssertRepresentative(representeeId2);
            AssertRepresentative(representeeId3);
            AssertRepresentees(representativeId1, representeeId1);
            AssertRepresentees(representativeId2);

            // Create.

            _representativesCommand.CreateRepresentative(representeeId2, representativeId1);
            AssertRepresentative(representeeId1, representativeId1);
            AssertRepresentative(representeeId2, representativeId1);
            AssertRepresentative(representeeId3);
            AssertRepresentees(representativeId1, representeeId1, representeeId2);
            AssertRepresentees(representativeId2);

            // Create.

            _representativesCommand.CreateRepresentative(representeeId3, representativeId2);
            AssertRepresentative(representeeId1, representativeId1);
            AssertRepresentative(representeeId2, representativeId1);
            AssertRepresentative(representeeId3, representativeId2);
            AssertRepresentees(representativeId1, representeeId1, representeeId2);
            AssertRepresentees(representativeId2, representeeId3);

            // Create.

            _representativesCommand.CreateRepresentative(representeeId1, representativeId2);
            AssertRepresentative(representeeId1, representativeId2);
            AssertRepresentative(representeeId2, representativeId1);
            AssertRepresentative(representeeId3, representativeId2);
            AssertRepresentees(representativeId1, representeeId2);
            AssertRepresentees(representativeId2, representeeId1, representeeId3);

            // Create.

            _representativesCommand.CreateRepresentative(representeeId2, representativeId2);
            AssertRepresentative(representeeId1, representativeId2);
            AssertRepresentative(representeeId2, representativeId2);
            AssertRepresentative(representeeId3, representativeId2);
            AssertRepresentees(representativeId1);
            AssertRepresentees(representativeId2, representeeId1, representeeId2, representeeId3);
        }

        private void AssertRepresentative(Guid representeeId)
        {
            Assert.IsNull(_representativesQuery.GetRepresentativeId(representeeId));
        }

        private void AssertRepresentative(Guid representeeId, Guid expectedRepresentativeId)
        {
            Assert.AreEqual(expectedRepresentativeId, _representativesQuery.GetRepresentativeId(representeeId));
        }

        private void AssertRepresentees(Guid representativeId, params Guid[] expectedRepresenteesId)
        {
            var representeeIds = _representativesQuery.GetRepresenteeIds(representativeId);
            Assert.AreEqual(expectedRepresenteesId.Length, representeeIds.Count);
            Assert.AreEqual(expectedRepresenteesId.Length, expectedRepresenteesId.Distinct().Count());
            Assert.AreEqual(representeeIds.Count, representeeIds.Distinct().Count());
            foreach (var representeeId in representeeIds)
                Assert.IsTrue(expectedRepresenteesId.Contains(representeeId));
        }
    }
}