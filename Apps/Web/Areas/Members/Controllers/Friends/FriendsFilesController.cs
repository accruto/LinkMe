using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.Friends
{
    [EnsureHttps, EnsureAuthorized(UserType.Member)]
    public class FriendsFilesController
        : ViewController
    {
        private readonly IMembersQuery _membersQuery;
        private readonly IFilesQuery _filesQuery;

        public FriendsFilesController(IMembersQuery membersQuery, IFilesQuery filesQuery)
        {
            _membersQuery = membersQuery;
            _filesQuery = filesQuery;
        }

        public ActionResult Photo(Guid friendId)
        {
            var friend = _membersQuery.GetMember(friendId);
            if (friend == null || friend.PhotoId == null)
                return HttpNotFound();

            var fileReference = _filesQuery.GetFileReference(friend.PhotoId.Value);
            if (fileReference == null || fileReference.FileData.FileType != FileType.ProfilePhoto)
                return HttpNotFound();

            // Return the file.

            return File(_filesQuery.OpenFile(fileReference), fileReference.MediaType);
        }
    }
}
