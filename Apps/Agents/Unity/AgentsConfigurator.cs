using System;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Commands;
using LinkMe.Apps.Agents.Applications.Data;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Data;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.Queries;
using LinkMe.Apps.Agents.Domain.Credits.Handlers;
using LinkMe.Apps.Agents.Domain.Roles.Candidates.Commands;
using LinkMe.Apps.Agents.Domain.Roles.JobAds.Handlers;
using LinkMe.Apps.Agents.Domain.Roles.Orders.Handlers;
using LinkMe.Apps.Agents.Domain.Roles.Recruiters.Affiliations.Handlers;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Agents.Featured.Commands;
using LinkMe.Apps.Agents.Featured.Data;
using LinkMe.Apps.Agents.Featured.Queries;
using LinkMe.Apps.Agents.Profiles;
using LinkMe.Apps.Agents.Profiles.Commands;
using LinkMe.Apps.Agents.Profiles.Data;
using LinkMe.Apps.Agents.Profiles.Queries;
using LinkMe.Apps.Agents.Query.Search.JobAds;
using LinkMe.Apps.Agents.Query.Search.JobAdsSupplemental;
using LinkMe.Apps.Agents.Query.Search.Members;
using LinkMe.Apps.Agents.Query.Search.Resources;
using LinkMe.Apps.Agents.Reports;
using LinkMe.Apps.Agents.Reports.Commands;
using LinkMe.Apps.Agents.Reports.Data;
using LinkMe.Apps.Agents.Reports.Employers.Commands;
using LinkMe.Apps.Agents.Reports.Employers.Queries;
using LinkMe.Apps.Agents.Reports.Queries;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Data;
using LinkMe.Apps.Agents.Security.Handlers;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Subscribers;
using LinkMe.Apps.Agents.Users.Accounts.Commands;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Employers.Queries;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Users.Members.Handlers;
using LinkMe.Apps.Agents.Users.Members.Queries;
using LinkMe.Apps.Agents.Users.Sessions;
using LinkMe.Apps.Agents.Users.Sessions.Commands;
using LinkMe.Apps.Agents.Users.Sessions.Data;
using LinkMe.Apps.Agents.Users.Sessions.Queries;
using LinkMe.Apps.Agents.Workflows;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.Data;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Wcf;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Agents.Unity
{
    public class AgentsConfigurator
        : IContainerConfigurer, IContainerInstanceConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            // Affiliations.

            container.RegisterType<IAffiliationsHandler, AffiliationsHandler>(new ContainerControlledLifetimeManager());

            // Applications.

            container.RegisterType<IWebSiteQuery, WebSiteQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITinyUrlQuery, TinyUrlQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITinyUrlCommand, TinyUrlCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApplicationsRepository, ApplicationsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IDbConnectionFactory>("database.tinyurl.connection.factory"),
                    new ResolvedParameter<IWebSiteQuery>()));

            // Administrators.

            container.RegisterType<IAdministratorAccountsCommand, AdministratorAccountsCommand>(new ContainerControlledLifetimeManager());

            // Employers.

            container.RegisterType<IEmployerAccountsCommand, EmployerAccountsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerAccountsQuery, EmployerAccountsQuery>(new ContainerControlledLifetimeManager());

            // Members.

            container.RegisterType<IMemberAccountsCommand, MemberAccountsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IVisitorStatusQuery, VisitorStatusQuery>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new [] {3, 10, 25},
                    new[] {3, 10}));
            container.RegisterType<IMessagesHandler, MessagesHandler>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFriendsHandler, FriendsHandler>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMembersHandler, MembersHandler>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResumesHandler, ResumesHandler>(new ContainerControlledLifetimeManager());
            container.RegisterType<MemberSearchSubscriber>(new ContainerControlledLifetimeManager());

            // Custodians.

            container.RegisterType<ICustodianAccountsCommand, CustodianAccountsCommand>(new ContainerControlledLifetimeManager());

            // Accounts.

            container.RegisterType<IAccountVerificationsCommand, AccountVerificationsCommand>(new ContainerControlledLifetimeManager());
        
            // Candidates.

            container.RegisterType<ICandidateStatusCommand, CandidateStatusCommand>(new ContainerControlledLifetimeManager());

            // JobAds.

            container.RegisterType<IJobAdsHandler, JobAdsHandler>(new ContainerControlledLifetimeManager());

            // Reports.

            container.RegisterType<IReportsCommand, ReportsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IReportsQuery, ReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IReportsRepository, ReportsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerReportsCommand, EmployerReportsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExecuteEmployerReportsCommand, ExecuteEmployerReportsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerReportsQuery, EmployerReportsQuery>(new ContainerControlledLifetimeManager());

            // Communications.

            container.RegisterType<IMemberSearchAlertsCommand, MemberSearchAlertsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberSearchAlertsQuery, MemberSearchAlertsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSearchAlertsCommand, JobAdSearchAlertsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSearchAlertsQuery, JobAdSearchAlertsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISearchAlertsRepository, SearchAlertsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailsCommand, EmailsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ISettingsQuery>(),
                    new ResolvedParameter<ISettingsCommand>(),
                    new ResolvedParameter<ICommunicationEngine>(),
                    new ResolvedParameter<ITemplateEngine>(),
                    new ResolvedParameter<IAffiliateEmailsQuery>(),
                    new ResolvedParameter<IUserEmailsQuery>(),
                    new ResolvedParameter<string>("email.memberservices.address"),
                    new ResolvedParameter<string>("email.clientservices.address"),
                    new ResolvedParameter<string>("email.system.address"),
                    new ResolvedParameter<string>("email.return.address"),
                    new ResolvedParameter<string>("email.services.displayname"),
                    new ResolvedParameter<string>("email.allstaff.address"),
                    new ResolvedParameter<string>("email.allstaff.displayname"),
                    new ResolvedParameter<string>("email.redstarresume.address"),
                    new ResolvedParameter<string>("email.redstarresume.displayname")));
            container.RegisterType<IAffiliateEmailsQuery, AffiliateEmailsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserEmailsQuery, UserEmailsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICampaignEmailsCommand, CampaignEmailsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ICampaignsQuery>(),
                    new ResolvedParameter<ISettingsQuery>(),
                    new ResolvedParameter<ISettingsCommand>(),
                    new ResolvedParameter<IEmailsCommand>(),
                    new ResolvedParameter<string>("email.return.address"),
                    new ResolvedParameter<string>("email.services.displayname")));

            container.RegisterType<AffiliationsSubscriber>(new ContainerControlledLifetimeManager());
            container.RegisterType<JobAdsSubscriber>(new ContainerControlledLifetimeManager());
            container.RegisterType<MessagesSubscriber>(new ContainerControlledLifetimeManager());
            container.RegisterType<FriendsSubscriber>(new ContainerControlledLifetimeManager());
            container.RegisterType<SecuritySubscriber>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICreditsHandler, CreditsHandler>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrdersHandler, OrdersHandler>(new ContainerControlledLifetimeManager());
            container.RegisterType<ProductsSubscriber>(new ContainerControlledLifetimeManager());

            container.RegisterType<SuggestedJobsWorkflowSubscriber>(
                new InjectionConstructor(
                    new ResolvedParameter<IChannelManager<Workflow.PeriodicWorkflow.IService>>("linkme.workflow.suggestedJobs.proxy"),
                    new ResolvedParameter<ICandidatesQuery>(),
                    new ResolvedParameter<ISettingsQuery>(),
                    new ResolvedParameter<ISettingsCommand>()));

            // Security.

            container.RegisterType<IPasswordsCommand, PasswordsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoginCredentialsCommand, LoginCredentialsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExternalCredentialsCommand, ExternalCredentialsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoginCredentialsQuery, LoginCredentialsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExternalCredentialsQuery, ExternalCredentialsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityRepository, SecurityRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoginAuthenticationCommand, LoginAuthenticationCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ILoginCredentialsQuery>(),
                    new ResolvedParameter<IUsersQuery>(),
                    new ResolvedParameter<bool>("linkme.apps.agents.security.overridePasswordEnabled"),
                    new ResolvedParameter<string>("linkme.apps.agents.security.overridePassword"),
                    new ResolvedParameter<bool>("linkme.apps.agents.security.obfuscateEmails")));
            container.RegisterType<IExternalAuthenticationCommand, ExternalAuthenticationCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityHandler, SecurityHandler>(new ContainerControlledLifetimeManager());

            // Sessions.

            container.RegisterType<IUserSessionsCommand, UserSessionsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserSessionsQuery, UserSessionsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserSessionsRepository, UserSessionsRepository>(new ContainerControlledLifetimeManager());

            // Profiles.

            container.RegisterType<IProfilesCommand, ProfilesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProfilesQuery, ProfilesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProfilesRepository, ProfilesRepository>(new ContainerControlledLifetimeManager());

            // Featured.

            container.RegisterType<IFeaturedCommand, FeaturedCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFeaturedQuery, FeaturedQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFeaturedRepository, FeaturedRepository>(new ContainerControlledLifetimeManager());

            // Database connections.

            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(
                "database.tinyurl.connection.factory",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("database.tinyurl.connection.string")));

            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(
                "database.workflow.activationemail.connection.factory",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("database.workflow.activationemail.connection.string")));

            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(
                "database.workflow.candidatestatus.connection.factory",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("database.workflow.candidatestatus.connection.string")));

            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(
                "database.workflow.suggestedjobs.connection.factory",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("database.workflow.suggestedjobs.connection.string")));

            // Emails.

            container.RegisterType<ICommunicationEngine, EmailCommunicationEngine>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailClient, SmtpEmailClient>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<string>("linkme.communications.smtp.server")),
                new InjectionProperty("Port", new ResolvedParameter<int>("linkme.communications.smtp.port")),
                new InjectionProperty("Timeout", new ResolvedParameter<int>("linkme.communications.smtp.timeout")),
                new InjectionProperty("UserName", new ResolvedParameter<string>("linkme.communications.smtp.userName")),
                new InjectionProperty("Password", new ResolvedParameter<string>("linkme.communications.smtp.password")));
            
            container.RegisterType<IContentEngine, ContentEngine>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContentCreator, ContentCreator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContentRepository, ContentRepository>(new ContainerControlledLifetimeManager());

            container.RegisterType<IContentEngine, ContentEngine>(
                "linkme.framework.content.emails",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IContentRepository>("linkme.framework.content.emails.repository")));

            container.RegisterType<IContentRepository, XmlTemplateContentRepository>(
                "linkme.framework.content.emails.repository",
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.config.folder"),
                    "email-templates*.xml"));

            // Workflows.

            container.RegisterType<IChannelManager<Workflow.ActivationEmailWorkflow.IService>, WcfMsmqChannelManager<Workflow.ActivationEmailWorkflow.IService>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.workflow.activationEmail.msmqAddress"),
                    "linkme.workflow.activationEmail.msmq"));

            container.RegisterType<IChannelManager<Workflow.CandidateStatusWorkflow.IService>, WcfMsmqChannelManager<Workflow.CandidateStatusWorkflow.IService>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.workflow.candidateStatus.msmqAddress"),
                    "linkme.workflow.candidateStatus.msmq"));

            container.RegisterType<IChannelManager<Workflow.PeriodicWorkflow.IService>, WcfMsmqChannelManager<Workflow.PeriodicWorkflow.IService>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.workflow.suggestedJobs.msmqAddress"),
                    "linkme.workflow.suggestedJobs.msmq"));

            container.RegisterType<IChannelManager<Workflow.PeriodicWorkflow.IService>, WcfMsmqChannelManager<Workflow.PeriodicWorkflow.IService>>(
                "linkme.workflow.suggestedJobs.proxy",
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.workflow.suggestedJobs.msmqAddress"),
                    "linkme.workflow.suggestedJobs.msmq"));
        }

        void IContainerInstanceConfigurer.RegisterInstances(IUnityContainer container)
        {
            container.RegisterInstance(container.Resolve<CandidateWorkflowSubscriber>());
            container.RegisterInstance(container.Resolve<SuggestedJobsWorkflowSubscriber>());
            container.RegisterInstance(container.Resolve<AffiliationsSubscriber>());
            container.RegisterInstance(container.Resolve<JobAdsSubscriber>());
            container.RegisterInstance(container.Resolve<MessagesSubscriber>());
            container.RegisterInstance(container.Resolve<FriendsSubscriber>());
            container.RegisterInstance(container.Resolve<SecuritySubscriber>());
            container.RegisterInstance(container.Resolve<ProductsSubscriber>());
            container.RegisterInstance(container.Resolve<MembersSubscriber>());
            container.RegisterInstance(container.Resolve<ResumeEventsSubscriber>());
            container.RegisterInstance(container.Resolve<MemberSearchSubscriber>());
            container.RegisterInstance(container.Resolve<JobAdSearchSubscriber>());
            container.RegisterInstance(container.Resolve<JobAdSortSubscriber>());
            container.RegisterInstance(container.Resolve<ResourceSearchSubscriber>());
            container.RegisterInstance(container.Resolve<PartnersEmailSubscriber>());
        }
    }
}
