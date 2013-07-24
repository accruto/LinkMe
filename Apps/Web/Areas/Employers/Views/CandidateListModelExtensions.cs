using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Search;

namespace LinkMe.Web.Areas.Employers.Views
{
    public static class CandidateListModelExtensions
    {
        public static string GetPageTitle(this CandidateListModel model)
        {
            return model is BrowseListModel
                ? "Find "
                    + ((BrowseListModel) model).SalaryBand.GetSalaryBandDisplayText()
                    + " candidates in "
                    + ((BrowseListModel) model).Location.Name
                : "Jobs - Online Job Search for Jobs, Employment &amp; Careers in Australia";
        }

        public static bool SetHash(this CandidateListModel model)
        {
            return model.GetType() == typeof(SearchListModel);
        }

        public static string GetHash(this CandidateListModel model)
        {
            return model.GetType() == typeof(SearchListModel)
                ? model.Criteria.GetHash()
                : null;
        }

        public static string GetPaginationResultsText(this CandidateListModel model)
        {
            return model is SuggestedCandidatesListModel || model is ManageCandidatesListModel
                ? "Candidates"
                : "Results";
        }

        public static string GetPaginationResultsSuffix(this CandidateListModel model)
        {
            return model is SuggestedCandidatesListModel
                ? " matching your job ad"
                : "";
        }

        public static bool GetBlockCandidatePermanently(this CandidateListModel model)
        {
            return model is ManageCandidatesListModel;
        }

        public static string GetFlagHolderCssClass(this CandidateListModel model)
        {
            return model is ManageCandidatesListModel ? "manage-candidate" : "";
        }
    }
}