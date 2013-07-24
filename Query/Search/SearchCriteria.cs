using System;
using System.Collections.Generic;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.Search
{
    public abstract class SearchCriteria
        : Criteria
    {
        protected static IDictionary<string, CriteriaDescription> Combine(IDictionary<string, CriteriaDescription> descriptions)
        {
            return Combine(new Dictionary<string, CriteriaDescription>(), descriptions);
        }

        protected SearchCriteria(IDictionary<string, CriteriaDescription> descriptions)
            : base(descriptions)
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as SearchCriteria;
            if (other == null)
                return false;

            return GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return 0;
        }

        protected override void OnCloned()
        {
            base.OnCloned();

            // Reset the id so that the clone is not identified as the old instance.

            Id = Guid.Empty;
        }

        public static IExpression CombineKeywords(bool allowShingling, string allKeywords, string exactPhrase, string anyKeywords, string withoutKeywords)
        {
            // All keywords.

            var allKeywordsExpression = allowShingling ? Expression.Parse(allKeywords, ModificationFlags.AllowShingling) : Expression.Parse(allKeywords, BinaryOperator.And);

            // Exact phrase.

            IExpression exactExpression = null;
            if (exactPhrase != null)
            {
                exactPhrase = exactPhrase.Trim(LiteralTerm.Quote).Trim();
                if (exactPhrase.Length > 0)
                    exactExpression = Expression.ParseExactPhrase(exactPhrase);
            }

            // Any keywords.

            var anyKeywordsExpression = Expression.Parse(anyKeywords, BinaryOperator.Or);

            IExpression withoutKeywordsExpression = string.IsNullOrEmpty(withoutKeywords)
                                                        ? null
                                                        : new UnaryExpression(UnaryOperator.Not, Expression.Parse(withoutKeywords, BinaryOperator.Or));

            // Combine all together.

            return Expression.Combine(BinaryOperator.And, allKeywordsExpression, exactExpression, anyKeywordsExpression, withoutKeywordsExpression);
        }

        public static IExpression SplitKeywords(bool allowShingling, string keywords, out string allKeywords, out string exactPhrase, out string anyKeywords, out string withoutKeywords)
        {
            var keywordsExpression = allowShingling ? Expression.Parse(keywords, ModificationFlags.AllowShingling) : Expression.Parse(keywords);
            if (!Expression.SplitIntoSimplifiedParts(keywordsExpression, out allKeywords, out exactPhrase, out anyKeywords, out withoutKeywords))
            {
                allKeywords = keywords;
                exactPhrase = null;
                anyKeywords = null;
                withoutKeywords = null;
            }

            return keywordsExpression;
        }
    }
}