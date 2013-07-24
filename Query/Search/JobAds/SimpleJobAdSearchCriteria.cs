using System;
using System.Collections.Generic;
using System.Text;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds
{
    public class SimpleJobAdSearchCriteria
        : JobAdSearchCriteria, ISearchCriteriaIndustries
    {
        private const string KeywordsName = "Keywords";
        private const string IndustryIdName = "IndustryId";
        private const string IndustriesName = "Industries";

        private static readonly IDictionary<string, CriteriaDescription> Descriptions = new Dictionary<string, CriteriaDescription>
        {
            {KeywordsName, new CriteriaValueDescription<string>(null)},
            {IndustryIdName, new CriteriaValueDescription<Guid?>(null, new GuidValuePersister())},
            {IndustriesName, new CriteriaValueDescription<string>(null)},
        };

        private static readonly IDictionary<string, CriteriaDescription> CombinedDescriptions = JobAdSearchCriteria.Combine(Descriptions);

        protected new static IDictionary<string, CriteriaDescription> Combine(IDictionary<string, CriteriaDescription> descriptions)
        {
            return Combine(CombinedDescriptions, descriptions);
        }

        public SimpleJobAdSearchCriteria()
            : base(CombinedDescriptions)
        {
        }

        public string Keywords
        {
            get
            {
                return GetValue<string>(KeywordsName);
            }
            set
            {
                value = value.NullIfEmpty();
                SetValue(KeywordsName, value);
                KeywordsExpression = Expression.Parse(value);
            }
        }

        public Guid? IndustryId
        {
            get { return GetValue<Guid?>(IndustryIdName); }
            set { SetValue(IndustryIdName, value); }
        }

        public override bool Equals(object obj)
        {
            var other = obj as SimpleJobAdSearchCriteria;
            if (other == null)
                return false;

            return base.Equals(obj)
                   && IndustryId == other.IndustryId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                   ^ IndustryId.GetHashCode();
        }

        public override bool IsEmpty
        {
            get
            {
                return base.IsEmpty
                       && IndustryId == null;
            }
        }

        protected override void ToString(StringBuilder sb)
        {
            base.ToString(sb);
            sb.AppendFormat(", keywords: '{0}'", Keywords);
        }

        public override bool HasFilters
        {
            get { return base.HasFilters || IndustryId != null; }
        }

        protected override void OnCloned()
        {
            base.OnCloned();
            KeywordsExpression = Expression.Parse(Keywords);
        }

        string ISearchCriteriaIndustries.Industries
        {
            get { return GetValue<string>(IndustriesName); }
        }

        IList<Guid> ISearchCriteriaIndustries.IndustryIds
        {
            get { return IndustryId == null ? null : new List<Guid> {IndustryId.Value}; }
            set { IndustryId = value == null || value.Count == 0 ? (Guid?)null : value[0]; }
        }

        public override JobAdSearchQuery GetSearchQuery(Range range)
        {
            var searchquery = base.GetSearchQuery(range);
            searchquery.IndustryIds = ((ISearchCriteriaIndustries) this).IndustryIds;
            return searchquery;
        }

        public override string GetKeywords()
        {
            return Keywords;
        }
    }
}