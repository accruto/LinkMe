using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Query.Search.Resources;
using LinkMe.Web.Areas.Members.Controllers.Resources;
using LinkMe.Web.Areas.Members.Models.Resources;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class ResourcesRoutes
    {
        public static RouteReference Resources { get; private set; }
        public static RouteReference PartialCurrent { get; private set; }
        
        public static RouteReference Articles { get; private set; }
        public static RouteReference Videos { get; private set; }
        public static RouteReference QnAs { get; private set; }

        public static RouteReference CategoryArticles { get; private set; }
        public static RouteReference CategoryVideos { get; private set; }
        public static RouteReference CategoryQnAs { get; private set; }

        public static RouteReference PartialArticles { get; private set; }
        public static RouteReference PartialVideos { get; private set; }
        public static RouteReference PartialQnAs { get; private set; }

        public static RouteReference Search { get; private set; }

        public static RouteReference Article { get; private set; }
        public static RouteReference Video { get; private set; }
        public static RouteReference QnA { get; private set; }

        public static RouteReference RecentArticles { get; private set; }
        public static RouteReference RecentVideos { get; private set; }
        public static RouteReference RecentQnAs { get; private set; }

        public static RouteReference PartialArticle { get; private set; }
        public static RouteReference PartialVideo { get; private set; }
        public static RouteReference PartialQnA { get; private set; }

        public static RouteReference PartialRecentArticles { get; private set; }
        public static RouteReference PartialRecentVideos { get; private set; }
        public static RouteReference PartialRecentQnAs { get; private set; }

        public static RouteReference ApiRateArticle { get; private set; }
        public static RouteReference ApiViewArticle { get; private set; }
        public static RouteReference ApiViewVideo { get; private set; }
        public static RouteReference ApiViewQnA { get; private set; }
        public static RouteReference ApiVote { get; private set; }
        public static RouteReference ApiAskQuestion { get; private set; }
        public static RouteReference ApiVideo { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Resources = context.MapAreaRoute<ResourcesController>("members/resources", c => c.Resources);
            PartialCurrent = context.MapAreaRoute<ResourcesController>("members/resources/current/partial", c => c.PartialCurrent);

            Articles = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/articles", c => c.Articles);
            Videos = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/videos", c => c.Videos);
            QnAs = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/qnas", c => c.QnAs);

            PartialArticles = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/articles/partial", c => c.PartialArticles);
            PartialVideos = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/videos/partial", c => c.PartialVideos);
            PartialQnAs = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/qnas/partial", c => c.PartialQnAs);

            context.MapRedirectRoute("members/resources/articles/all", Articles);
            context.MapRedirectRoute("members/resources/videos/all", Videos);
            context.MapRedirectRoute("members/resources/answeredquestions/all", QnAs);

            RecentArticles = context.MapAreaRoute<ResourcesController>("members/resources/articles/recent", c => c.RecentArticles);
            RecentVideos = context.MapAreaRoute<ResourcesController>("members/resources/videos/recent", c => c.RecentVideos);
            RecentQnAs = context.MapAreaRoute<ResourcesController>("members/resources/qnas/recent", c => c.RecentQnAs);

            PartialRecentArticles = context.MapAreaRoute<ResourcesController, ResourcesPresentationModel>("members/resources/articles/recent/partial", c => c.PartialRecentArticles);
            PartialRecentVideos = context.MapAreaRoute<ResourcesController, ResourcesPresentationModel>("members/resources/videos/recent/partial", c => c.PartialRecentVideos);
            PartialRecentQnAs = context.MapAreaRoute<ResourcesController, ResourcesPresentationModel>("members/resources/qnas/recent/partial", c => c.PartialRecentQnAs);

            context.MapRedirectRoute("members/resources/recentlyviewed/article", RecentArticles);
            context.MapRedirectRoute("members/resources/recentlyviewed/video", RecentVideos);
            context.MapRedirectRoute("members/resources/recentlyviewed/answeredquestion", RecentQnAs);

            CategoryArticles = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/articles/{category}", c => c.CategoryArticles);
            CategoryVideos = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/videos/{category}", c => c.CategoryVideos);
            CategoryQnAs = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria, ResourcesPresentationModel>("members/resources/qnas/{category}", c => c.CategoryQnAs);

            context.MapRedirectRoute("members/resources/answeredquestions/{category}", CategoryQnAs, new { category = new RedirectRouteValue(), categoryId = new RedirectQueryString(false) });
            context.MapRedirectRoute("members/resources/answeredquestions/{category}", CategoryQnAs, new { category = new RedirectRouteValue(), subcategoryId = new RedirectQueryString(false) });

            Search = context.MapAreaRoute<ResourcesController, ResourceSearchCriteria>("members/resources/search", c => c.Search);

            Article = context.MapAreaRoute<ResourcesController, Guid>("members/resources/articles/{category}/{id}", c => c.Article);
            Video = context.MapAreaRoute<ResourcesController, Guid>("members/resources/videos/{category}/{id}", c => c.Video);
            QnA = context.MapAreaRoute<ResourcesController, Guid>("members/resources/qnas/{category}/{id}", c => c.QnA);

            context.MapRedirectRoute("members/resources/article/{category}/{id}", Article, new { category = new RedirectRouteValue(), id = new RedirectRouteValue() });
            context.MapRedirectRoute("members/resources/video/{category}/{id}", Video, new { category = new RedirectRouteValue(), id = new RedirectRouteValue() });
            context.MapRedirectRoute("members/resources/answeredquestion/{category}/{id}", QnA, new { category = new RedirectRouteValue(), id = new RedirectRouteValue() });

            PartialArticle = context.MapAreaRoute<ResourcesController, Guid>("members/resources/articles/{id}/partial", c => c.PartialArticle);
            PartialVideo = context.MapAreaRoute<ResourcesController, Guid>("members/resources/videos/{id}/partial", c => c.PartialVideo);
            PartialQnA = context.MapAreaRoute<ResourcesController, Guid>("members/resources/qnas/{id}/partial", c => c.PartialQnA);

            ApiRateArticle = context.MapAreaRoute<ResourcesApiController, Guid, byte>("members/resources/articles/api/rate/{id}", c => c.RateArticle);
            ApiViewArticle = context.MapAreaRoute<ResourcesApiController, Guid>("members/resources/articles/api/view/{id}", c => c.ViewArticle);
            ApiViewVideo = context.MapAreaRoute<ResourcesApiController, Guid>("members/resources/videos/api/view/{id}", c => c.ViewVideo);
            ApiViewQnA = context.MapAreaRoute<ResourcesApiController, Guid>("members/resources/qnas/api/view/{id}", c => c.ViewAnsweredQuestion);

            ApiVote = context.MapAreaRoute<ResourcesApiController, byte>("members/resources/api/vote/{name}/{voteId}", c => c.Vote);
            ApiAskQuestion = context.MapAreaRoute<ResourcesApiController, Guid?, string>("members/resources/api/askquestion", c => c.AskQuestion);
            ApiVideo = context.MapAreaRoute<ResourcesApiController, string>("members/resources/api/videodetail", c => c.YouTubeVideoDetail);

            // Old urls.

            context.MapRedirectRoute("members/resources/current", Resources);
            context.MapRedirectRoute("ui/unregistered/ResumeStyleGuide.aspx", Resources);
            context.MapRedirectRoute("ui/unregistered/ResumeTips.aspx", Resources);
            context.MapRedirectRoute("ui/unregistered/video/Videos.aspx", Resources);
            context.MapRedirectRoute("career/{*catchall}", Resources);
            context.MapRedirectRoute("videos/{*catchall}", Resources);
        }
    }
}