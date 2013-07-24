using LinkMe.Apps.Agents.Applications.Commands;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Asp.Referrals;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Asp.Templates;
using LinkMe.Apps.Pageflows;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Asp.Unity
{
    public class AspConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ITemplateEngine, CombinedTemplateEngine>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ITemplateEngine>("linkme.framework.content.templates.website"),
                    new ResolvedParameter<ITemplateEngine>("linkme.framework.content.templates.management"),
                    new[] { "MemberNewsletterEmail", "EmployerNewsletterEmail", "IosLaunchEmail", "EdmEmail", "ReengagementEmail" }));

            container.RegisterType<ITemplateEngine, WebSiteTemplateEngine>(
                "linkme.framework.content.templates.website",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IContentEngine>("linkme.framework.content.emails"),
                    new ResolvedParameter<IWebSiteQuery>(),
                    new ResolvedParameter<ITinyUrlCommand>()));

            container.RegisterType<ITemplateEngine, ManagementTemplateEngine>(
                "linkme.framework.content.templates.management",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IWebSiteQuery>(),
                    240000));

            // Referrals.

            container.RegisterType<IReferralsManager, ReferralsManager>(new ContainerControlledLifetimeManager());

            // Security.
            
            container.RegisterType<IAuthenticationManager, AuthenticationManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAccountsManager, AccountsManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICookieManager, CookieManager>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new[] { new[] { "linkme.com.au", "linkme.net.au" } }));

            container.RegisterType<IDevAuthenticationManager, DevAuthenticationManager>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<bool>("dev.always.logged.in"),
                    new ResolvedParameter<string>("dev.password")));

            // Pageflow.
            
            container.RegisterType<IPageflowEngine, PageflowEngine>(new ContainerControlledLifetimeManager());
        }
    }
}
