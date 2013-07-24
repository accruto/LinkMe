using System;

namespace LinkMe.Domain.Contacts
{
    // Defined in domain model. Numeric values stored in DB - do not change. The default values are defined in
    // http://intranet/wikime/moin.cgi/Requirements/RequirementsForMyVisibilityBasicSettingsPage
    [Flags]
    public enum PersonalVisibility
    {
		None = 0,
		/// <summary>
		/// See the contact's first and last name
		/// </summary>
		Name = 1,
		/// <summary>
		/// See the contact's profile photograph
		/// </summary>
		Photo = 2,
		/// <summary>
		/// See the contact's gender.
		/// </summary>
		Gender = 4,
		/// <summary>
		/// See the contact's country (Address.Country)
		/// </summary>
		Country = 8,
		/// <summary>
		/// See the contact's state/county (Address.CountrySubdivision)
		/// </summary>
		CountrySubdivision = 0x10,
		/// <summary>
		/// See the contact's suburb (Address.Suburb)
		/// </summary>
		Suburb = 0x20,
		/// <summary>
		/// See the contact's current job titles and employers.
		/// </summary>
		CurrentJobs = 0x40,
		/// <summary>
		/// View Education section of the resume.
		/// </summary>
		Education = 0x80,
		/// <summary>
		/// View Interests sections of the resume.
		/// </summary>
		Interests = 0x100,
		/// <summary>
		/// View the contact's list of friends (first degree contacts).
		/// </summary>
		FriendsList = 0x400,
		/// <summary>
		/// See all of the contact's phone numbers.
		/// </summary>
		PhoneNumbers = 0x800,
		/// <summary>
		/// See the contact's email addresses.
		/// </summary>
		EmailAddress = 0x1000,
		/// <summary>
		/// See the "contact view" of the contact's full resume. This includes education,
		/// interests and affiliation, but excludes email address and phone numbers,
		/// regardless of the privacy settings for those details.
		/// </summary>
		Resume = 0x2000,
		/// <summary>
		/// View the value of Candidate.status
		/// </summary>
		CandidateStatus = 0x4000,
		/// <summary>
		/// See the contact's desired job title (desired job types and salary are always
		/// visible)
		/// </summary>
		DesiredJob = 0x8000,
		/// <summary>
		/// Send messages, endorsement requests and introduction requests to the contact
		/// </summary>
		SendMessages = 0x10000,
		/// <summary>
		/// Send requests to the contact to add them to the sender's network (friends list).
		/// </summary>
		SendInvites = 0x80000,
		/// <summary>
		/// See the contact's age (but not date of birth).
		/// </summary>
		Age = 0x100000,
		/// <summary>
		/// View Affiliations sections of the resume.
		/// </summary>
		Affiliations = 0x200000,
		/// <summary>
		/// See the previous (latest that isn't current) job title and employer from the
		/// contact's Resume.
		/// </summary>
		PreviousJob = 0x400000,
        /// <summary>
        /// All possible flags.
        /// </summary>
        All = Name | Photo | Gender | Country | CountrySubdivision | Suburb | CurrentJobs
            | Education | Interests | FriendsList | PhoneNumbers | EmailAddress
            | Resume | CandidateStatus | DesiredJob | SendMessages
            | SendInvites | Age | Affiliations | PreviousJob,
    }
}
