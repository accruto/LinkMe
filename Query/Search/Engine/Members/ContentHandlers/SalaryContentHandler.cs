using com.browseengine.bobo.api;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class SalaryContentHandler
        : SalaryFieldHandler, IContentHandler
    {
        public SalaryContentHandler(IBooster booster)
            : base(FieldName.MinSalary, FieldName.MaxSalary, booster)
        {
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            Salary salary = null;

            if (content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                && content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Salary))
            {
                // Convert to yearly to standardise search.

                salary = content.Candidate.DesiredSalary;
                if (salary != null)
                    salary = salary.ToRate(SalaryRate.Year);
            }

            AddContent(document, salary == null ? null : salary.LowerBound, salary == null ? null : salary.UpperBound, false);
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            // Convert to yearly to standardise search.

            var salary = searchQuery.Salary;
            if (salary != null)
                salary = salary.ToRate(SalaryRate.Year);
            return GetFilter(salary, searchQuery.ExcludeNoSalary);
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return GetSort(searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return null;
        }
    }
}