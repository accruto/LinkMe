using System;
using System.ServiceModel;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.Members
{
    [ServiceContract]
    [ServiceKnownType(typeof(LiteralTerm))]
    [ServiceKnownType(typeof(CommutativeExpression))]
    [ServiceKnownType(typeof(ModifierExpression))]
    [ServiceKnownType(typeof(UnaryExpression))]
    public interface IMemberSearchService
    {
        [OperationContract]
        MemberSearchResults Search(Guid? employerId, Guid? organisationId, MemberSearchQuery query);

        [OperationContract]
        MemberSearchResults SearchFolder(Guid? employerId, Guid? organisationId, Guid folderId, MemberSearchQuery query);
        [OperationContract]
        MemberSearchResults SearchFlagged(Guid? employerId, Guid? organisationId, MemberSearchQuery query);
        [OperationContract]
        MemberSearchResults SearchBlockList(Guid? employerId, Guid? organisationId, Guid blockListId, MemberSearchQuery query);
        [OperationContract]
        MemberSearchResults SearchSuggested(Guid? employerId, Guid? organisationId, Guid jobAdId, MemberSearchQuery query);
        [OperationContract]
        MemberSearchResults SearchManaged(Guid? employerId, Guid? organisationId, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query);

        [OperationContract]
        void UpdateMember(Guid memberId);

        [OperationContract]
        void IndexMember(Guid memberId);
        [OperationContract]
        void UnindexMember(Guid memberId);
        [OperationContract]
        bool IsIndexed(Guid memberId);

        [OperationContract]
        void Clear();

        [OperationContract]
        SpellCheckCollation[] GetSpellingSuggestions(string queryString, int maxCollations);
    }
}
