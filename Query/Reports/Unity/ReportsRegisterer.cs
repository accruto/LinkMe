using System;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Reports.Accounts;
using LinkMe.Query.Reports.Accounts.Data;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Reports.Credits;
using LinkMe.Query.Reports.Credits.Data;
using LinkMe.Query.Reports.Credits.Queries;
using LinkMe.Query.Reports.DailyReports.Queries;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Commands;
using LinkMe.Query.Reports.Roles.Candidates.Data;
using LinkMe.Query.Reports.Roles.Candidates.Queries;
using LinkMe.Query.Reports.Roles.Communications;
using LinkMe.Query.Reports.Roles.Communications.Data;
using LinkMe.Query.Reports.Roles.Communications.Queries;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Commands;
using LinkMe.Query.Reports.Roles.Integration.Data;
using LinkMe.Query.Reports.Roles.Integration.Queries;
using LinkMe.Query.Reports.Roles.JobAds;
using LinkMe.Query.Reports.Roles.JobAds.Data;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using LinkMe.Query.Reports.Roles.Networking;
using LinkMe.Query.Reports.Roles.Networking.Data;
using LinkMe.Query.Reports.Roles.Networking.Queries;
using LinkMe.Query.Reports.Roles.Orders;
using LinkMe.Query.Reports.Roles.Orders.Data;
using LinkMe.Query.Reports.Roles.Orders.Queries;
using LinkMe.Query.Reports.Roles.Registration;
using LinkMe.Query.Reports.Roles.Registration.Data;
using LinkMe.Query.Reports.Roles.Registration.Queries;
using LinkMe.Query.Reports.Search;
using LinkMe.Query.Reports.Search.Data;
using LinkMe.Query.Reports.Search.Queries;
using LinkMe.Query.Reports.Users.Employers;
using LinkMe.Query.Reports.Users.Employers.Data;
using LinkMe.Query.Reports.Users.Employers.Queries;
using Microsoft.Practices.Unity;

namespace LinkMe.Query.Reports.Unity
{
    public class ReportsConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDataContextFactory, DataContextFactory>(
                "reports.datacontext.factory",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IDbConnectionFactory>(),
                    new TimeSpan(0, 20, 0)));

            container.RegisterType<IDataContextFactory, DataContextFactory>(
                "reports.tracking.datacontext.factory",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IDbConnectionFactory>("database.tracking.connection.factory"),
                    new TimeSpan(0, 20, 0)));

            container.RegisterType<ICreditReportsQuery, CreditReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICreditReportsRepository, CreditReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IJobAdReportsQuery, JobAdReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAccountReportsQuery, AccountReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAccountReportsRepository, AccountReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IResumeReportsCommand, ResumeReportsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResumeReportsQuery, ResumeReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateReportsQuery, CandidateReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICandidateReportsRepository, CandidateReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IJobAdSearchReportsQuery, JobAdSearchReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSearchReportsRepository, JobAdSearchReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IMemberSearchReportsQuery, MemberSearchReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerMemberAccessReportsQuery, EmployerMemberAccessReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployerMemberAccessReportsRepository, EmployerMemberAccessReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<INetworkingReportsQuery, NetworkingReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<INetworkingReportsRepository, NetworkingReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IOrderReportsQuery, OrderReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOrderReportsRepository, OrderReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IDailyReportsQuery, DailyReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRegistrationReportsQuery, RegistrationReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRegistrationReportsRepository, RegistrationReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<ICommunicationReportsQuery, CommunicationReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdReportsRepository, JobAdReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IMemberSearchReportsRepository, MemberSearchReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IIntegrationReportsRepository, IntegrationReportsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.datacontext.factory")));
            container.RegisterType<IJobAdIntegrationReportsCommand, JobAdIntegrationReportsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdIntegrationReportsQuery, JobAdIntegrationReportsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommunicationsRepository, CommunicationsRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDataContextFactory>("reports.tracking.datacontext.factory")));
        }
    }
}
