namespace LinkMe.Query.JobAds
{
    public enum JobAdSortOrder
    {
        None = 0,
        /// <summary>
        /// Sort by the search result score with the highest score first. This is the default - no change is required
        /// </summary>
        Relevance = 1,
        /// <summary>
        /// More recently created appear first.
        /// </summary>
        CreatedTime = 2,
        /// <summary>
        /// Order is FullTime, PartTime, Contract, Temp, JobShare
        /// </summary>
        JobType = 3,
        /// <summary>
        /// Shorter distacnes appear first.
        /// </summary>
        Distance = 6,
        /// <summary>
        /// Larger salaries appear first.
        /// </summary>
        Salary = 7,
        /// <summary>
        /// Flagged appear first.
        /// </summary>
        Flagged = 8
    }
}