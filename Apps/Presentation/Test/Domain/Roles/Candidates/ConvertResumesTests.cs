using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Presentation.Test.Domain.Roles.Candidates
{
    [TestClass]
    public class ConvertResumesTests
        : TestClass
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IResumeFilesQuery _resumeFilesQuery = Resolve<IResumeFilesQuery>();

        [TestInitialize]
        public void ConvertResumesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestConvertResumeToRtf()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);

            var candidate1 = new Candidate {Id = member1.Id};
            _candidatesCommand.CreateCandidate(candidate1);
            var candidate2 = new Candidate { Id = member2.Id };
            _candidatesCommand.CreateCandidate(candidate2);

            var resume1 = _candidateResumesCommand.AddTestResume(candidate1);
            var resume2 = _candidateResumesCommand.AddTestResume(candidate2);

            var resumeFile1 = _resumeFilesQuery.GetResumeFile(employer, member1, candidate1, resume1);
            var resumeFile2 = _resumeFilesQuery.GetResumeFile(employer, member2, candidate2, resume2);

            Assert.IsTrue(resumeFile1.Contents.Length > 1000);
            Assert.IsTrue(resumeFile2.Contents.Length > 1000);
        }
    }
}