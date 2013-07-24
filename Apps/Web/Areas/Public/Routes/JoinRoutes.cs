using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Public.Controllers.Join;
using LinkMe.Web.Areas.Public.Models.Logins;
using MemberJoin = LinkMe.Web.Models.Accounts.MemberJoin;

namespace LinkMe.Web.Areas.Public.Routes
{
    public static class JoinRoutes
    {
        public static RouteReference Join { get; private set; }
        public static RouteReference PersonalDetails { get; private set; }
        public static RouteReference JobDetails { get; private set; }
        public static RouteReference Activate { get; private set; }
        public static RouteReference ApiJoin { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Join = context.MapAreaRoute<JoinController, Guid?, LoginReason?>("join", c => c.Join);
            PersonalDetails = context.MapAreaRoute<JoinController>("join/personaldetails", c => c.PersonalDetails);
            JobDetails = context.MapAreaRoute<JoinController>("join/jobdetails", c => c.JobDetails);
            Activate = context.MapAreaRoute<JoinController>("join/activate", c => c.Activate);

            ApiJoin  = context.MapAreaRoute<JoinApiController, MemberJoin, bool>("join/api", c => c.Join);

            // Old urls.

            context.MapRedirectRoute("ui/unregistered/NewNetworkerUserProfileForm.aspx", Join);
            context.MapRedirectRoute("Join.aspx", Join);
            context.MapRedirectRoute("ui/NewNetworkerUserProfileForm.aspx", Join);
            context.MapRedirectRoute("join/default.aspx", Join);
            context.MapRedirectRoute("ui/unregistered/newnetworkerjoinform.aspx", Join);
            context.MapRedirectRoute("ui/unregistered/networkerjoinform.aspx", Join);
            context.MapRedirectRoute("ui/unregistered/QuickJoin.aspx", Join);
            context.MapRedirectRoute("ui/unregistered/JoinForm.aspx", Join);
        }
    }
}