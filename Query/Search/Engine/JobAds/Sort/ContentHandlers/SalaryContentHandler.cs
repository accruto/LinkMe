using LinkMe.Domain;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.JobAds.Sort.ContentHandlers
{
    internal class SalaryContentHandler
        : SalaryFieldHandler, IContentHandler
    {
        public SalaryContentHandler(IBooster booster)
            : base(FieldName.MinSalary, FieldName.MaxSalary, booster)
        {
        }

        void IContentHandler.AddContent(Document document, JobAdSortContent content)
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

            AddContent(document, salary == null ? null : salary.LowerBound, salary == null ? null : salary.UpperBound, true);
        }

        LuceneSort IContentHandler.GetSort(JobAdSortQuery query)
        {
            return GetSort(query.ReverseSortOrder);
        }
    }
}