using com.browseengine.bobo.api;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.ContentHandlers
{
    internal class IntegratorHandler
        : IContentHandler
    {
        #region Implementation of IContentHandler

        public void AddContent(Document document, JobAd jobAd, EmployerContent employerContent)
        {
            if (jobAd.Integration.IntegratorUserId != null)
            {
                var field = new Field(FieldName.Integrator, jobAd.Integration.IntegratorUserId.Value.ToFieldValue(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.setOmitTermFreqAndPositions(true);
                document.add(field);
            }
        }

        public LuceneFilter GetFilter(JobAdSearchQuery searchQuery)
        {
            if (searchQuery.ExcludeIntegratorIds == null || searchQuery.ExcludeIntegratorIds.Count == 0)
                return null;

            var termsFilter = new TermsFilter();
            foreach (var integratorId in searchQuery.ExcludeIntegratorIds)
            {
                termsFilter.addTerm(new Term(FieldName.Integrator, integratorId.ToFieldValue()));
            }

            var filter = new BooleanFilter();
            filter.add(new FilterClause(termsFilter, BooleanClause.Occur.MUST_NOT ));

            return filter;
        }

        public Sort GetSort(JobAdSearchQuery searchQuery)
        {
            return null;
        }

        public BrowseSelection GetSelection(JobAdSearchQuery searchQuery)
        {
            /*
            if (searchQuery.ExcludeIntegratorIds == null || searchQuery.ExcludeIntegratorIds.Count == 0)
                return null;

            var selection = new BrowseSelection(FieldName.Integrator);
            foreach (var integratorId in searchQuery.ExcludeIntegratorIds)
                selection.addNotValue(integratorId.ToFieldValue());

            return selection;
            */

            return null;
        }

        #endregion
    }
}