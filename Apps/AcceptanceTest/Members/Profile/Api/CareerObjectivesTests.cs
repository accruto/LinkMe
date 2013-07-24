using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class CareerObjectivesTests
        : ApiTests
    {
        private ReadOnlyUrl _careerObjectivesUrl;

        private const string NewObjective = "My new objective";
        private const string UpdatedObjective = "My updated objective";
        private const string NewSummary = "My new summary";
        private const string UpdatedSummary = "My updated summary";
        private const string NewSkills = "My new skills";
        private const string UpdatedSkills = "My updated skills";

        [TestInitialize]
        public void TestInitialize()
        {
            _careerObjectivesUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/careerobjectives");
        }

        [TestMethod]
        public void TestObjective()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Objective = NewObjective };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Objective = UpdatedObjective;
            parameters = GetParameters(resume);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Objective = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestSummary()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Summary = NewSummary };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Summary = UpdatedSummary;
            parameters = GetParameters(resume);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Summary = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestSkills()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Skills = NewSkills };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Skills = UpdatedSkills;
            parameters = GetParameters(resume);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Skills = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(CareerObjectives(parameters));
            AssertMember(member, candidate, resume, true);
        }

        private static NameValueCollection GetParameters(IResume resume)
        {
            return new NameValueCollection
            {
                {"Objective", resume == null ? null : resume.Objective},
                {"Summary", resume == null ? null : resume.Summary},
                {"Skills", resume == null ? null : resume.Skills},
            };
        }

        private JsonResponseModel CareerObjectives(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_careerObjectivesUrl, parameters));
        }

        protected override JsonResponseModel Call()
        {
            return CareerObjectives(new NameValueCollection());
        }
    }
}
