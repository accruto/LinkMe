using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Engine.JobAds.Search;
using LinkMe.Query.Search.Engine.JobAds.Sort;
using LinkMe.Query.Search.Engine.Members;
using LinkMe.Query.Search.Engine.Resources;
using Microsoft.Practices.Unity;
using org.apache.solr.common;

namespace LinkMe.Query.Search.Engine.Unity
{
    public class QueryEngineConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ResourceLoader, ResourceLoaderImpl>(
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.config.folder")));

            container.RegisterType<IJobAdSearchBooster, JobAdSearchBooster>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSortBooster, JobAdSortBooster>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberSearchBooster, MemberSearchBooster>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResourceSearchBooster, ResourceSearchBooster>(new ContainerControlledLifetimeManager());
        }
    }
}
