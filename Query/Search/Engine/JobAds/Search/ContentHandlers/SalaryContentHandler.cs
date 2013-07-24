using com.browseengine.bobo.api;
using LinkMe.Domain;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    internal class SalaryContentHandler
        : SalaryFieldHandler, IContentHandler
    {
        private readonly IJobAdSearchBooster _booster;

        public SalaryContentHandler(IJobAdSearchBooster booster)
            : base(FieldName.MinSalary, FieldName.MaxSalary, booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, JobAdSearchContent content)
        {
            Salary salary = null;

            if (content.JobAd.Description.Salary == null)
            {
                if (content.JobAd.Description.ParsedSalary != null)
                    salary = content.JobAd.Description.ParsedSalary.ToRate(SalaryRate.Year);
            }
            else
            {
                salary = content.JobAd.Description.Salary.ToRate(SalaryRate.Year);
            }

            var minSalary = salary == null ? null : salary.LowerBound;
            var maxSalary = salary == null ? null : salary.UpperBound;
            AddContent(document, minSalary, maxSalary, true);

            // Boost documents with a salary.

            _booster.SetSalaryBoost(document, minSalary.HasValue || maxSalary.HasValue);
        }

        LuceneFilter IContentHandler.GetFilter(JobAdSearchQuery searchQuery)
        {
            return GetFilter(searchQuery.Salary, searchQuery.ExcludeNoSalary);
        }

        LuceneSort IContentHandler.GetSort(JobAdSearchQuery searchQuery)
        {
            return GetSort(searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(JobAdSearchQuery searchQuery)
        {
            return null;
        }
    }
}