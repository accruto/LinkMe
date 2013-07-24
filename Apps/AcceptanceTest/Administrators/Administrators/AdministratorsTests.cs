using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Administrators
{
    [TestClass]
    public abstract class AdministratorsTests
        : WebTestClass
    {
        protected static IAdministratorAccountsCommand _administratorAccountsCommand
        {
            get { return Resolve<IAdministratorAccountsCommand>(); }
        }

        private ReadOnlyUrl _administratorsUrl;

        [TestInitialize]
        public void AdministratorsTestsInitialize()
        {
            _administratorsUrl = new ReadOnlyApplicationUrl(true, "~/administrators/administrators");
        }

        protected Administrator CreateAdministrator(int index)
        {
            return _administratorAccountsCommand.CreateTestAdministrator(index);
        }

        protected ReadOnlyUrl GetAdministratorsUrl()
        {
            return _administratorsUrl;
        }

        protected ReadOnlyUrl GetAdministratorUrl(Administrator administrator)
        {
            var url = (_administratorsUrl.AbsoluteUri + "/")
                .AddUrlSegments(administrator.Id.ToString());
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetNewAdministratorUrl()
        {
            var url = (_administratorsUrl.AbsoluteUri + "/")
                .AddUrlSegments("new");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }
    }
}
