using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Recruiters
{
    public class OrganisationNameAttribute
        : RegexAttribute
    {
        public OrganisationNameAttribute()
            : base(RegularExpressions.CompleteOrganisationName, Constants.OrganisationNameMinLength, Constants.OrganisationNameMaxLength)
        {
        }
    }
}