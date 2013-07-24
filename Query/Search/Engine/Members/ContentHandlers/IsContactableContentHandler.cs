using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.util;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class IsContactableContentHandler
        : IContentHandler
    {
        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            document.add(new NumericField(FieldName.IsContactable).setIntValue(CanContact(content.Member) ? 1 : 0));
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            if (searchQuery.IsContactable == null)
                return null;

            var filter = new TermsFilter();
            filter.addTerm(new Term(FieldName.IsContactable, NumericUtils.intToPrefixCoded(searchQuery.IsContactable.Value ? 1 : 0)));
            return filter;
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return null;
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return null;
        }

        private static bool CanContact(IMember member)
        {
            // Must have either a verified email address ...

            if (member.EmailAddresses != null && member.EmailAddresses.Any(a => a.IsVerified))
                return true;

            // or have a phone number.

            return member.PhoneNumbers != null && member.PhoneNumbers.Count > 0;
        }
    }
}
