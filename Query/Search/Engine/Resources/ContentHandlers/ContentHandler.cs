using LinkMe.Domain.Resources;
using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine.Resources.ContentHandlers
{
    internal class ContentHandler
    {
        private readonly IResourceSearchBooster _booster;

        public ContentHandler(IResourceSearchBooster booster)
        {
            _booster = booster;
        }

        public void AddContent(Document document, ResourceContent content)
        {
            if (content.Resource != null)
            {
                if (!string.IsNullOrEmpty(content.Resource.Text))
                    document.add(new Field(FieldName.Content, content.Resource.Text, Field.Store.YES, Field.Index.ANALYZED));
                if (!string.IsNullOrEmpty(content.Resource.Title))
                    document.add(new Field(FieldName.Content, content.Resource.Title, Field.Store.NO, Field.Index.ANALYZED));

                var faq = content.Resource as Faq;
                if (faq != null)
                {
                    if (!string.IsNullOrEmpty(faq.Keywords))
                        document.add(new Field(FieldName.Content, faq.Keywords, Field.Store.YES, Field.Index.ANALYZED));

                    _booster.SetHelpfulBoost(document, faq.HelpfulYes - faq.HelpfulNo);
                }
            }
        }
    }
}
