using LinkMe.Domain.Criterias;
using LinkMe.Domain.Data;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Framework.Text.Synonyms;
using LinkMe.Framework.Text.Synonyms.Data;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.JobAds;
using LinkMe.Query.Members;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Communications.Campaigns;
using LinkMe.Query.Search.Communications.Campaigns.Data;
using LinkMe.Query.Search.Employers;
using LinkMe.Query.Search.Employers.Data;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Data;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Data;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Query.Search.Recruiters;
using LinkMe.Query.Search.Recruiters.Data;
using LinkMe.Query.Search.Resources.Commands;
using LinkMe.Query.Search.Resources.Data;
using Microsoft.Practices.Unity;

namespace LinkMe.Query.Search.Unity
{
    public class SearchConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ISynonymsCommand, SynonymsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ISynonymsRepository>(),
                    new ResolvedParameter<string[]>("ignored.job.titles")));

            container.RegisterType<ISynonymsRepository, SynonymsRepository>(new ContainerControlledLifetimeManager());

            // Job ads.

            container.RegisterType<IJobAdSearchEngineRepository, JobAdSearchEngineRepository>(new ContainerControlledLifetimeManager());

            container.RegisterType<IJobAdSearchEngineQuery, JobAdSearchEngineQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSearchEngineCommand, JobAdSearchEngineCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSortEngineQuery, JobAdSortEngineQuery>(new ContainerControlledLifetimeManager());

            container.RegisterType<IJobAdSearchesCommand, JobAdSearchesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSearchesQuery, JobAdSearchesQuery>(new ContainerControlledLifetimeManager());

            container.RegisterType<IExecuteJobAdSearchCommand, ExecuteJobAdSearchCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IChannelManager<IJobAdSearchService>>()));
            container.RegisterType<IExecuteJobAdSortCommand, ExecuteJobAdSortCommand>(new ContainerControlledLifetimeManager());

            container.RegisterType<IJobAdsRepository, JobAdsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IDataContextFactory>(),
                    new ResolvedParameter<ICriteriaPersister>("linkme.domain.criterias.jobads")));

            container.RegisterType<ICriteriaPersister, JobAdSearchCriteriaPersister>(
                "linkme.domain.criterias.jobads",
                new ContainerControlledLifetimeManager());

            container.RegisterType<IChannelManager<IJobAdSearchService>, WcfTcpChannelManager<IJobAdSearchService>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.search.jobads.tcpAddress"),
                    "linkme.search.jobads.tcp",
                    1000000));

            container.RegisterType<IChannelManager<IJobAdSortService>, WcfTcpChannelManager<IJobAdSortService>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.sort.jobads.tcpAddress"),
                    "linkme.sort.jobads.tcp",
                    1000000));

            // Members.

            container.RegisterType<IMemberSearchEngineRepository, MemberSearchEngineRepository>(new ContainerControlledLifetimeManager());

            container.RegisterType<IMemberSearchEngineQuery, MemberSearchEngineQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberSearchEngineCommand, MemberSearchEngineCommand>(new ContainerControlledLifetimeManager());

            container.RegisterType<IExecuteMemberSearchCommand, ExecuteMemberSearchCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUpdateMemberSearchCommand, UpdateMemberSearchCommand>(new ContainerControlledLifetimeManager());

            container.RegisterType<IMemberSearchSuggestionsQuery, MemberSearchSuggestionsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberSearchesCommand, MemberSearchesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberSearchesQuery, MemberSearchesQuery>(new ContainerControlledLifetimeManager());

            container.RegisterType<ISuggestedMembersQuery, SuggestedMembersQuery>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ISynonymsCommand>(),
                    new ResolvedParameter<string>("linkme.search.members.suggested.ignore")));

            container.RegisterType<IMembersRepository, MembersRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IDataContextFactory>(),
                    new ResolvedParameter<ICriteriaPersister>("linkme.domain.criterias.members")));
            container.RegisterType<IFilterMembersRepository, FilterMembersRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAdministrativeMemberSearchCommand, AdministrativeMemberSearchCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISearchMembersRepository, SearchMembersRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICriteriaPersister, MemberSearchCriteriaPersister>(
                "linkme.domain.criterias.members",
                new ContainerControlledLifetimeManager());
            
            container.RegisterType<IChannelManager<IMemberSearchService>, WcfTcpChannelManager<IMemberSearchService>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.search.members.tcpAddress"),
                    "linkme.search.members.tcp",
                    1000000));

            // Employers.

            container.RegisterType<IExecuteOrganisationSearchCommand, ExecuteOrganisationSearchCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRecruitersRepository, RecruitersRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExecuteEmployerSearchCommand, ExecuteEmployerSearchCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployersRepository, EmployersRepository>(new ContainerControlledLifetimeManager());
            
            // Resources.

            container.RegisterType<IResourceSearchEngineRepository, ResourceSearchEngineRepository>(new ContainerControlledLifetimeManager());

            container.RegisterType<IResourceSearchEngineQuery, ResourceSearchEngineQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResourceSearchEngineCommand, ResourceSearchEngineCommand>(new ContainerControlledLifetimeManager());

            container.RegisterType<IExecuteResourceSearchCommand, ExecuteResourceSearchCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExecuteFaqSearchCommand, ExecuteFaqSearchCommand>(new ContainerControlledLifetimeManager());

            container.RegisterType<IChannelManager<IResourceSearchService>, WcfTcpChannelManager<IResourceSearchService>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.search.resources.tcpAddress"),
                    "linkme.search.resources.tcp",
                    1000000));

            // Campaigns.

            container.RegisterType<ICampaignCriteriaCommand, CampaignCriteriaCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICampaignQueriesRepository, CampaignQueriesRepository>(new ContainerControlledLifetimeManager());

            container.RegisterType<ICriteriaPersister, CampaignCriteriaPersister>(
                "linkme.domain.criterias.campaigns",
                new ContainerControlledLifetimeManager());
        }
    }
}
