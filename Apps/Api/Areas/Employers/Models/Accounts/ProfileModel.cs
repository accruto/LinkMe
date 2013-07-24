using System;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Accounts
{
    public class ProfileModel
        : JsonResponseModel
    {
        public Guid Id { get; set; }
        public string LoginId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganisationName { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public int Credits { get; set; }
    }
}