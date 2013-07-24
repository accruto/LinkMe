using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Members.Controllers
{
    public abstract class MembersApiController
        : ApiController
    {
        private MemberContext _context;

        protected MemberContext MemberContext
        {
            get { return _context ?? (_context = new MemberContext(HttpContext)); }
        }
    }
}
