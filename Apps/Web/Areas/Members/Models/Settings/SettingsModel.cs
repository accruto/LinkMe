using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Members.Models.Settings
{
    public class SettingsModel
    {
        public bool CanEditContactDetails { get; set; }

        [Required, FirstName, Trim]
        public string FirstName { get; set; }
        [Required, LastName, Trim]
        public string LastName { get; set; }
        [Required, EmailAddress(true), Trim]
        public string EmailAddress { get; set; }
        [EmailAddress(true), Trim]
        public string SecondaryEmailAddress { get; set; }

        public IList<Tuple<Category, Frequency?>> PeriodicCategories { get; set; }
        public IList<Tuple<Category, Frequency?>> NotificationCategories { get; set; }
    }
}