using System.Linq;
using LinkMe.Query.Search.Employers;

namespace LinkMe.Apps.Presentation.Converters
{
    public class EmployerSearchCriteriaConverter
        : Converter<OrganisationEmployerSearchCriteria>
    {
        public override void Convert(OrganisationEmployerSearchCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;

            values.SetValue("Employers", criteria.Employers);
            values.SetValue("Recruiters", criteria.Recruiters);
            values.SetValue("VerifiedOrganisations", criteria.VerifiedOrganisations);
            values.SetValue("UnverifiedOrganisations", criteria.UnverifiedOrganisations);
            if (criteria.IndustryIds != null && criteria.IndustryIds.Count > 0)
                values.SetValue("IndustryIds", criteria.IndustryIds.ToArray());
            values.SetValue("MinimumLogins", criteria.MinimumLogins);
            values.SetValue("MaximumLogins", criteria.MaximumLogins);
        }

        public override OrganisationEmployerSearchCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new OrganisationEmployerSearchCriteria
                       {
                           Employers = values.GetBooleanValue("Employers") ?? true,
                           Recruiters = values.GetBooleanValue("Recruiters") ?? true,
                           VerifiedOrganisations = values.GetBooleanValue("VerifiedOrganisations") ?? true,
                           UnverifiedOrganisations = values.GetBooleanValue("UnverifiedOrganisations") ?? true,
                           IndustryIds = values.GetGuidArrayValue("IndustryIds"),
                           MinimumLogins = values.GetIntValue("MinimumLogins"),
                           MaximumLogins = values.GetIntValue("MaximumLogins")
                       };
        }
    }
}