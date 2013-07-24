namespace LinkMe.Apps.Presentation.Domain.Users.Members.Search
{
    public static class MemberSearchContext
    {
        // Suggested Candidates shown on the website
        public const string SuggestedCandidatesPage = "SuggestedCandidates_2.3";
        // Suggested Candidates emailed by EmailSuggestedCandidatesTask
        public const string SuggestedCandidatesEmail = "SuggestedCandidatesEmail_2.9";
        // A search that resulted from the user clicking the "More suggested candidates" link in the above
        // email OR clicking on one of the Full Candidate View link in the email (which internally also
        // performs the search so it can show previous/next links.
        public const string SuggestedCandidatesMoreFromEmail = "MoreSuggestedCandidates_2.9";
        public const string SuggestedCandidatesFcvFromEmail = "FCVSuggestedCandidates_2.9";
        public const string SimpleSearchPage = "SimpleSearch_2.3";
        public const string AdvancedSearchPage = "AdvancedSearch_2.3";
        public const string FolderSearch = "CandidateFolder_8.8"; // This shouldn't be saved to the DB.
        public const string SavedSearch = "SavedSearch";
    }
}