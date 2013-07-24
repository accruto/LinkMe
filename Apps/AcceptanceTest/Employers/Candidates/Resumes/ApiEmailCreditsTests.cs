using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Resumes
{
    [TestClass]
    public class ApiEmailCreditsTests
        : ApiActionCreditsTests
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

        protected override Member CreateMember(int index)
        {
            var member = base.CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected override JsonResponseModel CallAction(Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            return Deserialize<JsonResponseModel>(Post(_sendUrl, parameters));
        }

        protected override MemberAccessReason? AssertModel(JsonResponseModel model, Employer employer, Member[] members)
        {
            AssertJsonSuccess(model);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertAttachment(members.Length == 1 ? members[0].FullName + ".doc" : "Resumes.zip", members.Length == 1 ? "application/msword" : "application/zip");

            return MemberAccessReason.ResumeSent;
        }
    }
}