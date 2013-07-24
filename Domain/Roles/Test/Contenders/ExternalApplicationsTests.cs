using System;
using System.Linq;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Contenders
{
    [TestClass]
    public class ExternalApplicationsTests
        : TestClass
    {
        private readonly IApplicationsCommand _applicationsCommand = Resolve<IApplicationsCommand>();
        private readonly IApplicationsQuery _applicationsQuery = Resolve<IApplicationsQuery>();

        [TestMethod]
        public void TestNoApplications()
        {
            var applicantId = Guid.NewGuid();
            Assert.AreEqual(0, _applicationsQuery.GetApplications<ExternalApplication>(applicantId, true).Count);
        }

        [TestMethod]
        public void TestMultipleJobAds()
        {
            var applicantId = Guid.NewGuid();
            var position1Id = Guid.NewGuid();
            var position2Id = Guid.NewGuid();

            var application1 = new ExternalApplication { ApplicantId = applicantId, PositionId = position1Id };
            _applicationsCommand.CreateApplication(application1);
            AssertApplications(applicantId, application1);

            var application2 = new ExternalApplication { ApplicantId = applicantId, PositionId = position2Id };
            _applicationsCommand.CreateApplication(application2);
            AssertApplications(applicantId, application1, application2);
        }

        [TestMethod]
        public void TestSameJobAd()
        {
            var applicantId = Guid.NewGuid();
            var position1Id = Guid.NewGuid();

            var application1 = new ExternalApplication { ApplicantId = applicantId, PositionId = position1Id, CreatedTime = DateTime.Now.AddMinutes(-5) };
            _applicationsCommand.CreateApplication(application1);
            AssertApplications(applicantId, application1);

            var application2 = new ExternalApplication { ApplicantId = applicantId, PositionId = position1Id, CreatedTime = DateTime.Now.AddMinutes(-1) };
            _applicationsCommand.CreateApplication(application2);
            AssertApplications(applicantId, application1);
        }

        private void AssertApplications(Guid applicantId, params ExternalApplication[] expectedApplications)
        {
            var applications = _applicationsQuery.GetApplications<ExternalApplication>(applicantId, true);
            Assert.AreEqual(expectedApplications.Length, applications.Count);
            Assert.IsTrue(expectedApplications.All(e => (from a in applications where a.PositionId == e.PositionId select a).Any()));
        }
    }
}
