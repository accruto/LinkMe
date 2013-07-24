namespace LinkMe.Query.Resources
{
    public enum ResourceSortOrder
    {
        /// <summary>
        /// Do not change the order of results returned from the search engine, which are sorted by relevance.
        /// </summary>
        Relevance = 1,
        Popularity = 2,
        CreatedTime = 3,
    }
}
