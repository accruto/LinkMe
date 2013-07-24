using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public class VisaStatusTests
        : FilterTests
    {
        [TestMethod]
        public void VisaStatusFilterTest()
        {
            // Create content.

            var citizen = Guid.NewGuid();
            var content = new MemberContent { Member = new Member { Id = citizen }, Candidate = new Candidate { VisaStatus = VisaStatus.Citizen } };
            IndexContent(content);

            var restrictedWorkVisa = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = restrictedWorkVisa }, Candidate = new Candidate { VisaStatus = VisaStatus.RestrictedWorkVisa } };
            IndexContent(content);

            var notApplicable = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = notApplicable }, Candidate = new Candidate { VisaStatus = VisaStatus.NotApplicable } };
            IndexContent(content);

            var none = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = none }, Candidate = new Candidate() };
            IndexContent(content);

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 10);
            Assert.IsTrue(new[] { citizen, restrictedWorkVisa, notApplicable, none }.CollectionEqual(results.MemberIds));

            // Not applicable.

            memberQuery = new MemberSearchQuery { VisaStatusList = new[] { VisaStatus.NotApplicable } };
            results = Search(memberQuery, 0, 10);
            Assert.IsTrue(new[] { notApplicable }.CollectionEqual(results.MemberIds));

            // RestrictedWorkVisa.

            memberQuery = new MemberSearchQuery { VisaStatusList = new[] { VisaStatus.RestrictedWorkVisa } };
            results = Search(memberQuery, 0, 10);
            Assert.IsTrue(new[] { restrictedWorkVisa }.CollectionEqual(results.MemberIds));

            // Citizen, which includes everyone with none.

            memberQuery = new MemberSearchQuery { VisaStatusList = new[] { VisaStatus.Citizen } };
            results = Search(memberQuery, 0, 10);
            Assert.IsTrue(new[] { citizen, notApplicable, none }.CollectionEqual(results.MemberIds));

            // RestrictedWorkVisa and citizen.

            memberQuery = new MemberSearchQuery { VisaStatusList = new[] { VisaStatus.RestrictedWorkVisa, VisaStatus.Citizen } };
            results = Search(memberQuery, 0, 10);
            Assert.IsTrue(new[] { restrictedWorkVisa, citizen, notApplicable, none }.CollectionEqual(results.MemberIds));
        }
    }
}