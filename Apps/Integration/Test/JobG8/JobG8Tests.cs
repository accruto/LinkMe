using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8
{
    [TestClass]
    public abstract class JobG8Tests
        : TestClass
    {
        protected const string Password = "password";
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IJobG8Query _jobG8Query = Resolve<IJobG8Query>();

        [TestInitialize]
        public void JobG8TestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Employer CreateEmployer()
        {
            return CreateEmployer("jobg8-jobs");
        }

        protected Employer CreateEmployer(string loginId)
        {
            return _employerAccountsCommand.CreateTestEmployer(loginId, _organisationsCommand.CreateTestOrganisation(0));
        }
    }
}
