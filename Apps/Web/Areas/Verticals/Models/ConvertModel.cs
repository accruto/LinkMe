using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Verticals.Models
{
    public abstract class ConvertCommunityModel
    {
        public Community Community { get; set; }
    }

    [Passwords]
    public class ConvertModel
        : ConvertCommunityModel, IHavePasswords
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobCompany { get; set; }
        [Required]
        public string NewEmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }

    public class ConvertedModel
        : ConvertCommunityModel
    {
    }
}
