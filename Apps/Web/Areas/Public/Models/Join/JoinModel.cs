using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Web.Areas.Public.Models.Logins;

namespace LinkMe.Web.Areas.Public.Models.Join
{
    public class JoinModel
        : PageflowModel
    {
        public bool HasInitialMember { get; set; }
        public bool HasMember { get; set; }
        public bool IsUploadingResume { get; set; }
        public LoginReason? Reason { get; set; }
    }
}
