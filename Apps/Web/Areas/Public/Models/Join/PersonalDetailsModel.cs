using System.Collections.Generic;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Public.Models.Join
{
    public class PersonalDetailsMemberModel
    {
        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }
        [Required, EmailAddress(true)]
        public string EmailAddress { get; set; }
        public int CountryId { get; set; }
        [Required]
        public string Location { get; set; }
        [Required, PhoneNumber]
        public string PhoneNumber { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }
        [Required]
        public CandidateStatus? Status { get; set; }
        [Required]
        public decimal? SalaryLowerBound { get; set; }
        public SalaryRate SalaryRate { get; set; }
        public ProfessionalVisibility Visibility { get; set; }
    }

    [Passwords]
    public class PersonalDetailsPasswordsModel
        : IHavePasswords
    {
        [Required, Password]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public bool AcceptTerms { get; set; }
    }

    public class PersonalDetailsReferenceModel
    {
        public IList<Country> Countries { get; set; }
        public Country DefaultCountry { get; set; }
        public IList<LocationReference> RelocationList { get; set; }
        public IList<SalaryRate> SalaryRates { get; set; }
    }

    public class PersonalDetailsModel
        : JoinModel
    {
        public PersonalDetailsMemberModel Member { get; set; }
        public PersonalDetailsPasswordsModel Passwords { get; set; }
        public AffiliationItems AffiliationItems { get; set; }
        public PersonalDetailsReferenceModel Reference { get; set; }
    }
}