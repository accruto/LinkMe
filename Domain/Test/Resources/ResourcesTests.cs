using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public abstract class ResourcesTests
        : TestClass
    {
        protected readonly IResourcesCommand _resourcesCommand = Resolve<IResourcesCommand>();
        protected readonly IResourcesQuery _resourcesQuery = Resolve<IResourcesQuery>();

        [TestInitialize]
        public void ResourcesTestIntialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }
    }
}
