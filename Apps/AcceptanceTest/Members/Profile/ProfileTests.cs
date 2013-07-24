using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile
{
    [TestClass]
    public abstract class ProfileTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();

        protected ReadOnlyUrl _profileUrl;

        [TestInitialize]
        public void ProfileTestInitialize()
        {
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
        }

        protected virtual Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }
    }
}
