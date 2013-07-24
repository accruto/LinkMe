using System;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Communities.Data;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Partners;
using LinkMe.Domain.Roles.Affiliations.Partners.Commands;
using LinkMe.Domain.Roles.Affiliations.Partners.Data;
using LinkMe.Domain.Roles.Affiliations.Partners.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Data;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Data;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Data;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Data;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Data;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.Integration.Data;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn.Data;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Data;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Networking;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Networking.Data;
using LinkMe.Domain.Roles.Networking.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Affiliations;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Data;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Affiliations.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Data;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Data;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Representatives.Data;
using LinkMe.Domain.Roles.Representatives.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Data;
using LinkMe.Domain.Roles.Resumes.Lens;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;

namespace LinkMe.Domain.Roles.Unity
{
    public class RolesConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            // Candidates.

            container.RegisterType<ICandidatesRepository, CandidatesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidatesCommand, CandidatesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidatesQuery, CandidatesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateDiariesCommand, CandidateDiariesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateDiariesQuery, CandidateDiariesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateResumesCommand, CandidateResumesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateResumeFilesCommand, CandidateResumeFilesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateResumeFilesQuery, CandidateResumeFilesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidatesWorkflowCommand, CandidatesWorkflowCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidatesWorkflowQuery, CandidatesWorkflowQuery>(new ContainerControlledLifetimeManager());

            // Resumes.

            container.RegisterType<IResumesRepository, ResumesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResumesCommand, ResumesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IParsedResumesCommand, ParsedResumesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResumesQuery, ResumesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IParseResumesCommand, ParseResumesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IParseResumeDataCommand, LensParseResumeDataCommand>(
                new TransientLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("lens.connection.host"),
                    new ResolvedParameter<int>("lens.connection.port")));
            container.RegisterType<IParseResumeXmlCommand, LensParseResumeXmlCommand>(new ContainerControlledLifetimeManager());

            // Communications.

            container.RegisterType<ISettingsRepository, SettingsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsCommand, SettingsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsQuery, SettingsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<INonMemberSettingsQuery, NonMemberSettingsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<INonMemberSettingsCommand, NonMemberSettingsCommand>(new ContainerControlledLifetimeManager());

            // Campaigns.

            container.RegisterType<ICampaignsRepository, CampaignsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IDataContextFactory>(),
                    new ResolvedParameter<ICriteriaPersister>("linkme.domain.criterias.campaigns")));
            container.RegisterType<ICampaignsCommand, CampaignsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICampaignsQuery, CampaignsQuery>(new ContainerControlledLifetimeManager());

            // Contenders.

            container.RegisterType<IApplicationsRepository, ApplicationsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApplicationsCommand, ApplicationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApplicationsQuery, ApplicationsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContendersQuery, ContendersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContenderListsRepository, ContenderListsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContenderListsCommand, ContenderListsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContenderListsQuery, ContenderListsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContenderNotesRepository, ContenderNotesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContenderNotesCommand, ContenderNotesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContenderNotesQuery, ContenderNotesQuery>(new ContainerControlledLifetimeManager());

            // Representatives.
            
            container.RegisterType<IRepresentativesRepository, RepresentativesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepresentativesCommand, RepresentativesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepresentativesQuery, RepresentativesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepresentativeInvitationsCommand, RepresentativeInvitationsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IRepresentativesRepository>(),
                    new ResolvedParameter<int>("linkme.domain.roles.networking.tempFriendDays")));
            container.RegisterType<IRepresentativeInvitationsQuery, RepresentativeInvitationsQuery>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IRepresentativesRepository>(),
                    new ResolvedParameter<int>("linkme.domain.roles.networking.tempFriendDays")));

            // Recruiters.

            container.RegisterType<IRecruitersRepository, RecruitersRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrganisationsCommand, OrganisationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrganisationsQuery, OrganisationsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRecruitersQuery, RecruitersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrganisationAffiliationsCommand, OrganisationAffiliationsCommand>(new ContainerControlledLifetimeManager());

            // Orders.

            container.RegisterType<IOrdersRepository, OrdersRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrdersCommand, OrdersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrderPricesCommand, OrderPricesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrdersQuery, OrdersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPurchaseTransactionsCommand, PurchaseTransactionsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPurchaseTransactionsQuery, PurchaseTransactionsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICouponsCommand, CouponsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICouponsQuery, CouponsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAdjustmentPersister, AdjustmentPersister>(new ContainerControlledLifetimeManager());

            // Job ads.

            container.RegisterType<IJobAdsRepository, JobAdsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdViewsRepository, JobAdViewsRepository>();
            container.RegisterType<IJobAdListsRepository, JobAdListsRepository>();
            container.RegisterType<IJobAdNotesRepository, JobAdNotesRepository>();
            container.RegisterType<IJobAdsCommand, JobAdsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IJobAdsRepository>(),
                    new ResolvedParameter<int>("linkme.domain.roles.jobads.durationDays"),
                    new ResolvedParameter<int>("linkme.domain.roles.jobads.extendedDurationDays")));
            container.RegisterType<IJobPostersCommand, JobPostersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdViewsCommand, JobAdViewsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdsQuery, JobAdsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdIntegrationQuery, JobAdIntegrationQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdViewsQuery, JobAdViewsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobPostersQuery, JobPostersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdExportCommand, JobAdExportCommand>(new ContainerControlledLifetimeManager());

            // Job ad lists.

            container.RegisterType<IJobAdNotesCommand, JobAdNotesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdListsCommand, JobAdListsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdNotesQuery, JobAdNotesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdListsQuery, JobAdListsQuery>(new ContainerControlledLifetimeManager());

            // Networking.

            container.RegisterType<INetworkingRepository, NetworkingRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<INetworkingCommand, NetworkingCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<INetworkingQuery, NetworkingQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<INetworkingInvitationsCommand, NetworkingInvitationsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<INetworkingRepository>(),
                    new ResolvedParameter<int>("linkme.domain.roles.networking.tempFriendDays")));
            container.RegisterType<INetworkingInvitationsQuery, NetworkingInvitationsQuery>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<INetworkingRepository>(),
                    new ResolvedParameter<int>("linkme.domain.roles.networking.tempFriendDays")));

            // Registration.

            container.RegisterType<IRegistrationRepository, RegistrationRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailVerificationsCommand, EmailVerificationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailVerificationsQuery, EmailVerificationsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IReferralsCommand, ReferralsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IReferralsQuery, ReferralsQuery>(new ContainerControlledLifetimeManager());

            // Affiliations.

            container.RegisterType<IVerticalsCommand, VerticalsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IVerticalsQuery, VerticalsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IVerticalsRepository, VerticalsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommunitiesCommand, CommunitiesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommunitiesQuery, CommunitiesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommunitiesRepository, CommunitiesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPartnersCommand, PartnersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPartnersQuery, PartnersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPartnersRepository, PartnersRepository>(new ContainerControlledLifetimeManager());

            // Integration.

            container.RegisterType<IIntegrationCommand, IntegrationCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IIntegrationQuery, IntegrationQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IIntegrationRepository, IntegrationRepository>(new ContainerControlledLifetimeManager());

            // LinkedIn.

            container.RegisterType<ILinkedInRepository, LinkedInRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILinkedInQuery, LinkedInQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILinkedInCommand, LinkedInCommand>(new ContainerControlledLifetimeManager());
        }
    }
}
