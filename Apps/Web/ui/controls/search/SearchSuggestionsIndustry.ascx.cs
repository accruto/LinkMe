using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Candidates;

namespace LinkMe.Web.UI.Controls.Search
{
    public partial class SearchSuggestionsIndustry
        : LinkMeUserControl
    {
        protected class IndustryInfo
        {
            public Guid Id;
            public string Name;
            public int Freq;

            public IndustryInfo(Guid id, string name, int freq)
            {
                Id = id;
                Name = name;
                Freq = freq;
            }
        }

        private IList<IndustryInfo> _industries;

        protected IList<IndustryInfo> Industries
        {
            get { return _industries; }
        }

        protected static string GetItemHtml(object item)
        {
            var industry = (IndustryInfo)item;
            return string.Format("<a href=\"javascript:NarrowSearch({{'Industries':'{0}'}})\">{1}</a>",
                industry.Id, industry.Name);
        }

        public void Display(IEnumerable<Candidate> candidates)
        {
            _industries = (from i in candidates.SelectMany(c => c.Industries)
                           group i by new { i.Id, i.ShortName } into g
                           select new IndustryInfo(g.Key.Id, g.Key.ShortName, g.Count())).OrderByDescending(i => i.Freq).Take(5).ToList();
            repeater.DataSource = _industries.Count > 1 && _industries.Count < 5 ? _industries : null;
            repeater.DataBind();
        }
    }
}
