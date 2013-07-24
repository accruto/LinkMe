using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Employers.Controllers
{
    public abstract class EmployersApiController
        : ApiController
    {
        private EmployerContext _context;

        protected EmployerContext EmployerContext
        {
            get { return _context ?? (_context = new EmployerContext(HttpContext)); }
        }
    }
}
