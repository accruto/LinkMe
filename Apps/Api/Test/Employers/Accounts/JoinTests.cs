using System.Net;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Api.Areas.Employers.Models.Accounts;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Accounts
{
    [TestClass]
    public class JoinTests
        : WebTestClass
    {
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();

        private ReadOnlyUrl _joinUrl;
        private ReadOnlyUrl _profileUrl;

        private const string LoginId = "mburns";
        private const string Password = "password";
        private const string FirstName = "Monty";
        private const string LastName = "Burns";
        private const string EmailAddress = "mburns@test.linkme.net.au";
        private const string Location = "Norlane VIC 3214";
        private const string OrganisationName = "Springfield Nuclear Plant";
        private const string PhoneNumber = "99999999";

        [TestInitialize]
        public void TestInitialize()
        {
            _joinUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/join");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/profile");
        }

        [TestMethod]
        public void TestJoin()
        {
            // Not logged in.

            AssertJsonError(Deserialize<JsonResponseModel>(Get(HttpStatusCode.Forbidden, _profileUrl)), null, "100", "The user is not logged in.");

            // Join.
            
            var model = new EmployerJoinModel
            {
                LoginId = LoginId,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                Location = Location,
                OrganisationName = OrganisationName,
                PhoneNumber = PhoneNumber,
                SubRole = EmployerSubRole.Recruiter,
            };
            AssertJsonSuccess(Join(model));

            // Check now logged in.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Get(_profileUrl)));

            // Check.

            var id = _loginCredentialsQuery.GetUserId(LoginId);
            AssertEmployer(model, _employersQuery.GetEmployer(id.Value));
        }

        private static void AssertEmployer(EmployerJoinModel model, Employer employer)
        {
            Assert.AreEqual(model.FirstName, employer.FirstName);
            Assert.AreEqual(model.LastName, employer.LastName);
            Assert.AreEqual(model.EmailAddress, employer.EmailAddress.Address);
            Assert.AreEqual(model.Location, employer.Organisation.Address.Location.ToString());
            Assert.AreEqual(model.OrganisationName, employer.Organisation.Name);
            Assert.AreEqual(model.PhoneNumber, employer.PhoneNumber.Number);
            Assert.AreEqual(model.SubRole, employer.SubRole);
        }

        private JsonResponseModel Join(EmployerJoinModel model)
        {
            return Deserialize<JsonResponseModel>(Post(_joinUrl, JsonContentType, Serialize(model, new EmployerJoinModelJavaScriptConverter())));
        }
    }
}
