using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Resumes
{
    [TestClass]
    public class ApiEmailCreditLimitsTests
        : ApiActionCreditLimitsTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private ReadOnlyUrl _sendUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _sendUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendresumes");
            _emailServer.ClearEmails();
        }

        protected override MemberAccessReason GetReason()
        {
            return MemberAccessReason.ResumeSent;
        }

        protected override Member CreateMember(int index)
        {
            var member = base.CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected override JsonResponseModel CallAction(Member member)
        {
            var parameters = new NameValueCollection {{"candidateId", member.Id.ToString()}};
            return Deserialize<JsonResponseModel>(Post(_sendUrl, parameters));
        }
    }
}