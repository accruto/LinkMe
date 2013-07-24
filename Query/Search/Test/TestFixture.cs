using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace LinkMe.Query.Search.Test
{
    public abstract class TestFixture
    {
        private static bool _initialised;

        protected static T Resolve<T>()
        {
            if (!_initialised)
            {
                InitialiseContainer();
                _initialised = true;
            }
            return Container.Current.Resolve<T>();
        }

        private static void InitialiseContainer()
        {
            new ContainerConfigurer()
                .Add(new DomainConfigurator())
                .Add(new RolesConfigurator())
                .Add(new UsersConfigurator())
                .Add(new QueryConfigurator())
                .Add(new SearchConfigurator())
                .Add(new QueryEngineConfigurator())
                .RegisterType<IJobAdProcessingQuery, MockJobAdProcessingQuery>(new ContainerControlledLifetimeManager())
                .Add("linkme.resources.container")
                .Configure(Container.Current);
        }

        [TestFixtureSetUp]
        public virtual void FixtureSetUp()
        {
        }

        [SetUp]
        public virtual void SetUp()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }
    }
}