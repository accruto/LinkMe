using System;
using System.ServiceModel;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.JobAds
{
    [ServiceContract]
    [ServiceKnownType(typeof(LiteralTerm))]
    [ServiceKnownType(typeof(CommutativeExpression))]
    [ServiceKnownType(typeof(ModifierExpression))]
    [ServiceKnownType(typeof(UnaryExpression))]
    public interface IJobAdSearchService
    {
        [OperationContract]
        JobAdSearchResults Search(Guid? memberId, JobAdSearchQuery query);
        [OperationContract]
        JobAdSearchResults SearchFlagged(Guid? memberId, JobAdSearchQuery query);
        [OperationContract]
        JobAdSearchResults SearchSuggested(Guid? memberId, JobAdSearchQuery query);
        [OperationContract]
        JobAdSearchResults SearchSimilar(Guid? memberId, Guid jobAdId, JobAdSearchQuery query);

        [OperationContract]
        void UpdateJobAd(Guid jobAdId);

        [OperationContract]
        bool IsIndexed(Guid jobAdId);

        [OperationContract]
        void Clear();
    }
}
