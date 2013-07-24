namespace LinkMe.Domain.Contacts
{
    public enum ProfessionalContactDegree
    {
        /// <summary>
        /// Viewer has access only to details that are available to everybody.
        /// </summary>
        NotContacted = 0,
        /// <summary>
        /// Viewer has access subject to visibility settings.
        /// </summary>
        Contacted = 1,
        /// <summary>
        /// Accessed + most visibility settings are ignored, because the candidate has explicitly made contact.
        /// </summary>
        Applicant = 2,
        /// <summary>
        /// Everything.
        /// </summary>
        Self = 3,
        /// <summary>
        /// Not much
        /// </summary>
        Public = 4,
    }
}
