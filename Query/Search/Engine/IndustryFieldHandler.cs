using System;
using System.Collections.Generic;
using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain.Industries.Queries;
using org.apache.lucene.document;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine
{
    internal class IndustryFieldHandler
    {
        private readonly string _fieldName;
        private readonly IBooster _booster;
        private readonly HashSet<Guid> _allIndustryIds;
        private readonly Guid? _otherIndustryId;

        public IndustryFieldHandler(string fieldName, IBooster booster, IIndustriesQuery industriesQuery)
        {
            _fieldName = fieldName;
            _booster = booster;
            _allIndustryIds = new HashSet<Guid>(industriesQuery.GetIndustries().Select(i => i.Id));
            var otherIndustry = industriesQuery.GetIndustry("Other"); 
            _otherIndustryId = otherIndustry == null ? (Guid?) null : otherIndustry.Id;
        }

        protected void AddContent(Document document, IEnumerable<Guid> industryIds)
        {
            if (industryIds != null)
            {
                foreach (var value in industryIds.Select(industry => industry.ToFieldValue()))
                {
                    var field = new Field(_fieldName, value, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                    _booster.SetBoost(field);
                    document.add(field);
                }
            }
            else
            {
                // If no industry has been supplied then include under "Other".

                if (_otherIndustryId != null)
                {
                    var field = new Field(_fieldName, _otherIndustryId.Value.ToFieldValue(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                    _booster.SetBoost(field);
                    document.add(field);
                }
            }
        }

        protected BrowseSelection GetSelection(ICollection<Guid> industryIds)
        {
            if (industryIds == null || industryIds.Count == 0 || _allIndustryIds.IsSubsetOf(industryIds))
                return null;

            var selection = new BrowseSelection(_fieldName);
            selection.setSelectionOperation(BrowseSelection.ValueOperation.ValueOperationOr);

            foreach (var industry in industryIds.Select(id => id.ToFieldValue()))
                selection.addValue(industry);

            return selection;
        }
    }
}