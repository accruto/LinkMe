using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Registration
{
    [TestClass]
    public class ExternalReferralsTests
        : TestClass
    {
        private readonly IReferralsQuery _referralsQuery = Resolve<IReferralsQuery>();
        private readonly IReferralsCommand _referralsCommand = Resolve<IReferralsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();

        [TestInitialize]
        public void ExternalReferralsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestExternalReferralSource()
        {
            Assert.AreEqual(12, _referralsQuery.GetExternalReferralSources().Count);
        }

        [TestMethod]
        public void TestNoReferral()
        {
            var member = _membersCommand.CreateTestMember(0);
            Assert.IsNull(_referralsQuery.GetExternalReferral(member.Id));
        }

        [TestMethod]
        public void TestCreateReferral()
        {
            var externalReferenceSource1 = _referralsQuery.GetExternalReferralSources()[1];
            var externalReferenceSource2 = _referralsQuery.GetExternalReferralSources()[2];

            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);

            Assert.IsNull(_referralsQuery.GetExternalReferral(member1.Id));
            Assert.IsNull(_referralsQuery.GetExternalReferral(member2.Id));

            _referralsCommand.CreateExternalReferral(new ExternalReferral {SourceId = externalReferenceSource1.Id, UserId = member1.Id});
            var referral = _referralsQuery.GetExternalReferral(member1.Id);
            Assert.AreEqual(member1.Id, referral.UserId);
            Assert.AreEqual(externalReferenceSource1.Id, referral.SourceId);
            Assert.IsNull(_referralsQuery.GetExternalReferral(member2.Id));

            _referralsCommand.CreateExternalReferral(new ExternalReferral { SourceId = externalReferenceSource2.Id, UserId = member2.Id });
            referral = _referralsQuery.GetExternalReferral(member1.Id);
            Assert.AreEqual(member1.Id, referral.UserId);
            Assert.AreEqual(externalReferenceSource1.Id, referral.SourceId);
            referral = _referralsQuery.GetExternalReferral(member2.Id);
            Assert.AreEqual(member2.Id, referral.UserId);
            Assert.AreEqual(externalReferenceSource2.Id, referral.SourceId);

            _referralsCommand.UpdateExternalReferral(new ExternalReferral { SourceId = externalReferenceSource2.Id, UserId = member1.Id });
            referral = _referralsQuery.GetExternalReferral(member1.Id);
            Assert.AreEqual(member1.Id, referral.UserId);
            Assert.AreEqual(externalReferenceSource2.Id, referral.SourceId);
            referral = _referralsQuery.GetExternalReferral(member2.Id);
            Assert.AreEqual(member2.Id, referral.UserId);
            Assert.AreEqual(externalReferenceSource2.Id, referral.SourceId);
        }
    }
}