using LinkMe.Domain.Users.Employers.Candidates;

namespace LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates
{
    public static class CandidateFoldersExtensions
    {
        public static string GetNameDisplayText(this CandidateFolder folder)
        {
            if (folder.FolderType == FolderType.Shortlist && string.IsNullOrEmpty(folder.Name))
                return "My shortlist";
            if (folder.FolderType == FolderType.Mobile)
                return "My mobile favourites";
            return folder.Name;
        }
    }
}
