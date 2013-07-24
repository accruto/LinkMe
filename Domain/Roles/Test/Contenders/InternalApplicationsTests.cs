using System;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Test;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Contenders
{
    [TestClass]
    public class InternalApplicationsTests
        : TestClass
    {
        private readonly IApplicationsQuery _applicationsQuery = Resolve<IApplicationsQuery>();
        private readonly IApplicationsCommand _applicationsCommand = Resolve<IApplicationsCommand>();

        [TestMethod]
        public void TestEmptyPositions()
        {
            var applicantId = Guid.NewGuid();

            // Null.

            Assert.AreEqual(0, _applicationsQuery.GetApplications<InternalApplication>(applicantId, null, true).Count);

            // Empty.

            Assert.AreEqual(0, _applicationsQuery.GetApplications<InternalApplication>(applicantId, new Guid[0], true).Count);
        }

        [TestMethod]
        public void TestErrors()
        {
            var applicantId = Guid.NewGuid();
            var positionId = Guid.NewGuid();

            var application = new InternalApplication
            {
                PositionId = positionId,
                ApplicantId = applicantId,
                CoverLetterText = new string('a', 2000),
            };
            AssertException.Thrown<ValidationErrorsException>(() => _applicationsCommand.CreateApplication(application));

            application.CoverLetterText = new string('a', 900);
            _applicationsCommand.CreateApplication(application);

            AssertApplication(application, _applicationsQuery.GetApplication<InternalApplication>(application.Id, true));

            var applications = _applicationsQuery.GetApplications<InternalApplication>(applicantId, true);
            Assert.AreEqual(1, applications.Count);
            AssertApplication(application, applications[0]);

            applications = _applicationsQuery.GetApplications<InternalApplication>(new[] { application.Id }, true);
            Assert.AreEqual(1, applications.Count);
            AssertApplication(application, applications[0]);

            applications = _applicationsQuery.GetApplications<InternalApplication>(applicantId, new[] { positionId }, true);
            Assert.AreEqual(1, applications.Count);
            AssertApplication(application, applications[0]);

            applications = _applicationsQuery.GetApplicationsByPositionId<InternalApplication>(positionId, true);
            Assert.AreEqual(1, applications.Count);
            AssertApplication(application, applications[0]);
        }

        private static void AssertApplication(InternalApplication expectedApplication, InternalApplication application)
        {
            Assert.AreEqual(expectedApplication.Id, application.Id);
            Assert.AreEqual(expectedApplication.PositionId, application.PositionId);
            Assert.AreEqual(expectedApplication.ApplicantId, application.ApplicantId);
            Assert.AreEqual(expectedApplication.ApplicantEmail, application.ApplicantEmail);
            Assert.AreEqual(expectedApplication.CoverLetterText, application.CoverLetterText);
            Assert.AreEqual(expectedApplication.IsPending, application.IsPending);
            Assert.AreEqual(expectedApplication.IsPositionFeatured, application.IsPositionFeatured);
            Assert.AreEqual(expectedApplication.ResumeFileId, application.ResumeFileId);
            Assert.AreEqual(expectedApplication.ResumeId, application.ResumeId);
        }
    }
}
