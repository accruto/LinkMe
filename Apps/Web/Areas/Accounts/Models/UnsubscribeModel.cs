using System;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Accounts.Models
{
    public class UnsubscribeRequestModel
    {
        [Required]
        public Guid? UserId { get; set; }
        [Required]
        public string Category { get; set; }
    }

    public class UnsubscribeModel
    {
        public Category Category { get; set; }
        public Login Login { get; set; }
        public bool HasUnsubscribed { get; set; }
    }
}
