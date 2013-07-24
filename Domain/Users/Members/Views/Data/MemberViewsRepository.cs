using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Users.Members.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Users.Members.Views.Data
{
    public class MemberViewsRepository
        : Repository, IMemberViewsRepository
    {
        // To be a first degree contact:
        // - must be a link
        // - must be enabled
        // - must be activated
        // - name must be visible to first degree contacts or be a representative or representee

        // To be a second degree contact
        // - must be links
        // - the first degree contacts
        //   - must be enabled,
        //   - must be activated
        //   - must have name visible to first degree contacts or be a representative or be a representee
        //   - must have their friend's list visible
        // - the second degree contact
        //   - must be enabled
        //   - must be activated
        //   - must have their name visible to second degree contacts
        //   - must not be ignored
        //   - must also not be a first degree contact

        // To be a public contact
        // - must be enabled
        // - must be activated
        // - name must be visible to the oublic

        // To be a contact
        // - must be the contact
        // - or must be a first degree contact
        // - or a second degree contact
        // - or have a networking invitation
        // - or have a representative invitation
        // - or be a public contact

        private readonly ILocationQuery _locationQuery;

        private class GetContactsArgs
        {
            public Guid FromId;
            public string ToIds;
            public DateTime MinLastSentTime;
        }

        private static readonly DataLoadOptions ContactLoadOptions = DataOptions.CreateLoadOptions<MemberEntity, AddressEntity>(m => m.AddressEntity, a => a.LocationReferenceEntity);

        private static readonly Func<ViewsDataContext, GetContactsArgs, ILocationQuery, IQueryable<PersonalView>> GetViewsQuery
            = CompiledQuery.Compile((ViewsDataContext dc, GetContactsArgs args, ILocationQuery locationQuery)
                                    => from m in dc.MemberEntities
                                       join u in dc.RegisteredUserEntities on m.id equals u.id
                                       join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                                       from tc in tCommunityMemberEntities.DefaultIfEmpty()
                                       join i in dc.SplitGuids(SplitList<Guid>.Delimiter, args.ToIds) on m.id equals i.value

                                       let isRepresentative = (from r in dc.RepresentativeEntities
                                                               where r.representeeId == args.FromId
                                                                     && r.representativeId == m.id
                                                               select r).Any()
                   
                                       let isRepresentee = (from r in dc.RepresentativeEntities
                                                            where r.representeeId == m.id
                                                                  && r.representativeId == args.FromId
                                                            select r).Any()
                   
                                       let isFirstDegree = (from l in dc.NetworkLinkEntities
                                                            join fm in dc.MemberEntities on l.toNetworkerId equals fm.id
                                                            join fu in dc.RegisteredUserEntities on fm.id equals fu.id
                                                            where l.fromNetworkerId == args.FromId
                                                                  && l.toNetworkerId == m.id
                                                                  && (fu.flags & (int) UserFlags.Disabled) == (int) UserFlags.None
                                                                  && (fu.flags & (int) UserFlags.Activated) == (int) UserFlags.Activated
                                                                  &&
                                                                  (
                                                                      (fm.firstDegreeAccess & (int) PersonalVisibility.Name) == (int) PersonalVisibility.Name
                                                                      ||
                                                                      isRepresentative
                                                                      ||
                                                                      isRepresentee
                                                                  )
                                                            select m.id).Any()
                   
                                       let isSecondDegree = (from l1 in dc.NetworkLinkEntities
                                                             join l2 in dc.NetworkLinkEntities on l1.toNetworkerId equals l2.fromNetworkerId
                                                             join m1 in dc.MemberEntities on l2.fromNetworkerId equals m1.id
                                                             join u1 in dc.RegisteredUserEntities on m1.id equals u1.id
                                                             join m2 in dc.MemberEntities on l2.toNetworkerId equals m2.id
                                                             join u2 in dc.RegisteredUserEntities on m2.id equals u2.id
                                                             where l1.fromNetworkerId == args.FromId
                                                                   && l2.toNetworkerId == m.id
                                                                   && (u1.flags & (int) UserFlags.Disabled) == (int) UserFlags.None
                                                                   && (u1.flags & (int) UserFlags.Activated) == (int) UserFlags.Activated
                                                                   &&
                                                                   (
                                                                       (m1.firstDegreeAccess & (int) PersonalVisibility.Name) == (int) PersonalVisibility.Name
                                                                       ||
                                                                       (from r in dc.RepresentativeEntities where r.representativeId == args.FromId && r.representeeId == m1.id select r).Any()
                                                                       ||
                                                                       (from r in dc.RepresentativeEntities where r.representeeId == args.FromId && r.representativeId == m1.id select r).Any()
                                                                   )
                                                                   && (m1.firstDegreeAccess & (int) PersonalVisibility.FriendsList) == (int) PersonalVisibility.FriendsList
                                                                   && (u2.flags & (int) UserFlags.Disabled) == (int) UserFlags.None
                                                                   && (u2.flags & (int) UserFlags.Activated) == (int) UserFlags.Activated
                                                                   && (m2.secondDegreeAccess & (int) PersonalVisibility.Name) == (int) PersonalVisibility.Name
                                                                   && l2.toNetworkerId != args.FromId
                                                                   && !(from l3 in dc.NetworkLinkEntities
                                                                        where l3.fromNetworkerId == args.FromId
                                                                              && l3.toNetworkerId == l2.toNetworkerId
                                                                        select l3).Any()
                                                                   && !(from im in dc.IgnoredNetworkMatchEntities
                                                                        where im.ignoredId == l2.toNetworkerId
                                                                              && im.ignorerId == args.FromId
                                                                        select im).Any()
                                                             select l2).Any()

                                       let hasNetworkingInvitation = (from ni in dc.NetworkInvitationEntities
                                                                      join ur in dc.UserToUserRequestEntities on ni.id equals ur.id
                                                                      where ni.inviteeId == args.FromId
                                                                            && ni.inviterId == m.id
                                                                            && ur.lastSentTime >= args.MinLastSentTime
                                                                            && ur.status == (byte) RequestStatus.Pending
                                                                      select ni).Any()

                                       let hasRepresentativeInvitation = (from ri in dc.RepresentativeInvitationEntities
                                                                          join ur in dc.UserToUserRequestEntities on ri.id equals ur.id
                                                                          where ri.inviteeId == args.FromId
                                                                                && ri.inviterId == m.id
                                                                                && ur.lastSentTime >= args.MinLastSentTime
                                                                                && ur.status == (byte)RequestStatus.Pending
                                                                          select ri).Any()

                                       let actual = args.FromId == m.id
                                                        ? PersonalContactDegree.Self
                                                        : isFirstDegree
                                                              ? PersonalContactDegree.FirstDegree
                                                              : isSecondDegree
                                                                    ? PersonalContactDegree.SecondDegree
                                                                    : PersonalContactDegree.Public

                                       let effective = args.FromId == i.value
                                                           ? PersonalContactDegree.Self
                                                           : (isRepresentee || hasRepresentativeInvitation)
                                                                 ? PersonalContactDegree.Representee
                                                                 : isRepresentative
                                                                       ? PersonalContactDegree.Representative
                                                                       : (isFirstDegree || hasNetworkingInvitation)
                                                                             ? PersonalContactDegree.FirstDegree
                                                                             : isSecondDegree
                                                                                   ? PersonalContactDegree.SecondDegree
                                                                                   : PersonalContactDegree.Public

                                       select new PersonalView(m.Map(u, tc, locationQuery), effective, actual));

        private static readonly Func<ViewsDataContext, GetContactsArgs, IQueryable<Tuple<Guid, Tuple<PersonalContactDegree, PersonalContactDegree>>>> GetContactDegreesQuery
            = CompiledQuery.Compile((ViewsDataContext dc, GetContactsArgs args)
                                    => from i in dc.SplitGuids(SplitList<Guid>.Delimiter, args.ToIds)

                                       let isRepresentative = (from r in dc.RepresentativeEntities
                                                               where r.representeeId == args.FromId
                                                                     && r.representativeId == i.value
                                                               select r).Any()

                                       let isRepresentee = (from r in dc.RepresentativeEntities
                                                            where r.representeeId == i.value
                                                                  && r.representativeId == args.FromId
                                                            select r).Any()

                                       let isFirstDegree = (from l in dc.NetworkLinkEntities
                                                            join fm in dc.MemberEntities on l.toNetworkerId equals fm.id
                                                            join fu in dc.RegisteredUserEntities on fm.id equals fu.id
                                                            where l.fromNetworkerId == args.FromId && l.toNetworkerId == i.value
                                                                  && (fu.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                                                  && (fu.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                                                  &&
                                                                  (
                                                                      (fm.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                                                      ||
                                                                      isRepresentative
                                                                      ||
                                                                      isRepresentee
                                                                  )
                                                            select l).Any()

                                       let isSecondDegree = (from l1 in dc.NetworkLinkEntities
                                                             join l2 in dc.NetworkLinkEntities on l1.toNetworkerId equals l2.fromNetworkerId
                                                             join m1 in dc.MemberEntities on l2.fromNetworkerId equals m1.id
                                                             join u1 in dc.RegisteredUserEntities on m1.id equals u1.id
                                                             join m2 in dc.MemberEntities on l2.toNetworkerId equals m2.id
                                                             join u2 in dc.RegisteredUserEntities on m2.id equals u2.id
                                                             where l1.fromNetworkerId == args.FromId
                                                                   && l2.toNetworkerId == i.value
                                                                   && (u1.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                                                   && (u1.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                                                   &&
                                                                   (
                                                                       (m1.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                                                       ||
                                                                       (from r in dc.RepresentativeEntities where r.representativeId == args.FromId && r.representeeId == m1.id select r).Any()
                                                                       ||
                                                                       (from r in dc.RepresentativeEntities where r.representeeId == args.FromId && r.representativeId == m1.id select r).Any()
                                                                   )
                                                                   && (m1.firstDegreeAccess & (int)PersonalVisibility.FriendsList) == (int)PersonalVisibility.FriendsList
                                                                   && (u2.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                                                   && (u2.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                                                   && (m2.secondDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                                                   && l2.toNetworkerId != args.FromId
                                                                   && !(from l3 in dc.NetworkLinkEntities
                                                                        where l3.fromNetworkerId == args.FromId
                                                                              && l3.toNetworkerId == l2.toNetworkerId
                                                                        select l3).Any()
                                                                   && !(from im in dc.IgnoredNetworkMatchEntities
                                                                        where im.ignoredId == l2.toNetworkerId
                                                                              && im.ignorerId == args.FromId
                                                                        select im).Any()
                                                             select l2).Any()

                                       let hasNetworkingInvitation = (from ni in dc.NetworkInvitationEntities
                                                                      join ur in dc.UserToUserRequestEntities on ni.id equals ur.id
                                                                      where ni.inviteeId == args.FromId
                                                                            && ni.inviterId == i.value
                                                                            && ur.lastSentTime >= args.MinLastSentTime
                                                                            && ur.status == (byte)RequestStatus.Pending
                                                                      select ni).Any()

                                       let hasRepresentativeInvitation = (from ri in dc.RepresentativeInvitationEntities
                                                                          join ur in dc.UserToUserRequestEntities on ri.id equals ur.id
                                                                          where ri.inviteeId == args.FromId
                                                                                && ri.inviterId == i.value
                                                                                && ur.lastSentTime >= args.MinLastSentTime
                                                                                && ur.status == (byte)RequestStatus.Pending
                                                                          select ri).Any()

                                       let actual = args.FromId == i.value
                                                        ? PersonalContactDegree.Self
                                                        : isFirstDegree
                                                              ? PersonalContactDegree.FirstDegree
                                                              : isSecondDegree
                                                                    ? PersonalContactDegree.SecondDegree
                                                                    : PersonalContactDegree.Public

                                       let effective = args.FromId == i.value
                                                           ? PersonalContactDegree.Self
                                                           : (isRepresentee || hasRepresentativeInvitation)
                                                                 ? PersonalContactDegree.Representee
                                                                 : isRepresentative
                                                                       ? PersonalContactDegree.Representative
                                                                       : (isFirstDegree || hasNetworkingInvitation)
                                                                             ? PersonalContactDegree.FirstDegree
                                                                             : isSecondDegree
                                                                                   ? PersonalContactDegree.SecondDegree
                                                                                   : PersonalContactDegree.Public

                                       select new Tuple<Guid, Tuple<PersonalContactDegree, PersonalContactDegree>>(
                                           i.value.Value,
                                           new Tuple<PersonalContactDegree, PersonalContactDegree>(effective, actual)));

        public MemberViewsRepository(IDataContextFactory dataContextFactory, ILocationQuery locationQuery)
            : base(dataContextFactory)
        {
            _locationQuery = locationQuery;
        }

        PersonalViews IMemberViewsRepository.GetPersonalViews(Guid fromId, IEnumerable<Guid> toIds, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var views = GetViews(dc, fromId, toIds, minLastSentTime).ToArray();
                return new PersonalViews(views);
            }
        }

        PersonalViews IMemberViewsRepository.GetPersonalViews(Guid fromId, IEnumerable<Member> tos, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var views = GetViews(dc, fromId, tos, minLastSentTime).ToArray();
                return new PersonalViews(views);
            }
        }

        private IQueryable<PersonalView> GetViews(ViewsDataContext dc, Guid fromId, IEnumerable<Guid> toIds, DateTime minSentTime)
        {
            dc.LoadOptions = ContactLoadOptions;
            var args = new GetContactsArgs
                           {
                               FromId = fromId,
                               ToIds = new SplitList<Guid>(toIds.Distinct()).ToString(),
                               MinLastSentTime = minSentTime
                           };
            return GetViewsQuery(dc, args, _locationQuery);
        }

        private static IEnumerable<PersonalView> GetViews(ViewsDataContext dc, Guid fromId, IEnumerable<Member> tos, DateTime minSentTime)
        {
            dc.LoadOptions = ContactLoadOptions;
            var args = new GetContactsArgs
                           {
                               FromId = fromId,
                               ToIds = new SplitList<Guid>((from to in tos select to.Id).Distinct()).ToString(),
                               MinLastSentTime = minSentTime
                           };

            var contactDegrees = GetContactDegreesQuery(dc, args).ToDictionary(c => c.Item1, c => c.Item2);

            return from to in tos
                   let c = (contactDegrees.ContainsKey(to.Id) ? contactDegrees[to.Id] : new Tuple<PersonalContactDegree, PersonalContactDegree>(PersonalContactDegree.Public, PersonalContactDegree.Public))
                   select new PersonalView(to, c.Item1, c.Item2);
        }

        private ViewsDataContext CreateContext()
        {
            return CreateContext(c => new ViewsDataContext(c));
        }
    }
}