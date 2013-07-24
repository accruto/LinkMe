using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Areas.Employers.Models.Search
{
    public class SearchListModel
        : CandidateListModel
    {
        public SearchListModel()
        {
            CanSelectCommunity = true;
        }

        public bool IsFirstSearch { get; set; }
        public bool IsNewSearch { get; set; }
        public string SavedSearch { get; set; }
        public bool CanSearchByName { get; set; }
        public bool CanSelectCommunity { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<Industry> Industries { get; set; }
        public IList<int> Distances { get; set; }
        public Country DefaultCountry { get; set; }
        public int DefaultDistance { get; set; }
        public IList<RecencyModel> Recencies { get; set; }
        public int DefaultRecency { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public int StepSalary { get; set; }
        public bool CreateEmailAlert { get; set; }
        public bool ShowHelpPrompt { get; set; }
    }

    public class BrowseListModel
        : SearchListModel
    {
        public IUrlNamedLocation Location { get; set; }
        public Salary SalaryBand { get; set; }
    }
}