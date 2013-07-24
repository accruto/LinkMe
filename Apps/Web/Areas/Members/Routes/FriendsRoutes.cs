using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Members.Controllers.Friends;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class FriendsRoutes
    {
        public static RouteReference Photo { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Photo = context.MapAreaRoute<FriendsFilesController, Guid>("members/friends/{friendId}/photo", c => c.Photo);
        }
    }
}
