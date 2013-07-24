using System;
using System.ServiceModel;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.Resources
{
    [ServiceContract]
    [ServiceKnownType(typeof(LiteralTerm))]
    [ServiceKnownType(typeof(CommutativeExpression))]
    [ServiceKnownType(typeof(ModifierExpression))]
    [ServiceKnownType(typeof(UnaryExpression))]
    public interface IResourceSearchService
    {
        [OperationContract]
        ResourceSearchResults Search(ResourceSearchQuery searchQuery, bool includeFacets);

        [OperationContract]
        void UpdateItem(Guid itemId);

        [OperationContract]
        void Clear();
    }
}
