using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Queries;

namespace LinkMe.Domain.Users.Members.Views.Queries
{
    public class MemberViewsQuery
        : IMemberViewsQuery
    {
        private readonly IMemberViewsRepository _repository;
        private readonly IMembersQuery _membersQuery;
        private readonly int _invitationAccessDays;

        public MemberViewsQuery(IMemberViewsRepository repository, IMembersQuery membersQuery, int invitationAccessDays)
        {
            _repository = repository;
            _membersQuery = membersQuery;
            _invitationAccessDays = invitationAccessDays;
        }

        PersonalViews IMemberViewsQuery.GetPersonalViews(Guid? fromId, IEnumerable<Guid> toIds)
        {
            return GetPersonalViews(fromId, toIds);
        }

        PersonalViews IMemberViewsQuery.GetPersonalViews(Guid? fromId, IEnumerable<Member> tos)
        {
            return GetPersonalViews(fromId, tos);
        }

        PersonalView IMemberViewsQuery.GetPersonalView(Guid? fromId, Guid toId)
        {
            return GetPersonalViews(fromId, new[] { toId })[toId];
        }

        PersonalView IMemberViewsQuery.GetPersonalView(Guid? fromId, Member to)
        {
            return GetPersonalViews(fromId, new[] { to })[to.Id];
        }

        private PersonalViews GetPersonalViews(Guid? fromId, IEnumerable<Guid> toIds)
        {
            if (toIds == null || !toIds.Any())
                return new PersonalViews();

            // If the from is not set then return anonymous for all tos.

            if (fromId == null)
                return GetAnonymousViews(_membersQuery.GetMembers(toIds));

            var minLastSentTime = DateTime.Now.AddDays(-1 * _invitationAccessDays);
            return _repository.GetPersonalViews(fromId.Value, toIds, minLastSentTime);
        }

        private PersonalViews GetPersonalViews(Guid? fromId, IEnumerable<Member> tos)
        {
            if (tos == null || !tos.Any())
                return new PersonalViews();

            // If the from is not set then return anonymous for all tos.

            if (fromId == null)
                return GetAnonymousViews(tos);

            var minLastSentTime = DateTime.Now.AddDays(-1 * _invitationAccessDays);
            return _repository.GetPersonalViews(fromId.Value, tos, minLastSentTime);
        }

        private static PersonalViews GetAnonymousViews(IEnumerable<Member> tos)
        {
            return new PersonalViews(from to in tos select new PersonalView(to, PersonalContactDegree.Anonymous, PersonalContactDegree.Anonymous));
        }
    }
}