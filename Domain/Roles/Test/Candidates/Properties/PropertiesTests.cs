using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Properties
{
    [TestClass]
    public abstract class PropertiesTests
        : TestClass
    {
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();

        [TestInitialize]
        public void PropertiesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }
    }
}