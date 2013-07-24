using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Domain.Users.Members.Affiliations.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Affiliations
{
    [TestClass]
    public abstract class AffiliationTests
        : TestClass
    {
        protected IMemberAffiliationsCommand _memberAffiliationsCommand = Resolve<IMemberAffiliationsCommand>();
        protected IMemberAffiliationsQuery _memberAffiliationsQuery = Resolve<IMemberAffiliationsQuery>();

        [TestInitialize]
        public void AffiliationTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }
    }
}