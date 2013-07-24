using System.Linq;
using LinkMe.Framework.Utility;
using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    internal class ContentHandler
    {
        private readonly IJobAdSearchBooster _booster;

        public ContentHandler(IJobAdSearchBooster booster)
        {
            _booster = booster;
        }

        public void AddContent(Document document, JobAdSearchContent content)
        {
            // Title

            if (!string.IsNullOrEmpty(content.JobAd.Title))
            {
                var field = new Field(FieldName.Title, content.JobAd.Title, Field.Store.YES, Field.Index.ANALYZED);
                _booster.SetBoost(field);
                document.add(field);

                field = new Field(FieldName.TitleExact, content.JobAd.Title, Field.Store.NO, Field.Index.ANALYZED);
                _booster.SetBoost(field);
                document.add(field);

                field = new Field(FieldName.Content, content.JobAd.Title, Field.Store.YES, Field.Index.ANALYZED);
                _booster.SetBoost(field);
                document.add(field);

                field = new Field(FieldName.ContentExact, content.JobAd.Title, Field.Store.NO, Field.Index.ANALYZED);
                _booster.SetBoost(field);
                document.add(field);
            }

            // BulletPoints

            if (content.JobAd.Description.BulletPoints != null)
            {
                foreach (var bulletPoint in content.JobAd.Description.BulletPoints)
                {
                    var field = new Field(FieldName.BulletPoints, bulletPoint, Field.Store.NO, Field.Index.ANALYZED);
                    _booster.SetBoost(field);
                    document.add(field);

                    field = new Field(FieldName.BulletPointsExact, bulletPoint, Field.Store.NO, Field.Index.ANALYZED);
                    _booster.SetBoost(field);
                    document.add(field);

                    document.add(new Field(FieldName.Content, bulletPoint, Field.Store.YES, Field.Index.ANALYZED));
                    document.add(new Field(FieldName.ContentExact, bulletPoint, Field.Store.NO, Field.Index.ANALYZED));
                }
            }

            // Content

            if (!string.IsNullOrEmpty(content.JobAd.Description.Content))
            {
                //strip HTML from the content before indexing
                var strippedContent = HtmlUtil.StripHtmlTags(content.JobAd.Description.Content);

                document.add(new Field(FieldName.Content, strippedContent, Field.Store.YES, Field.Index.ANALYZED));
                document.add(new Field(FieldName.ContentExact, strippedContent, Field.Store.NO, Field.Index.ANALYZED));
            }

            // AdvertiserName

            // Should these be subject to HideContactDetails etc?

            var companyNames = new[] { content.JobAd.Description.CompanyName, content.JobAd.ContactDetails == null ? null : content.JobAd.ContactDetails.CompanyName, content.Employer.CompanyName };
            foreach (var companyName in companyNames.Where(name => !string.IsNullOrEmpty(name)))
            {
                var field = new Field(FieldName.AdvertiserName, companyName, Field.Store.NO, Field.Index.ANALYZED);
                _booster.SetBoost(field);
                document.add(field);

                field = new Field(FieldName.AdvertiserNameExact, companyName, Field.Store.NO, Field.Index.ANALYZED);
                _booster.SetBoost(field);
                document.add(field);

                document.add(new Field(FieldName.Content, companyName, Field.Store.NO, Field.Index.ANALYZED));
                document.add(new Field(FieldName.ContentExact, companyName, Field.Store.NO, Field.Index.ANALYZED));
            }

            // ExternalReferenceId

            if (!string.IsNullOrEmpty(content.JobAd.Integration.ExternalReferenceId))
            {
                document.add(new Field(FieldName.Content, content.JobAd.Integration.ExternalReferenceId, Field.Store.NO, Field.Index.ANALYZED));
                document.add(new Field(FieldName.ContentExact, content.JobAd.Integration.ExternalReferenceId, Field.Store.NO, Field.Index.ANALYZED));
            }

            // Package

            if (!string.IsNullOrEmpty(content.JobAd.Description.Package))
            {
                document.add(new Field(FieldName.Content, content.JobAd.Description.Package, Field.Store.YES, Field.Index.ANALYZED));
                document.add(new Field(FieldName.ContentExact, content.JobAd.Description.Package, Field.Store.NO, Field.Index.ANALYZED));
            }
        }
    }
}
