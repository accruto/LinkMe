using System;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Limits
{
    [TestClass]
    public abstract class LimitsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IEmployerViewsRepository _repository = Resolve<IEmployerViewsRepository>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void LimitsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestDailyLimit()
        {
            var employer = CreateEmployer();

            // Access up to the daily limit.

            Member member;
            Tuple<int, int> counts;

            for (var index = 0; index < DailyLimit; ++index)
            {
                member = CreateMember(index);

                // Check the counts.

                counts = _repository.GetMemberAccessCounts(employer.Id, member.Id, new[] { GetAccessReason() });
                Assert.AreEqual(index, counts.Item1);
                Assert.AreEqual(index, counts.Item2);

                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
            }

            // Go one more.

            member = CreateMember(DailyLimit);

            counts = _repository.GetMemberAccessCounts(employer.Id, member.Id, new[] { GetAccessReason() });
            Assert.AreEqual(DailyLimit, counts.Item1);
            Assert.AreEqual(DailyLimit, counts.Item2);

            try
            {
                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }

            try
            {
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }
        }

        [TestMethod]
        public void TestDailyLimitSameMembers()
        {
            var employer = CreateEmployer();

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);

            // Access the same members upto the daily limit.

            Tuple<int, int> counts;

            for (var index = 0; index < DailyLimit / 2; ++index)
            {
                // Check the counts.

                counts = _repository.GetMemberAccessCounts(employer.Id, member0.Id, new[] { GetAccessReason() });
                Assert.AreEqual(index == 0 ? 0 : 1, counts.Item1);
                Assert.AreEqual(index == 0 ? 0 : 1, counts.Item2);

                counts = _repository.GetMemberAccessCounts(employer.Id, member1.Id, new[] { GetAccessReason() });
                Assert.AreEqual(index == 0 ? 0 : 1, counts.Item1);
                Assert.AreEqual(index == 0 ? 0 : 1, counts.Item2);

                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member0), GetAccessReason());
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member0), GetAccessReason());
                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), GetAccessReason());
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), GetAccessReason());
            }

            // Go one more.

            counts = _repository.GetMemberAccessCounts(employer.Id, member0.Id, new[] { GetAccessReason() });
            Assert.AreEqual(1, counts.Item1);
            Assert.AreEqual(1, counts.Item2);

            counts = _repository.GetMemberAccessCounts(employer.Id, member1.Id, new[] { GetAccessReason() });
            Assert.AreEqual(1, counts.Item1);
            Assert.AreEqual(1, counts.Item2);

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member0), GetAccessReason());
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member0), GetAccessReason());
            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), GetAccessReason());
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), GetAccessReason());
        }

        [TestMethod]
        public void TestBulkDailyLimit()
        {
            var employer = CreateEmployer();

            // Access up to the bulk daily limit doing 2 at a time.

            for (var index = 0; index < BulkDailyLimit / 2; ++index)
            {
                var member1 = CreateMember(2 * index);
                var member2 = CreateMember(2 * index + 1);

                // Check the counts.

                var counts = _repository.GetMemberAccessCounts(employer.Id, new[] { member1.Id, member2.Id }, new[] { GetAccessReason() });
                Assert.AreEqual(2 * index, counts.Item1);
                Assert.AreEqual(2 * index, counts.Item2);

                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member1, member2 }), GetAccessReason());
            }

            // Go two more.

            try
            {
                var member1 = CreateMember(BulkDailyLimit);
                var member2 = CreateMember(BulkDailyLimit + 1);

                var counts = _repository.GetMemberAccessCounts(employer.Id, new[] { member1.Id, member2.Id }, new[] { GetAccessReason() });
                Assert.AreEqual(BulkDailyLimit, counts.Item1);
                Assert.AreEqual(BulkDailyLimit, counts.Item2);

                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member1, member2 }), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }
        }

        [TestMethod]
        public void TestJustOverBulkDailyLimit()
        {
            var employer = CreateEmployer();

            // Access half.

            var members = new Member[BulkDailyLimit / 2];
            for (var index = 0; index < BulkDailyLimit / 2; ++index)
                members[index] = CreateMember(index);

            // Check the counts.

            var counts = _repository.GetMemberAccessCounts(employer.Id, from m in members select m.Id, new[] { GetAccessReason() });
            Assert.AreEqual(0, counts.Item1);
            Assert.AreEqual(0, counts.Item2);

            // Access them.

            _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), GetAccessReason());

            // Go over limit by one.

            try
            {
                members = new Member[BulkDailyLimit / 2 + 1];
                for (var index = BulkDailyLimit / 2; index < BulkDailyLimit; ++index)
                    members[index - BulkDailyLimit / 2] = CreateMember(index);
                members[BulkDailyLimit /2] = CreateMember(BulkDailyLimit + 1);

                counts = _repository.GetMemberAccessCounts(employer.Id, from m in members select m.Id, new[] { GetAccessReason() });
                Assert.AreEqual(BulkDailyLimit / 2, counts.Item1);
                Assert.AreEqual(BulkDailyLimit / 2, counts.Item2);
                Assert.AreEqual(BulkDailyLimit + 1, counts.Item1 + members.Length);
                Assert.AreEqual(BulkDailyLimit + 1, counts.Item2 + members.Length);

                // Should throw because this will put it over the limit.

                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }
        }

        [TestMethod]
        public void TestJumpOverBulkDailyLimit()
        {
            var employer = CreateEmployer();

            // Access half.

            var members = new Member[BulkDailyLimit / 2];
            for (var index = 0; index < BulkDailyLimit / 2; ++index)
                members[index] = CreateMember(index);

            // Check the counts.

            var counts = _repository.GetMemberAccessCounts(employer.Id, from m in members select m.Id, new[] { GetAccessReason() });
            Assert.AreEqual(0, counts.Item1);
            Assert.AreEqual(0, counts.Item2);

            // Access them.

            _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), GetAccessReason());

            // Go over limit in a big way.

            try
            {
                members = new Member[BulkDailyLimit];
                for (var index = BulkDailyLimit / 2; index < BulkDailyLimit / 2 + BulkDailyLimit; ++index)
                    members[index - BulkDailyLimit / 2] = CreateMember(index);

                counts = _repository.GetMemberAccessCounts(employer.Id, from m in members select m.Id, new[] { GetAccessReason() });
                Assert.AreEqual(BulkDailyLimit / 2, counts.Item1);
                Assert.AreEqual(BulkDailyLimit / 2, counts.Item2);
                Assert.AreEqual(BulkDailyLimit / 2 + BulkDailyLimit, counts.Item1 + members.Length);
                Assert.AreEqual(BulkDailyLimit / 2 + BulkDailyLimit, counts.Item2 + members.Length);

                // Should throw because this will put it over the limit.

                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }
        }

        [TestMethod]
        public void TestUsedDailyLimitCanStillDoBulk()
        {
            var employer = CreateEmployer();

            // Access up to the daily limit.

            Member member;
            for (var index = 0; index < DailyLimit; ++index)
            {
                member = CreateMember(index);
                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
            }

            // Go one more.

            member = CreateMember(DailyLimit);

            try
            {
                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }

            try
            {
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }

            // Access up to the bulk daily limit doing 2 at a time.

            for (var index = 0; index < BulkDailyLimit / 2; ++index)
            {
                var member1 = CreateMember(2 * index + DailyLimit + 1);
                var member2 = CreateMember(2 * index + 1 + DailyLimit + 1);
                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member1, member2 }), GetAccessReason());
            }

            // Go two more.

            try
            {
                var member1 = CreateMember(BulkDailyLimit + DailyLimit + 1);
                var member2 = CreateMember(BulkDailyLimit + 1 + DailyLimit + 1);
                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member1, member2 }), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }
        }

        [TestMethod]
        public void TestUsedBulkLimitCanStillDo()
        {
            var employer = CreateEmployer();

            // Access up to the bulk daily limit doing 2 at a time.

            for (var index = 0; index < BulkDailyLimit / 2; ++index)
            {
                var member1 = CreateMember(2 * index);
                var member2 = CreateMember(2 * index + 1);
                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member1, member2 }), GetAccessReason());
            }

            // Go two more.

            try
            {
                var member1 = CreateMember(BulkDailyLimit);
                var member2 = CreateMember(BulkDailyLimit + 1);
                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member1, member2 }), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }

            // Access up to the daily limit.

            Member member;
            for (var index = 0; index < DailyLimit; ++index)
            {
                member = CreateMember(index + BulkDailyLimit + 2);
                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
            }

            // Go one more.

            member = CreateMember(DailyLimit + BulkDailyLimit + 2);

            try
            {
                _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }
            
            try
            {
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), GetAccessReason());
                Assert.Fail();
            }
            catch (TooManyAccessesException)
            {
            }
        }

        protected abstract MemberAccessReason GetAccessReason();
        protected abstract int DailyLimit { get; }
        protected abstract int BulkDailyLimit { get; }

        private Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        private IEmployer CreateEmployer()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });
            return employer;
        }
    }
}
