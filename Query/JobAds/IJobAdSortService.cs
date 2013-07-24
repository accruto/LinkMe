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
    public interface IJobAdSortService
    {
        [OperationContract]
        JobAdSearchResults SortFolder(Guid? memberId, Guid folderId, JobAdSortQuery sortQuery);
        [OperationContract]
        JobAdSearchResults SortFlagged(Guid? memberId, JobAdSortQuery sortQuery);
        [OperationContract]
        JobAdSearchResults SortBlocked(Guid? memberId, JobAdSortQuery sortQuery);

        [OperationContract]
        void UpdateJobAd(Guid jobAdId);

        [OperationContract]
        void Clear();
    }
}
