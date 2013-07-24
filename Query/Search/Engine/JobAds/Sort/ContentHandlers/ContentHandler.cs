using System.Linq;
using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine.JobAds.Sort.ContentHandlers
{
    internal class ContentHandler
    {
        public void AddContent(Document document, JobAdSortContent content)
        {
            if (!string.IsNullOrEmpty(content.JobAd.Title))
                document.add(new Field(FieldName.Title, content.JobAd.Title, Field.Store.NO, Field.Index.ANALYZED));

            var companyNames = new[] { content.JobAd.Description.CompanyName, content.JobAd.ContactDetails == null ? null : content.JobAd.ContactDetails.CompanyName, content.Employer.CompanyName };
            foreach (var companyName in companyNames.Where(name => !string.IsNullOrEmpty(name)))
                document.add(new Field(FieldName.AdvertiserName, companyName, Field.Store.NO, Field.Index.ANALYZED));
        }
    }
}
