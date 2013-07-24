using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public abstract class AuthorizedAttribute
        : FilterAttribute, IAuthorizationFilter
    {
        protected AuthorizedAttribute()
        {
            // Ensure that this filter gets executed before all other filters.

            Order = -1;
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            OnAuthorization(filterContext);
        }

        protected abstract void OnAuthorization(AuthorizationContext context);
    }
}
