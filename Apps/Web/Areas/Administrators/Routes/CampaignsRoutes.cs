using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Web.Areas.Administrators.Controllers.Campaigns;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class CampaignsRoutes
    {
        public static RouteReference Index { get; private set; }
        public static RouteReference PagedIndex { get; private set; }
        public static RouteReference CategoryIndex { get; private set; }
        public static RouteReference PagedCategoryIndex { get; private set; }
        public static RouteReference New { get; private set; }
        public static RouteReference Delete { get; private set; }
        public static RouteReference Activate { get; private set; }
        public static RouteReference Stop { get; private set; }
        public static RouteReference Edit { get; private set; }
        public static RouteReference EditCriteria { get; private set; }
        public static RouteReference Search { get; private set; }
        public static RouteReference EditTemplate { get; private set; }
        public static RouteReference Preview { get; private set; }
        public static RouteReference Send { get; private set; }
        public static RouteReference Report { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Index = context.MapAreaRoute<CampaignsController>("administrators/communications/campaigns", c => c.Index);
            PagedIndex = context.MapAreaRoute<CampaignsController, int>("administrators/communications/campaigns/{page}", c => c.PagedIndex);
            New = context.MapAreaRoute<CampaignsController>("administrators/communications/campaigns/campaign/new", c => c.New);
            CategoryIndex = context.MapAreaRoute<CampaignsController, CampaignCategory>("administrators/communications/campaigns/{category}", c => c.CategoryIndex);
            PagedCategoryIndex = context.MapAreaRoute<CampaignsController, CampaignCategory, int>("administrators/communications/campaigns/{category}/{page}", c => c.PagedCategoryIndex);
            Edit = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}", c => c.Edit);
            Delete = context.MapAreaRoute<CampaignsController, Guid, int?>("administrators/communications/campaigns/campaign/{id}/delete", c => c.Delete);
            Activate = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}/activate", c => c.Activate);
            Stop = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}/stop", c => c.Stop);
            EditCriteria = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}/criteria", c => c.EditCriteria);
            EditTemplate = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}/template", c => c.EditTemplate);
            Search = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}/search", c => c.Search);
            Send = context.MapAreaRoute<CampaignsController, Guid, string, string>("administrators/communications/campaigns/campaign/{id}/send", c => c.Send);
            Preview = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}/preview", c => c.Preview);
            Report = context.MapAreaRoute<CampaignsController, Guid>("administrators/communications/campaigns/campaign/{id}/report", c => c.Report);
        }
    }
}