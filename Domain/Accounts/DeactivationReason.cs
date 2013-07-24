namespace LinkMe.Domain.Accounts
{
    public enum DeactivationReason
    {
        Other,
        /// <summary>
        /// I don't want my current employer to know I'm looking for work
        /// </summary>
        Employer,
        /// <summary>
        /// I'm not looking for work anymore
        /// </summary>
        NotLooking,
        /// <summary>
        /// I receive too many emails
        /// </summary>
        Emails,
        /// <summary>
        /// I receive too many job alert emails
        /// </summary>
        JobAlerts,
        /// <summary>
        /// I don't find the website useful
        /// </summary>
        NotUseful,
        /// <summary>
        /// I couldn't find the job I was looking for
        /// </summary>
        NoJobFound,
        /// <summary>
        /// I couldn't contact people to build my network
        /// </summary>
        NoContactsFound,
        /// <summary>
        /// I am going to join using different login details
        /// </summary>
        DifferentLogin
    }
}