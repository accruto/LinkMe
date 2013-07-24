namespace LinkMe.Query.Members
{
    public enum MemberSortOrder
    {
        /// <summary>
        /// Do not change the order of results returned from the search engine, which are sorted by relevance.
        /// </summary>
        Relevance = 1,
        /// <summary>
        /// Sort by the Resume Last Updated Date with the most recently updated first.
        /// </summary>
        DateUpdated = 3,
        /// <summary>
        /// The name of the candidate, as displayed to the searcher (which may not be their real name).
        /// Currently only supported for candidate folder search.
        /// </summary>
        DisplayName = 4,
        /// <summary>
        /// The time that the candidate was added to one of the searcher's folders. Only applies to candidate
        /// folder search.
        /// </summary>
        EntryCreatedTimeDescending = 5,

        Distance = 6,
        Salary = 7,
        Flagged = 8,
        Availability = 9,
        FirstName = 10,
    }
}
