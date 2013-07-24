using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.Api
{
    [TestClass]
    public class ApiJoinTests
        : WebTestClass
    {
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();

        private ReadOnlyUrl _apiJoinUrl;

        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress = "homer@test.linkme.net.au";
        private const string Password = "password";

        [TestInitialize]
        public void TestInitialize()
        {
            _apiJoinUrl = new ReadOnlyApplicationUrl(true, "~/join/api");
        }

        [TestMethod]
        public void TestJoin()
        {
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, EmailAddress, Password, Password, true))));
            var member = _membersQuery.GetMember(EmailAddress);
            Assert.AreEqual(FirstName, member.FirstName);
            Assert.AreEqual(LastName, member.LastName);
            Assert.IsTrue(member.IsEnabled);
            Assert.IsFalse(member.IsActivated);
        }

        [TestMethod]
        public void TestFirstNameErrors()
        {
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters("", LastName, EmailAddress, Password, Password, true))), "FirstName", "The first name is required.");
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters("a", LastName, EmailAddress, Password, Password, true))), "FirstName", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(new string('a', 400), LastName, EmailAddress, Password, Password, true))), "FirstName", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestLastNameErrors()
        {
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, "", EmailAddress, Password, Password, true))), "LastName", "The last name is required.");
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, "a", EmailAddress, Password, Password, true))), "LastName", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, new string('a', 400), EmailAddress, Password, Password, true))), "LastName", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestEmailAddressErrors()
        {
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, "", Password, Password, true))), "EmailAddress", "The email address is required.");
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, "a", Password, Password, true))), "EmailAddress", "The email address must be valid and have less than 320 characters.");
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, new string('a', 400), Password, Password, true))), "EmailAddress", "The email address must be valid and have less than 320 characters.");
        }

        [TestMethod]
        public void TestPasswordErrors()
        {
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, EmailAddress, "", Password, true))), "ConfirmPassword", "The confirm password and password must match.", "Password", "The password is required.");
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, EmailAddress, "a", Password, true))), "ConfirmPassword", "The confirm password and password must match.", "Password", "The password must be between 6 and 50 characters in length.");
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, EmailAddress, Password, "", true))), "ConfirmPassword", "The confirm password and password must match.", "ConfirmPassword", "The confirm password is required.");
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, EmailAddress, Password, "a", true))), "ConfirmPassword", "The confirm password and password must match.");
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, EmailAddress, Password, Password + Password, true))), "ConfirmPassword", "The confirm password and password must match.");
        }

        [TestMethod]
        public void TestAcceptTermsErrors()
        {
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters(FirstName, LastName, EmailAddress, Password, Password, false))), "AcceptTerms", "Please accept the terms and conditions.");
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetParameters("a", LastName, EmailAddress, Password, Password, false))), "FirstName", "The first name must be between 2 and 30 characters in length and not have any invalid characters.", "AcceptTerms", "Please accept the terms and conditions.");
        }

        private static NameValueCollection GetParameters(string firstName, string lastName, string emailAddress, string password, string confirmPassword, bool acceptTerms)
        {
            return new NameValueCollection
            {
                {"FirstName", firstName},
                {"LastName", lastName},
                {"EmailAddress", emailAddress},
                {"JoinPassword", password},
                {"JoinConfirmPassword", confirmPassword},
                {"AcceptTerms", acceptTerms.ToString()},
            };
        }
    }
}
