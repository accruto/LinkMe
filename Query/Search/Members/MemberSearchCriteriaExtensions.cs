using System;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.Search.Members
{
    public static class MemberSearchCriteriaExtensions
    {
        public static MemberSearchCriteria Clone(this MemberSearchCriteria criteria)
        {
            return (MemberSearchCriteria) ((ICloneable)criteria).Clone();
        }

        public static MemberSearchCriteria ChangeToSearchAllJobs(this MemberSearchCriteria criteria)
        {
            var cloned = criteria.Clone();
            cloned.JobTitlesToSearch = 0;
            return cloned;
        }

        public static MemberSearchCriteria ChangeJobTitleToKeywords(this MemberSearchCriteria criteria)
        {
            var cloned = criteria.Clone();
            cloned.SetKeywords(cloned.JobTitleExpression == null ? null : cloned.JobTitleExpression.GetUserExpression());
            cloned.JobTitle = null;
            return cloned;
        }

        public static MemberSearchCriteria ChangeKeywordsToOred(this MemberSearchCriteria criteria)
        {
            var cloned = criteria.Clone();
            var expression = Expression.Flatten(cloned.KeywordsExpression, BinaryOperator.Or);
            cloned.SetKeywords(expression == null ? null : expression.GetUserExpression());
            return cloned;
        }
    }
}