using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class OtherTests
        : ApiTests
    {
        private ReadOnlyUrl _otherUrl;

        private static readonly string[] NewCourses = new[]{ "My new course" };
        private static readonly string[] UpdatedCourses = new[]{"My updated course 1", "My updated course 2"};
        private static readonly string[] NewAwards= new[] { "My new award" };
        private static readonly string[] UpdatedAwards = new[] { "My updated award 1", "My updated award 2" };
        private const string NewProfessional = "My new professional";
        private const string UpdatedProfessional = "My updated professional";
        private const string NewInterests = "My new interests";
        private const string UpdatedInterests = "My updated interests";
        private const string NewAffiliations = "My new affiliations";
        private const string UpdatedAffiliations = "My updated affiliations";
        private const string NewOther = "My new other";
        private const string UpdatedOther = "My updated other";
        private const string NewReferees = "My new referees";
        private const string UpdatedReferees = "My updated referees";

        [TestInitialize]
        public void TestInitialize()
        {
            _otherUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/other");
        }

        [TestMethod]
        public void TestCourses()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Courses = NewCourses };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Courses = UpdatedCourses;
            parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Courses = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestAwards()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Awards = NewAwards };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Awards = UpdatedAwards;
            parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Awards = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestProfessional()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Professional = NewProfessional };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Professional = UpdatedProfessional;
            parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Professional = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestInterests()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Interests = NewInterests };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Interests = UpdatedInterests;
            parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Interests = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestAffiliations()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Affiliations = NewAffiliations };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Affiliations = UpdatedAffiliations;
            parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Affiliations = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestOther()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Other = NewOther };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Other = UpdatedOther;
            parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Other = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestReferees()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Referees = NewReferees };
            var parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Referees = UpdatedReferees;
            parameters = GetParameters(resume);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Referees = null;
            parameters = GetParameters(null);
            AssertJsonSuccess(Other(parameters));
            AssertMember(member, candidate, resume, true);
        }

        private static NameValueCollection GetParameters(IResume resume)
        {
            return new NameValueCollection
            {
                {"Courses", resume == null || resume.Courses == null ? null : string.Join("\n", resume.Courses.ToArray())},
                {"Awards", resume == null || resume.Awards == null ? null : string.Join("\n", resume.Awards.ToArray())},
                {"Professional", resume == null ? null : resume.Professional},
                {"Interests", resume == null ? null : resume.Interests},
                {"Affiliations", resume == null ? null : resume.Affiliations},
                {"Other", resume == null ? null : resume.Other},
                {"Referees", resume == null ? null : resume.Referees},
            };
        }

        private JsonResponseModel Other(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_otherUrl, parameters));
        }

        protected override JsonResponseModel Call()
        {
            return Other(new NameValueCollection());
        }
    }
}
