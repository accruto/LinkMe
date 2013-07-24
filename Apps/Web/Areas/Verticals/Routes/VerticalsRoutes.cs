using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Verticals.Controllers;

namespace LinkMe.Web.Areas.Verticals.Routes
{
    public static class VerticalsRoutes
    {
        public static RouteReference Converted { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<VerticalsController>("verticals/reset", c => c.Reset);

            context.MapAreaRoute<VerticalsController, string>("{verticalUrl}/accounts/convert", c => c.Convert);
            Converted = context.MapAreaRoute<VerticalsController, string>("{verticalUrl}/accounts/converted", c => c.Converted);

            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}", c => c.Home);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/join.aspx", c => c.Join);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/login.aspx", c => c.LogIn);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/employers/Employer.aspx", c => c.EmployerHome);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/employers/Join.aspx", c => c.EmployerJoin);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/employers/LogIn.aspx", c => c.EmployerLogin);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/{verticalUrl2}", c => c.Home);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/{verticalUrl2}/join.aspx", c => c.Join);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/{verticalUrl2}/login.aspx", c => c.LogIn);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/{verticalUrl2}/employers/Employer.aspx", c => c.EmployerHome);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/{verticalUrl2}/employers/Join.aspx", c => c.EmployerJoin);
            context.MapAreaRoute<VerticalsController, string, string>(false, "{verticalUrl}/{verticalUrl2}/employers/LogIn.aspx", c => c.EmployerLogin);
        }
    }
}