using LinkMe.Domain.Roles.Recruiters.Affiliations.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters.Affiliations
{
    [TestClass]
    public abstract class AffiliationTests
        : TestClass
    {
        protected IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected IOrganisationsQuery _organisationsQuery = Resolve<IOrganisationsQuery>();
        protected IOrganisationAffiliationsCommand _organisationAffiliationsCommand = Resolve<IOrganisationAffiliationsCommand>();
        
        [TestInitialize]
        public void AffiliationTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }
    }
}