using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Routes;

namespace LinkMe.Web.Domain.Users.Members
{
    public static class PhotoExtensions
    {
        public static readonly ReadOnlyApplicationUrl DefaultPhotoUrl = new ReadOnlyApplicationUrl("~/ui/images/universal/photo-default.png");

        public static ReadOnlyUrl GetPhotoUrlOrNull(this IMember friend)
        {
            return friend != null && friend.PhotoId != null ? FriendsRoutes.Photo.GenerateUrl(new { friendId = friend.Id }) : null;
        }

        public static ReadOnlyUrl GetPhotoUrlOrDefault(this IMember friend)
        {
            return friend != null && friend.PhotoId != null ? FriendsRoutes.Photo.GenerateUrl(new { friendId = friend.Id }) : DefaultPhotoUrl;
        }
    }
}
