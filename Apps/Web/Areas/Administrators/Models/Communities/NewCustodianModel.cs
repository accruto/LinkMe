using LinkMe.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Web.Areas.Administrators.Models.Communities
{
    public class NewCustodianModel
    {
        public Community Community { get; set; }
        public CreateCustodianModel Custodian { get; set; }
    }
}