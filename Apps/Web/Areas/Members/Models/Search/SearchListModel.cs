using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Areas.Members.Models.Search
{
    public class BrowseListModel
        : SearchListModel
    {
        public Industry Industry { get; set; }
        public IUrlNamedLocation Location { get; set; }
    }

    public class SearchListModel
        : JobAdSearchListModel
    {
        public bool IsFirstSearch { get; set; }
        public bool IsSavedSearch { get; set; }
        public SearchAncillaryModel AncillaryData { get; set; }
    }

    public class SearchAncillaryModel
    {
        public IList<Region> Regions { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public Country DefaultCountry { get; set; }
        
        public IList<Industry> Industries { get; set; }

        public IList<int> Distances { get; set; }
        public int DefaultDistance { get; set; }
        
        public IList<RecencyModel> Recencies { get; set; }
        public int DefaultRecency { get; set; }
        public int MaxRecency { get; set; }
        
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public int StepSalary { get; set; }
        public int MinHourlySalary { get; set; }
        public int MaxHourlySalary { get; set; }
        public int StepHourlySalary { get; set; }
    }
}