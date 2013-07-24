using com.browseengine.bobo.api;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using org.apache.lucene.analysis;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class NameContentHandler
        : IContentHandler
    {
        private readonly Analyzer _analyzer;
        private readonly IBooster _booster;

        public NameContentHandler(Analyzer analyzer, IBooster booster)
        {
            _analyzer = analyzer;
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            // Only include if visible.

            if (!content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                || !content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name))
                return;

            if (!string.IsNullOrEmpty(content.Member.FirstName))
            {
                var field = new Field(FieldName.FirstName, content.Member.FirstName.ToLowerInvariant(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                _booster.SetBoost(field);
                document.add(field);
                field = new Field(FieldName.Name, _analyzer.tokenStream(FieldName.FirstName, new java.io.StringReader(content.Member.FirstName)));
                _booster.SetBoost(field);
                document.add(field);
                field = new Field(FieldName.Name_Exact, _analyzer.tokenStream(FieldName.FirstName_Exact, new java.io.StringReader(content.Member.FirstName)));
                _booster.SetBoost(field); 
                document.add(field);
            }

            if (!string.IsNullOrEmpty(content.Member.LastName))
            {
                var field = new Field(FieldName.LastName, content.Member.LastName.ToLowerInvariant(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                _booster.SetBoost(field);
                document.add(field);
                field = new Field(FieldName.Name, _analyzer.tokenStream(FieldName.LastName, new java.io.StringReader(content.Member.LastName)));
                _booster.SetBoost(field);
                document.add(field);
                field = new Field(FieldName.Name_Exact, _analyzer.tokenStream(FieldName.LastName_Exact, new java.io.StringReader(content.Member.LastName)));
                _booster.SetBoost(field);
                document.add(field);
            }
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            return null;
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return new Sort(new[]
            {
                new SortField(FieldName.FirstName, SortField.STRING, searchQuery.ReverseSortOrder),
                new SortField(FieldName.LastName, SortField.STRING, searchQuery.ReverseSortOrder)
            });
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return null;
        }
    }
}
