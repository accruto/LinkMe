using System.IO;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Api.Areas.Employers.Models.Accounts;
using LinkMe.Apps.Api.Areas.Employers.Models.Purchases;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Purchase
{
    [TestClass]
    public class PurchaseTests
        : WebTestClass
    {
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private ReadOnlyUrl _purchaseUrl;
        private ReadOnlyUrl _profileUrl;
        protected const string BusinessAnalyst = "business analyst";

        [TestInitialize]
        public void TestInitialize()
        {
            ClearSearchIndexes();

            _purchaseUrl = new ReadOnlyApplicationUrl(true, "~/v1/credits/purchase");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/profile");
        }

        [TestMethod]
        public void VerificationTest()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var encodedTransactionString = GetVerificationString();

            var verificationResponse = Verify(encodedTransactionString);

            Assert.IsTrue(verificationResponse.Success);
            Assert.IsNull(verificationResponse.Errors);

            //Now ensure the credits actually got added
            var profileResponse = GetProfile();

            Assert.AreEqual(employer.GetLoginId(), profileResponse.LoginId);
            Assert.AreEqual(1, profileResponse.Credits);
        }

        private JsonResponseModel Verify(string encodedTransactionString)
        {
            var model = new VerifyModel { EncodedTransactionReceipt = encodedTransactionString };
            return Deserialize<JsonResponseModel>(Post(_purchaseUrl, JsonContentType, Serialize(model)));
        }

        private ProfileModel GetProfile()
        {
            return Deserialize<ProfileModel>(Get(_profileUrl));
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private static string GetVerificationString()
        {
            var path = FileSystem.GetAbsolutePath(@"Apps\Api\Test\Employers\Purchase\VerificationString2.txt", RuntimeEnvironment.GetSourceFolder());
            return File.ReadAllText(path);
        }
    }
}
