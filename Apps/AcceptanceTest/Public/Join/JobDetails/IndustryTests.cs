using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class IndustryTests
        : FieldsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private Industry _accounting;
        private Industry _administration;

        [TestInitialize]
        public void TestInitialize()
        {
            _accounting = _industriesQuery.GetIndustry("Accounting");
            _administration = _industriesQuery.GetIndustry("Administration");
        }

        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.IsNull(candidate.Industries);
            candidate.Industries = new[] { _accounting, _administration };
        }

        private void AssertDefault()
        {
            Assert.AreEqual(0, GetSelectedIndustryIds().Count);
        }
    }
}
