using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.JobAds;
using LinkMe.Query.Members;
using Microsoft.Practices.Unity;

namespace LinkMe.Query.Unity
{
    public class QueryConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IJobAdActivityFiltersQuery, JobAdActivityFiltersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSortFiltersQuery, JobAdSortFiltersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberActivityFiltersQuery, MemberActivityFiltersQuery>(new ContainerControlledLifetimeManager());
        }
    }
}
