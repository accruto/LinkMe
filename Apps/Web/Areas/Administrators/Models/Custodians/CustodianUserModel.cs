using System.ComponentModel.DataAnnotations;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Web.Areas.Administrators.Models.Custodians
{
    public class CustodianLoginModel
    {
        public string LoginId { get; set; }
        [Required, Password]
        public string Password { get; set; }
    }

    public class CustodianUserModel
        : UserModel<ICustodian, CustodianLoginModel>
    {
        public Community Community { get; set; }
    }
}