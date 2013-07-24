using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Email
{
    [TestClass]
    public abstract class EmailJobAdsTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        [TestInitialize]
        public void EmailJobAdsTestInitialize()
        {
            _emailServer.ClearEmails();

        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected static ReadOnlyUrl GetApiEmailUrl(Guid? jobAdId)
        {
            return jobAdId.HasValue
                ? new ReadOnlyApplicationUrl("~/members/jobs/api/email?jobAdId=" + jobAdId.Value)
                : new ReadOnlyApplicationUrl("~/members/jobs/api/email");
        }

        protected static ReadOnlyUrl GetEmailUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/" + jobAdId + "/email");
        }

        protected static ReadOnlyUrl GetEmailSentUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/" + jobAdId + "/emailsent");
        }
    }
}
