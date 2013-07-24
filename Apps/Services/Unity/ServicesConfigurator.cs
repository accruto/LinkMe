using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Users.LinkedIn.Commands;
using LinkMe.Apps.Services.External.Apple.AppStore;
using LinkMe.Apps.Services.External.Apple.Notifications;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.External.Commands;
using LinkMe.Apps.Services.External.Dewr.Queries;
using LinkMe.Apps.Services.External.Disqus;
using LinkMe.Apps.Services.External.HrCareers.Queries;
using LinkMe.Apps.Services.External.JobG8.Commands;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Apps.Services.External.JobSearch;
using LinkMe.Apps.Services.External.JXT.Queries;
using LinkMe.Apps.Services.External.MyCareer.Queries;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Apps.Services.JobAds.Salaries;
using LinkMe.Apps.Services.Security;
using LinkMe.Domain.Devices.Apple.Commands;
using LinkMe.Domain.Devices.Apple.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Wcf;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Services.Unity
{
    public class ServicesConfigurator
        : IContainerConfigurer, IContainerInstanceConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            // JobAds.

            container.RegisterType<IExternalJobAdsCommand, ExternalJobAdsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExternalJobAdsQuery, ExternalJobAdsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdProcessingQuery, JobAdProcessingQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMemberJobAdViewsQuery, MemberJobAdViewsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobAdSalariesParserCommand, JobAdSalariesParserCommand>(new ContainerControlledLifetimeManager());

            // Security.

            container.RegisterType<IServiceAuthenticationManager, ServiceAuthenticationManager>(new ContainerControlledLifetimeManager());

            // External.

            container.RegisterType<ISendApplicationsCommand, SendApplicationsCommand>(new ContainerControlledLifetimeManager());
            
            // CareerOne.

            container.RegisterType<ICareerOneQuery, CareerOneQuery>(new ContainerControlledLifetimeManager());
            
            // MyCareer.

            container.RegisterType<IMyCareerQuery, MyCareerQuery>(new ContainerControlledLifetimeManager());
            
            // JobG8.

            container.RegisterType<ISendJobG8Command, SendJobG8Command>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJobG8Query, JobG8Query>(new ContainerControlledLifetimeManager());
            
            // JXT

            container.RegisterType<IJxtQuery, JxtQuery>(new ContainerControlledLifetimeManager());

            // Dewr

            container.RegisterType<IDewrQuery, DewrQuery>(new ContainerControlledLifetimeManager());

            // HrCareers

            container.RegisterType<IHrCareersQuery, HrCareersQuery>(new ContainerControlledLifetimeManager());

            // SecurePay.

            container.RegisterType<ISendSecurePayCommand, SendSecurePayCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IPurchaseTransactionsCommand>(),
                    true,
                    new ResolvedParameter<string>("securepay.url"),
                    new ResolvedParameter<string>("securepay.antifraud.url")));
            container.RegisterType<IPurchasesCommand, PurchasesCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ISendSecurePayCommand>(),
                    new ResolvedParameter<ILocationQuery>(),
                    true,
                    new ResolvedParameter<string>("securepay.merchantid"),
                    new ResolvedParameter<string>("securepay.password"),
                    new ResolvedParameter<string>("securepay.antifraud.merchantid"),
                    new ResolvedParameter<string>("securepay.antifraud.password"),
                    "Australia"));
            
            // Apple.
            
            container.RegisterType<ISendAppleCommand, SendAppleCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("apple.url")));
            container.RegisterType<IPushNotificationsCommand, PushNotificationsCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("apple.p12FileName"),
                    new ResolvedParameter<string>("apple.p12Password"),
                    new ResolvedParameter<bool>("apple.sandbox"),
                    new ResolvedParameter<IAppleDevicesQuery>(),
                    new ResolvedParameter<IMemberSearchAlertsQuery>(),
                    new ResolvedParameter<IMemberSearchAlertsCommand>(),
                    new ResolvedParameter<IMembersQuery>(),
                    new ResolvedParameter<ICandidatesQuery>(),
                    new ResolvedParameter<IResumesQuery>()));
            container.RegisterType<IPushDevicesFeedbackCommand, PushDevicesFeedbackCommand>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("apple.p12FileName"),
                    new ResolvedParameter<string>("apple.p12Password"),
                    new ResolvedParameter<bool>("apple.sandbox"),
                    new ResolvedParameter<IAppleDevicesQuery>(),
                    new ResolvedParameter<IAppleDevicesCommand>()));
            
            // Disqus.
            
            container.RegisterType<IDisqusQuery, DisqusQuery>(new ContainerControlledLifetimeManager());

            // Job ad export.

            container.RegisterType<IChannelManager<IJobAdExporter>, WcfMsmqChannelManager<IJobAdExporter>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.jobexport.jobsearch.msmqAddress"),
                    "linkme.jobexport.jobsearch.msmq"));

            // LinkedIn.

            container.RegisterType<ILinkedInAuthenticationCommand, LinkedInAuthenticationCommand>(new ContainerControlledLifetimeManager());
        }

        void IContainerInstanceConfigurer.RegisterInstances(IUnityContainer container)
        {
            container.RegisterInstance(container.Resolve<JobAdSubscriber>());
        }
    }
}
