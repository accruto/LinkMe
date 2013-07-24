using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Public.Controllers.Support;
using LinkMe.Web.Areas.Public.Models.Support;

namespace LinkMe.Web.Areas.Public.Routes
{
    public class SupportRoutes
    {
        public static RouteReference AboutUs { get; private set; }
        public static RouteReference Careers { get; private set; }
        public static RouteReference ContactUs { get; private set; }
        public static RouteReference ContactUsPartial { get; private set; }
        public static RouteReference ApiSendContactUs { get; private set; }
        public static RouteReference Privacy { get; private set; }
        public static RouteReference Terms { get; private set; }
        public static RouteReference MemberTerms { get; private set; }
        public static RouteReference EmployerTerms { get; private set; }
        public static RouteReference PrivacyDetail { get; private set; }
        public static RouteReference TermsDetail { get; private set; }
        public static RouteReference MemberTermsDetail { get; private set; }
        public static RouteReference EmployerTermsDetail { get; private set; }
        public static RouteReference SwitchBrowser { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            AboutUs = context.MapAreaRoute<SupportController>("aboutus", c => c.AboutUs);
            Careers = context.MapAreaRoute<SupportController>("careers", c => c.Careers);
            ContactUs = context.MapAreaRoute<SupportController>("contactus", c => c.ContactUs);
            ContactUsPartial = context.MapAreaRoute<SupportController>("contactus/partial", c => c.ContactUsPartial);
            ApiSendContactUs = context.MapAreaRoute<SupportApiController, EmailUsModel>("api/contactus/send", c => c.SendContactUs);
            Privacy = context.MapAreaRoute<SupportController>("privacy", c => c.Privacy);
            Terms = context.MapAreaRoute<SupportController>("terms", c => c.Terms);
            MemberTerms = context.MapAreaRoute<SupportController>("members/terms", c => c.MemberTerms);
            EmployerTerms = context.MapAreaRoute<SupportController>("employers/terms", c => c.EmployerTerms);
            PrivacyDetail = context.MapAreaRoute<SupportController>("privacydetail", c => c.PrivacyDetail);
            TermsDetail = context.MapAreaRoute<SupportController>("termsdetail", c => c.TermsDetail);
            MemberTermsDetail = context.MapAreaRoute<SupportController>("members/termsdetail", c => c.MemberTermsDetail);
            EmployerTermsDetail = context.MapAreaRoute<SupportController>("employers/termsdetail", c => c.EmployerTermsDetail);

            SwitchBrowser = context.MapAreaRoute<BrowserSwitcherController, bool, string>("browser/switch", c => c.SwitchBrowser);

            // Old urls.

            context.MapRedirectRoute("AboutUs.aspx", AboutUs);
            context.MapRedirectRoute("ui/unregistered/common/AboutUs.aspx", AboutUs);
            context.MapRedirectRoute("CareersAtLinkMe.aspx", Careers);
            context.MapRedirectRoute("ui/unregistered/common/CareersAtLinkMe.aspx", Careers);
            context.MapRedirectRoute("PrivacyStatement.aspx", Privacy);
            context.MapRedirectRoute("ui/unregistered/common/PrivacyStatement.aspx", Privacy);
            context.MapRedirectRoute("ui/unregistered/common/privacystatementform.aspx", Privacy);
            context.MapRedirectRoute("TermsAndConditions.aspx", Terms);
            context.MapRedirectRoute("ui/unregistered/common/TermsAndConditions.aspx", Terms);
            context.MapRedirectRoute("ui/unregistered/common/termsandconditionsform.aspx", Terms);
            context.MapRedirectRoute("TermsAndConditionsDetail.aspx", TermsDetail);
            context.MapRedirectRoute("ui/unregistered/common/ContactUsForm.aspx", ContactUs);
            context.MapRedirectRoute("Partners.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("ui/unregistered/common/Partners.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("Affiliates.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("ui/unregistered/common/Affiliates.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("media/{*catchall}", HomeRoutes.Home);
            context.MapRedirectRoute("featured/{*catchall}", HomeRoutes.Home);
            context.MapRedirectRoute("marketing/{*catchall}", HomeRoutes.Home);
            context.MapRedirectRoute("ui/registered/networkers/assessmeform.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("ui/registered/Home.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("guests/Messages.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("guests/ReferEmployer.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("ui/FileNotFoundError.aspx", HomeRoutes.Home);
            context.MapRedirectRoute("ui/ServerError.aspx", HomeRoutes.Home);

            // Tiny urls.

            context.MapAreaRoute<TinyUrlsController, Guid>("url/{id}.aspx", c => c.Track);
            context.MapAreaRoute<TinyUrlsController, Guid>("url/{id}", c => c.TinyUrl);
        }
    }
}
