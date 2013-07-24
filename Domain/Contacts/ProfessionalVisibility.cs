using System;

namespace LinkMe.Domain.Contacts
{
    // Defined in domain model. Numeric values stored in DB - do not change. The default values are defined in
    // http://intranet/wikime/moin.cgi/Requirements/RequirementsForMyVisibilityBasicSettingsPage
    [Flags]
	public enum ProfessionalVisibility
    {
		None = 0,
		/// <summary>
		/// See the member's resume without identifying details.
		/// </summary>
		Resume = 1,
		/// <summary>
		/// See the member's first and last name. Only available if Resume is enabled.
		/// </summary>
		Name = 2,
		/// <summary>
		/// See all of the member's phone numbers. Only available if Resume is enabled.
		/// </summary>
		PhoneNumbers = 4,
		/// <summary>
		/// See the member's profile photograph. Only available if Resume is enabled.
		/// </summary>
		ProfilePhoto = 8,
		/// <summary>
		/// See the member's current and previous employers. Only available if Resume is
		/// enabled.
		/// </summary>
		RecentEmployers = 0x10,
        /// <summary>
        /// See the communities that the member belongs to. Only available if Resume is enabled.
        /// </summary>
        Communities = 0x20,
        /// <summary>
        /// See the Referees section of the member's resume. This currently cannot be changed by the
        /// Member, but depends on whether or not the Employer has paid to see the resume.
        /// </summary>
        Referees = 0x40,
        Salary = 0x80,
        /// <summary>
        /// All possible ProfessionalVisibility flags.
        /// </summary>
        All = Resume | Name | PhoneNumbers | ProfilePhoto | RecentEmployers | Communities | Referees | Salary,
	}
}
