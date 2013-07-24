using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Models.Accounts
{
    [Passwords]
    public class MemberJoin
        : Join
    {
        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }
        [Required, EmailAddress(true)]
        public string EmailAddress { get; set; }
    }
}
