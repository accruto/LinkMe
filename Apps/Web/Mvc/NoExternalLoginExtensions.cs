using System;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;

namespace LinkMe.Web.Mvc
{
    public static class NoExternalLoginExtensions
    {
        public static bool CanEditContactDetails(this IVerticalsQuery verticalsQuery, Guid? verticalId)
        {
            if (verticalId != null)
            {
                var vertical = verticalsQuery.GetVertical(verticalId.Value);
                if (vertical != null && vertical.RequiresExternalLogin)
                    return false;
            }

            return true;
        }
    }
}