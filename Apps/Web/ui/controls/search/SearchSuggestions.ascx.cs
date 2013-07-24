using System.Collections.Generic;
using LinkMe.Domain.Roles.Candidates;

namespace LinkMe.Web.UI.Controls.Search
{
    public partial class SearchSuggestions
        : LinkMeUserControl
    {
        public void Display(IEnumerable<Candidate> candidates)
        {
            ucIndustry.Display(candidates);
        }
    }
}
