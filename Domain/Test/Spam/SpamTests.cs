using System;
using LinkMe.Domain.Spam;
using LinkMe.Domain.Spam.Commands;
using LinkMe.Domain.Spam.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Spam
{
    [TestClass]
    public class SpamTests
        : TestClass
    {
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";

        private readonly ISpamRepository _spamRepository = Resolve<ISpamRepository>();
        private readonly ISpamCommand _spamCommand = Resolve<ISpamCommand>();
        private readonly ISpamQuery _spamQuery = Resolve<ISpamQuery>();

        [TestInitialize]
        public void SpamTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateSpammerByName()
        {
            var spammer1 = CreateSpammer(1);
            var spammer2 = CreateSpammer(2);
            var spammer3 = CreateSpammer(3);

            Assert.IsFalse(_spamQuery.IsSpammer(spammer1));
            Assert.IsFalse(_spamQuery.IsSpammer(spammer2));
            Assert.IsFalse(_spamQuery.IsSpammer(spammer3));

            // Create a spammer.

            _spamCommand.CreateSpammer(spammer1);
            Assert.IsTrue(_spamQuery.IsSpammer(spammer1));
            Assert.IsFalse(_spamQuery.IsSpammer(spammer2));
            Assert.IsFalse(_spamQuery.IsSpammer(spammer3));

            // Create another.

            _spamCommand.CreateSpammer(spammer2);
            Assert.IsTrue(_spamQuery.IsSpammer(spammer1));
            Assert.IsTrue(_spamQuery.IsSpammer(spammer2));
            Assert.IsFalse(_spamQuery.IsSpammer(spammer3));

            // Create again.

            _spamCommand.CreateSpammer(spammer2);
            Assert.IsTrue(_spamQuery.IsSpammer(spammer1));
            Assert.IsTrue(_spamQuery.IsSpammer(spammer2));
            Assert.IsFalse(_spamQuery.IsSpammer(spammer3));
        }

        [TestMethod]
        public void TestReportSpammer()
        {
            // No reports.

            var spammer = new Spammer {UserId = Guid.NewGuid()};

            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report once.

            _spamCommand.ReportSpammer(Guid.NewGuid(), spammer);
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report twice.

            _spamCommand.ReportSpammer(Guid.NewGuid(), spammer);
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report thrice.

            _spamCommand.ReportSpammer(Guid.NewGuid(), spammer);
            Assert.IsTrue(_spamQuery.IsSpammer(spammer));
        }

        [TestMethod]
        public void TestReportSpammerSameUser()
        {
            // No reports.

            var spammer = new Spammer {UserId = Guid.NewGuid()};
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report once.

            var reportedById = Guid.NewGuid();
            _spamCommand.ReportSpammer(reportedById, spammer);
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report twice.

            _spamCommand.ReportSpammer(reportedById, spammer);
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report thrice.

            _spamCommand.ReportSpammer(reportedById, spammer);
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Different users.

            _spamCommand.ReportSpammer(Guid.NewGuid(), spammer);
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            _spamCommand.ReportSpammer(Guid.NewGuid(), spammer);
            Assert.IsTrue(_spamQuery.IsSpammer(spammer));
        }

        [TestMethod]
        public void TestReportSpammerDates()
        {
            // No reports.

            var spammer = new Spammer {UserId = Guid.NewGuid()};
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report once.

            var now = DateTime.Now;
            _spamRepository.ReportSpammer(new SpammerReport { ReportedByUserId = Guid.NewGuid(), ReportedTime = now.AddDays(-1 * 40), Spammer = spammer });
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report twice.

            _spamRepository.ReportSpammer(new SpammerReport { ReportedByUserId = Guid.NewGuid(), ReportedTime = now.AddDays(-1 * 20), Spammer = spammer });
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report thrice.

            _spamRepository.ReportSpammer(new SpammerReport { ReportedByUserId = Guid.NewGuid(), ReportedTime = now.AddDays(-1 * 10), Spammer = spammer });
            Assert.IsFalse(_spamQuery.IsSpammer(spammer));

            // Report again within the timeframe.

            _spamCommand.ReportSpammer(Guid.NewGuid(), spammer);
            Assert.IsTrue(_spamQuery.IsSpammer(spammer));
        }

        private static Spammer CreateSpammer(int index)
        {
            return new Spammer
            {
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
            };
        }
    }
}
