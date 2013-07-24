using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Apps.Presentation.Query.Search.Resources;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Presentation.Unity
{
    public class PresentationConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            // Candidates.

            container.RegisterType<IResumeFilesQuery, ResumeFilesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerResumeFilesQuery, EmployerResumeFilesQuery>(new ContainerControlledLifetimeManager());

            // JobAds.

            container.RegisterType<IJobAdFilesQuery, JobAdFilesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberJobAdFilesQuery, MemberJobAdFilesQuery>(new ContainerControlledLifetimeManager());

            // Search.

            container.RegisterType<IResumeHighlighterFactory, ResumeHighlighterFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdHighlighterFactory, JobAdHighlighterFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResourceHighlighterFactory, ResourceHighlighterFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFaqHighlighterFactory, FaqHighlighterFactory>(new ContainerControlledLifetimeManager());
        }
    }
}
