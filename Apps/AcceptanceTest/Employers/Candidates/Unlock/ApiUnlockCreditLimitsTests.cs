using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Unlock
{
    [TestClass]
    public class ApiUnlockCreditLimitsTests
        : ApiActionCreditLimitsTests
    {
        private ReadOnlyUrl _unlockUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _unlockUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/unlock");
        }

        protected override MemberAccessReason GetReason()
        {
            return MemberAccessReason.Unlock;
        }

        protected override JsonResponseModel CallAction(Member member)
        {
            var parameters = new NameValueCollection {{"candidateId", member.Id.ToString()}};
            return Deserialize<JsonResponseModel>(Post(_unlockUrl, parameters));
        }
    }
}