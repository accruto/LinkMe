using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    [TestClass]
    public abstract class OrganisationsTests
        : TestClass
    {
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IOrganisationsQuery _organisationsQuery = Resolve<IOrganisationsQuery>();

        [TestInitialize]
        public void OrganisationsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }
    }
}