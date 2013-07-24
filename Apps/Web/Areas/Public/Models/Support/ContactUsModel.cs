using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Public.Models.Support
{
    public class EmailUsModel
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string From { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public Guid? SubcategoryId { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class ContactUsModel
    {
        public EmailUsModel EmailUs { get; set; }
        public IList<Subcategory> MemberSubCategories { get; set; }
        public IList<Subcategory> EmployerSubCategories { get; set; }
        public IList<UserType> UserTypes { get; set; }
    }
}
