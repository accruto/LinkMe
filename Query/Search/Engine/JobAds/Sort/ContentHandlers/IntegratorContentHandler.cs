using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.JobAds.Sort.ContentHandlers
{
    internal class IntegratorContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        public IntegratorContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, JobAdSortContent content)
        {
            if (content.JobAd.Integration.IntegratorUserId != null)
            {
                var field = new Field(FieldName.Integrator, content.JobAd.Integration.IntegratorUserId.Value.ToFieldValue(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.setOmitTermFreqAndPositions(true);
                _booster.SetBoost(field);
                document.add(field);
            }
        }

        LuceneSort IContentHandler.GetSort(JobAdSortQuery query)
        {
            return null;
        }
    }
}