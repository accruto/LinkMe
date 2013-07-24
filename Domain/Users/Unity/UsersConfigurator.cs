using System;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Donations.Commands;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Networking.Queries;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Representatives.Queries;
using LinkMe.Domain.Users.Administrators;
using LinkMe.Domain.Users.Administrators.Commands;
using LinkMe.Domain.Users.Administrators.Data;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Domain.Users.Anonymous.Data;
using LinkMe.Domain.Users.Anonymous.JobAds.Commands;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Custodians;
using LinkMe.Domain.Users.Custodians.Commands;
using LinkMe.Domain.Users.Custodians.Data;
using LinkMe.Domain.Users.Custodians.Queries;
using LinkMe.Domain.Users.Employers;
using LinkMe.Domain.Users.Employers.Applicants.Commands;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Employers.Contacts.Commands;
using LinkMe.Domain.Users.Employers.Contacts.Data;
using LinkMe.Domain.Users.Employers.Contacts.Queries;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Data;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Logos.Commands;
using LinkMe.Domain.Users.Employers.Orders.Commands;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Data;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Domain.Users.Members.Affiliations.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Communications;
using LinkMe.Domain.Users.Members.Communications.Data;
using LinkMe.Domain.Users.Members.Communications.Queries;
using LinkMe.Domain.Users.Members.Contacts;
using LinkMe.Domain.Users.Members.Contacts.Data;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Data;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Photos.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Data;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;
using AnonymousIInternalApplicationsCommand=LinkMe.Domain.Users.Anonymous.JobAds.Commands.IInternalApplicationsCommand;
using AnonymousInternalApplicationsCommand = LinkMe.Domain.Users.Anonymous.JobAds.Commands.InternalApplicationsCommand;
using IInternalApplicationsCommand = LinkMe.Domain.Users.Members.JobAds.Commands.IInternalApplicationsCommand;
using InternalApplicationsCommand = LinkMe.Domain.Users.Members.JobAds.Commands.InternalApplicationsCommand;

namespace LinkMe.Domain.Users.Unity
{
    public class UsersConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            // Administrators.

            container.RegisterType<IAdministratorsCommand, AdministratorsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAdministratorsQuery, AdministratorsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAdministratorsRepository, AdministratorsRepository>(new ContainerControlledLifetimeManager());

            // Anonymous.

            container.RegisterType<IAnonymousUsersCommand, AnonymousUsersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAnonymousUsersQuery, AnonymousUsersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAnonymousRepository, AnonymousRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<AnonymousIInternalApplicationsCommand, AnonymousInternalApplicationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAnonymousJobAdsCommand, AnonymousJobAdsCommand>(
                new ContainerControlledLifetimeManager());

            // Employers.

            container.RegisterType<IEmployersCommand, EmployersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployersQuery, EmployersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerMemberViewsCommand, EmployerMemberViewsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IEmployerViewsRepository>(),
                    new ResolvedParameter<IEmployerCreditsCommand>(),
                    300,
                    1500,
                    80,
                    300,
                    300,
                    1500));
            container.RegisterType<IEmployerMemberViewsQuery, EmployerMemberViewsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerViewsRepository, EmployerViewsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerMemberContactsCommand, EmployerMemberContactsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerMemberContactsQuery, EmployerMemberContactsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerContactsRepository, EmployerContactsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerCreditsCommand, EmployerCreditsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ICreditsQuery>(),
                    new ResolvedParameter<IAllocationsQuery>(),
                    new ResolvedParameter<IExercisedCreditsCommand>(),
                    new ResolvedParameter<IExercisedCreditsQuery>(),
                    new ResolvedParameter<IEmployersQuery>(),
                    new ResolvedParameter<IRecruitersQuery>(),
                    new ResolvedParameter<JobAdApplicantsQuery>(),
                    new ResolvedParameter<int>("linkme.domain.users.employers.credits.applicantsPerJobAd")));
            container.RegisterType<IEmployerAllocationsCommand, EmployerAllocationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerCreditsQuery, EmployerCreditsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerOrdersCommand, EmployerOrdersCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IOrdersCommand>(),
                    10));
            container.RegisterType<IEmployerOrdersQuery, EmployerOrdersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployersRepository, EmployersRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateFoldersCommand, CandidateFoldersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateFoldersQuery, CandidateFoldersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateBlockListsQuery, CandidateBlockListsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateListsCommand, CandidateListsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateFlagListsQuery, CandidateFlagListsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateNotesCommand, CandidateNotesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateNotesQuery, CandidateNotesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdApplicantsCommand, JobAdApplicantsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdApplicationSubmissionsCommand, JobAdApplicationSubmissionsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdApplicationSubmissionsQuery, JobAdApplicationSubmissionsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerJobAdsCommand, EmployerJobAdsCommand>(
                new InjectionConstructor(
                    new ResolvedParameter<IJobAdsCommand>(),
                    new ResolvedParameter<IJobAdsQuery>(),
                    new ResolvedParameter<IEmployerCreditsCommand>(),
                    50,
                    250));
            container.RegisterType<IJobAdApplicantsQuery, JobAdApplicantsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerJobAdsQuery, EmployerJobAdsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerLogosCommand, EmployerLogosCommand>(new ContainerControlledLifetimeManager());

            // Members.

            container.RegisterType<IMembersCommand, MembersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMembersQuery, MembersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberStatusQuery, MemberStatusQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberFriendsCommand, MemberFriendsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<INetworkingCommand>(),
                    new ResolvedParameter<INetworkingInvitationsCommand>(),
                    new ResolvedParameter<INetworkingInvitationsQuery>(),
                    new ResolvedParameter<IRepresentativesCommand>(),
                    new ResolvedParameter<IRepresentativeInvitationsCommand>(),
                    new ResolvedParameter<IRepresentativeInvitationsQuery>(),
                    new ResolvedParameter<IDonationsCommand>(),
                    new ResolvedParameter<int>("linkme.domain.roles.networking.invitationResendableDays"),
                    10));
            container.RegisterType<IMemberFriendsQuery, MemberFriendsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMembersRepository, MembersRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberAffiliationsCommand, MemberAffiliationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberAffiliationsQuery, MemberAffiliationsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAffiliationsProvider, FinsiaAffiliationsProvider>(
                "linkme.affiliations.finsia.provider",
                new ContainerControlledLifetimeManager());
            container.RegisterType<IAffiliationsProvider, AimeAffiliationsProvider>(
                "linkme.affiliations.aime.provider",
                new ContainerControlledLifetimeManager());
            container.RegisterType<IAffiliationsProvider, ItcraLinkAffiliationsProvider>(
                "linkme.affiliations.itcralink.provider",
                new ContainerControlledLifetimeManager());
            container.RegisterType<IAffiliationItemsFactory, AffiliationItemsFactory>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new []
                    {
                        new Guid("1AD1D2EC-2442-4360-9E10-F07512281FC9"),
                        new Guid("7088F0A9-E627-4D72-AA06-2305846EA5D1"),
                        new Guid("6F8E9378-D3C8-416D-A05F-319BA4A10EDA")
                    },
                    new ResolvedArrayParameter(
                        typeof(IAffiliationsProvider),
                        new[]
                        {
                            new ResolvedParameter<IAffiliationsProvider>("linkme.affiliations.finsia.provider"),
                            new ResolvedParameter<IAffiliationsProvider>("linkme.affiliations.aime.provider"),
                            new ResolvedParameter<IAffiliationsProvider>("linkme.affiliations.itcralink.provider")
                        })));
            container.RegisterType<IMemberContactsQuery, MemberContactsQuery>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IMemberContactsRepository>(),
                    new ResolvedParameter<int>("linkme.domain.roles.networking.tempFriendDays")));
            container.RegisterType<IMemberContactsRepository, MemberContactsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberViewsQuery, MemberViewsQuery>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IMemberViewsRepository>(),
                    new ResolvedParameter<IMembersQuery>(),
                    new ResolvedParameter<int>("linkme.domain.roles.networking.tempFriendDays")));
            container.RegisterType<IMemberViewsRepository, MemberViewsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberJobAdNotesCommand, MemberJobAdNotesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberJobAdNotesQuery, MemberJobAdNotesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberJobAdListsCommand, MemberJobAdListsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdBlockListsQuery, JobAdBlockListsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdFlagListsQuery, JobAdFlagListsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdFoldersQuery, JobAdFoldersQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdFoldersCommand, JobAdFoldersCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberPhotosCommand, MemberPhotosCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IInternalApplicationsCommand, InternalApplicationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberApplicationsQuery, MemberApplicationsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberCommunicationsQuery, MemberCommunicationsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberCommunicationsRepository, MemberCommunicationsRepository>(new ContainerControlledLifetimeManager());

            // Custodians.

            container.RegisterType<ICustodiansCommand, CustodiansCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICustodianAffiliationsCommand, CustodianAffiliationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICustodiansQuery, CustodiansQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICustodiansRepository, CustodiansRepository>(new ContainerControlledLifetimeManager());

            // Users.

            container.RegisterType<IUsersQuery, UsersQuery>(new ContainerControlledLifetimeManager());
        }
    }
}
