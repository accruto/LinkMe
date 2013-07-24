using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Users.Members.Data;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Users.Members.Contacts.Data
{
    public class MemberContactsRepository
        : Repository, IMemberContactsRepository
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

        // Representative contacts.

        private static readonly Func<ContactsDataContext, Guid, Guid> GetRepresentativeContact
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId)
                => (from r in dc.RepresentativeEntities
                    join m in dc.MemberEntities on r.representativeId equals m.id
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    where r.representeeId == fromId
                    && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    select m.id).SingleOrDefault());

        private static readonly Func<ContactsDataContext, string, IQueryable<Tuple<Guid, Guid>>> GetRepresentativeContacts
            = CompiledQuery.Compile((ContactsDataContext dc, string fromIds)
                => from r in dc.RepresentativeEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, fromIds) on r.representeeId equals i.value
                   join m in dc.MemberEntities on r.representativeId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   select new Tuple<Guid, Guid>(r.representeeId, r.representativeId));

        private static readonly Func<ContactsDataContext, Guid, IQueryable<Guid>> GetRepresenteeContacts
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId)
                => from r in dc.RepresentativeEntities
                   join m in dc.MemberEntities on r.representeeId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where r.representativeId == fromId
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   select m.id);

        private static readonly Func<ContactsDataContext, Guid, string, IQueryable<Guid>> GetRepresenteeContactsByName
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, string nameStartsWith)
                => from r in dc.RepresentativeEntities
                   join m in dc.MemberEntities on r.representeeId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where r.representativeId == fromId
                   && SqlMethods.Like(u.firstName, nameStartsWith)
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   select m.id);

        // First degree contacts.

        private static readonly Func<ContactsDataContext, Guid, IQueryable<MemberEntity>> GetFirstDegreeContactsFunc
            = (dc, fromId)
                => from l in dc.NetworkLinkEntities
                   join m in dc.MemberEntities on l.toNetworkerId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where l.fromNetworkerId == fromId
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   &&
                   (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                   )
                   orderby u.firstName, u.lastName, m.profilePhotoId == null ? 1 : 0, u.createdTime, u.id
                   select m;

        private static readonly Func<ContactsDataContext, Guid, IQueryable<Guid>> GetFirstDegreeContacts
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId)
                => from l in dc.NetworkLinkEntities
                   join m in dc.MemberEntities on l.toNetworkerId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where l.fromNetworkerId == fromId
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   &&
                   (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                   )
                   orderby u.firstName, u.lastName, m.profilePhotoId == null ? 1 : 0, u.createdTime, u.id
                   select m.id);

        private static readonly Func<ContactsDataContext, Guid, string, Guid> GetFirstDegreeContactByEmailAddress
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, string emailAddress)
                => (from l in dc.NetworkLinkEntities
                    join m in dc.MemberEntities on l.toNetworkerId equals m.id
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    where l.fromNetworkerId == fromId
                    && u.emailAddress == emailAddress
                    && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    &&
                    (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                    )
                    orderby u.firstName, u.lastName, m.profilePhotoId == null ? 1 : 0, u.createdTime, u.id
                    select m.id).SingleOrDefault());

        private static readonly Func<ContactsDataContext, Guid, string, IQueryable<Guid>> GetFirstDegreeContactsByName
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, string nameStartsWith)
                => from l in dc.NetworkLinkEntities
                   join m in dc.MemberEntities on l.toNetworkerId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where l.fromNetworkerId == fromId
                   && SqlMethods.Like(u.firstName, nameStartsWith)
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   &&
                   (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                   )
                   orderby u.firstName, u.lastName, m.profilePhotoId == null ? 1 : 0, u.createdTime, u.id
                   select m.id);

        private static readonly Func<ContactsDataContext, Guid, IQueryable<Guid>> GetFirstDegreeContactsWithPhoto
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId)
                => from l in dc.NetworkLinkEntities
                   join m in dc.MemberEntities on l.toNetworkerId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where l.fromNetworkerId == fromId
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   &&
                   (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                   )
                   && m.profilePhotoId != null
                   orderby u.firstName, u.lastName, u.createdTime, u.id
                   select m.id);

        private static readonly Func<ContactsDataContext, Guid, PersonalVisibility, IQueryable<Guid>> GetFirstDegreeContactsWithVisibility
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, PersonalVisibility visibility)
                => from l in dc.NetworkLinkEntities
                   join m in dc.MemberEntities on l.toNetworkerId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where l.fromNetworkerId == fromId
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   &&
                   (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                   )
                   && (m.firstDegreeAccess & (int)visibility) == (int)visibility
                   orderby u.firstName, u.lastName, u.createdTime, u.id
                   select m.id);

        private static readonly Func<ContactsDataContext, Guid, IQueryable<Guid>> GetFirstDegreeContactsWithoutPhoto
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId)
                => from l in dc.NetworkLinkEntities
                   join m in dc.MemberEntities on l.toNetworkerId equals m.id
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where l.fromNetworkerId == fromId
                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   &&
                   (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                   )
                   && m.profilePhotoId == null
                   orderby u.firstName, u.lastName, u.createdTime, u.id
                   select m.id);

        private static readonly Func<ContactsDataContext, Guid, Guid, bool> AreFirstDegreeContacts
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, Guid toId)
                => (from l in dc.NetworkLinkEntities
                    join m in dc.MemberEntities on l.toNetworkerId equals m.id
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    where l.fromNetworkerId == fromId && l.toNetworkerId == toId
                    && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    &&
                    (
                        (m.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m.id select r).Any()
                    )
                    select m.id).Any());

        // Second degree contacts.

        private static readonly Func<ContactsDataContext, Guid, IQueryable<Tuple<Guid, Guid>>> GetSecondDegreeContacts
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId)
                => from l1 in dc.NetworkLinkEntities
                   join l2 in dc.NetworkLinkEntities on l1.toNetworkerId equals l2.fromNetworkerId
                   join m1 in dc.MemberEntities on l2.fromNetworkerId equals m1.id
                   join u1 in dc.RegisteredUserEntities on m1.id equals u1.id
                   join m2 in dc.MemberEntities on l2.toNetworkerId equals m2.id
                   join u2 in dc.RegisteredUserEntities on m2.id equals u2.id
                   where l1.fromNetworkerId == fromId
                   && (u1.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u1.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   &&
                   (
                        (m1.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m1.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m1.id select r).Any()
                   )
                   && (m1.firstDegreeAccess & (int)PersonalVisibility.FriendsList) == (int)PersonalVisibility.FriendsList
                   && (u2.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u2.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                   && (m2.secondDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                   && l2.toNetworkerId != fromId
                   && !(from l3 in dc.NetworkLinkEntities
                        where l3.fromNetworkerId == fromId
                        && l3.toNetworkerId == l2.toNetworkerId
                        select l3).Any()
                   && !(from m in dc.IgnoredNetworkMatchEntities
                        where m.ignoredId == l2.toNetworkerId
                        && m.ignorerId == fromId
                        select m).Any()

                   orderby u2.firstName, u2.lastName, m2.profilePhotoId == null ? 1 : 0, u2.createdTime, u2.id

                   select new Tuple<Guid, Guid>(l2.fromNetworkerId, l2.toNetworkerId));

        private static readonly Func<ContactsDataContext, Guid, Guid, bool> AreSecondDegreeContacts
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, Guid toId)
                => (from l1 in dc.NetworkLinkEntities
                    join l2 in dc.NetworkLinkEntities on l1.toNetworkerId equals l2.fromNetworkerId
                    join m1 in dc.MemberEntities on l2.fromNetworkerId equals m1.id
                    join u1 in dc.RegisteredUserEntities on m1.id equals u1.id
                    join m2 in dc.MemberEntities on l2.toNetworkerId equals m2.id
                    join u2 in dc.RegisteredUserEntities on m2.id equals u2.id
                    where l1.fromNetworkerId == fromId
                    && l2.toNetworkerId == toId
                    && (u1.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                    && (u1.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    &&
                    (
                        (m1.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                        ||
                        (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m1.id select r).Any()
                        ||
                        (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m1.id select r).Any()
                    )
                    && (m1.firstDegreeAccess & (int)PersonalVisibility.FriendsList) == (int)PersonalVisibility.FriendsList
                    && (u2.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                    && (u2.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && (m2.secondDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                    && l2.toNetworkerId != fromId
                    && !(from l3 in dc.NetworkLinkEntities
                         where l3.fromNetworkerId == fromId
                         && l3.toNetworkerId == l2.toNetworkerId
                         select l3).Any()
                    && !(from m in dc.IgnoredNetworkMatchEntities
                         where m.ignoredId == l2.toNetworkerId
                         && m.ignorerId == fromId
                         select m).Any()
                    select l2).Any());

        // Contacts.

        private static readonly Func<ContactsDataContext, Guid, DateTime, IQueryable<MemberEntity>> GetContactsFunc
            = (dc, fromId, minLastSentTime)
                => from m in dc.MemberEntities
                   join u in dc.RegisteredUserEntities on m.id equals u.id

                   let isRepresentative = (from r in dc.RepresentativeEntities
                                           where r.representeeId == fromId
                                           && r.representativeId == m.id
                                           select r).Any()

                   let isRepresentee = (from r in dc.RepresentativeEntities
                                        where r.representeeId == m.id
                                        && r.representativeId == fromId
                                        select r).Any()

                   let isFirstDegree = (from l in dc.NetworkLinkEntities
                                        join fm in dc.MemberEntities on l.toNetworkerId equals fm.id
                                        join fu in dc.RegisteredUserEntities on fm.id equals fu.id
                                        where l.fromNetworkerId == fromId
                                        && l.toNetworkerId == m.id
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
                                        select m.id).Any()

                   let isSecondDegree = (from l1 in dc.NetworkLinkEntities
                                         join l2 in dc.NetworkLinkEntities on l1.toNetworkerId equals l2.fromNetworkerId
                                         join m1 in dc.MemberEntities on l2.fromNetworkerId equals m1.id
                                         join u1 in dc.RegisteredUserEntities on m1.id equals u1.id
                                         join m2 in dc.MemberEntities on l2.toNetworkerId equals m2.id
                                         join u2 in dc.RegisteredUserEntities on m2.id equals u2.id
                                         where l1.fromNetworkerId == fromId
                                         && l2.toNetworkerId == m.id
                                         && (u1.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                         && (u1.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                         &&
                                         (
                                            (m1.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                            ||
                                            (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m1.id select r).Any()
                                            ||
                                            (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m1.id select r).Any()
                                         )
                                         && (m1.firstDegreeAccess & (int)PersonalVisibility.FriendsList) == (int)PersonalVisibility.FriendsList
                                         && (u2.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                         && (u2.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                         && (m2.secondDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                         && l2.toNetworkerId != fromId
                                         && !(from l3 in dc.NetworkLinkEntities
                                              where l3.fromNetworkerId == fromId
                                              && l3.toNetworkerId == l2.toNetworkerId
                                              select l3).Any()
                                         && !(from im in dc.IgnoredNetworkMatchEntities
                                              where im.ignoredId == l2.toNetworkerId
                                              && im.ignorerId == fromId
                                              select im).Any()
                                         select l2).Any()

                   let isPublic = (m.publicAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name

                   let hasNetworkingInvitation = (from ni in dc.NetworkInvitationEntities
                                                  join ur in dc.UserToUserRequestEntities on ni.id equals ur.id
                                                  where ni.inviteeId == fromId
                                                  && ni.inviterId == m.id
                                                  && ur.lastSentTime >= minLastSentTime
                                                  && ur.status == (byte)RequestStatus.Pending
                                                  select ni).Any()

                   let hasRepresentativeInvitation = (from ri in dc.RepresentativeInvitationEntities
                                                      join ur in dc.UserToUserRequestEntities on ri.id equals ur.id
                                                      where ri.inviteeId == fromId
                                                      && ri.inviterId == m.id
                                                      && ur.lastSentTime >= minLastSentTime
                                                      && ur.status == (byte)RequestStatus.Pending
                                                      select ri).Any()

                   where 
                   (
                        isFirstDegree
                        ||
                        isSecondDegree
                        ||
                        isPublic
                        ||
                        hasNetworkingInvitation
                        ||
                        hasRepresentativeInvitation
                   )

                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated

                   orderby u.firstName, u.lastName, m.profilePhotoId == null ? 1 : 0, u.createdTime, u.id

                   select m;

        private static readonly Func<ContactsDataContext, Guid, string, DateTime, IQueryable<Guid>> GetFilteredContacts
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, string toIds, DateTime minLastSentTime)
                => from m in dc.MemberEntities
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, toIds) on u.id equals i.value

                   let isSelf = fromId == m.id

                   let isRepresentative = (from r in dc.RepresentativeEntities
                                           where r.representeeId == fromId
                                           && r.representativeId == m.id
                                           select r).Any()

                   let isRepresentee = (from r in dc.RepresentativeEntities
                                        where r.representeeId == m.id
                                        && r.representativeId == fromId
                                        select r).Any()

                   let isFirstDegree = (from l in dc.NetworkLinkEntities
                                        join fm in dc.MemberEntities on l.toNetworkerId equals fm.id
                                        join fu in dc.RegisteredUserEntities on fm.id equals fu.id
                                        where l.fromNetworkerId == fromId
                                        && l.toNetworkerId == m.id
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
                                        select m.id).Any()

                   let isSecondDegree = (from l1 in dc.NetworkLinkEntities
                                         join l2 in dc.NetworkLinkEntities on l1.toNetworkerId equals l2.fromNetworkerId
                                         join m1 in dc.MemberEntities on l2.fromNetworkerId equals m1.id
                                         join u1 in dc.RegisteredUserEntities on m1.id equals u1.id
                                         join m2 in dc.MemberEntities on l2.toNetworkerId equals m2.id
                                         join u2 in dc.RegisteredUserEntities on m2.id equals u2.id
                                         where l1.fromNetworkerId == fromId
                                         && l2.toNetworkerId == m.id
                                         && (u1.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                         && (u1.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                         &&
                                         (
                                            (m1.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                            ||
                                            (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m1.id select r).Any()
                                            ||
                                            (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m1.id select r).Any()
                                         )
                                         && (m1.firstDegreeAccess & (int)PersonalVisibility.FriendsList) == (int)PersonalVisibility.FriendsList
                                         && (u2.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                         && (u2.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                         && (m2.secondDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                         && l2.toNetworkerId != fromId
                                         && !(from l3 in dc.NetworkLinkEntities
                                              where l3.fromNetworkerId == fromId
                                              && l3.toNetworkerId == l2.toNetworkerId
                                              select l3).Any()
                                         && !(from im in dc.IgnoredNetworkMatchEntities
                                              where im.ignoredId == l2.toNetworkerId
                                              && im.ignorerId == fromId
                                              select im).Any()
                                         select l2).Any()

                   let isPublic = (m.publicAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name

                   let hasNetworkingInvitation = (from ni in dc.NetworkInvitationEntities
                                                  join ur in dc.UserToUserRequestEntities on ni.id equals ur.id
                                                  where ni.inviteeId == fromId
                                                  && ni.inviterId == m.id
                                                  && ur.lastSentTime >= minLastSentTime
                                                  && ur.status == (byte)RequestStatus.Pending
                                                  select ni).Any()

                   let hasRepresentativeInvitation = (from ri in dc.RepresentativeInvitationEntities
                                                      join ur in dc.UserToUserRequestEntities on ri.id equals ur.id
                                                      where ri.inviteeId == fromId
                                                      && ri.inviterId == m.id
                                                      && ur.lastSentTime >= minLastSentTime
                                                      && ur.status == (byte)RequestStatus.Pending
                                                      select ri).Any()

                   where
                   (
                        isSelf
                        ||
                        isFirstDegree
                        ||
                        isSecondDegree
                        ||
                        isPublic
                        ||
                        hasNetworkingInvitation
                        ||
                        hasRepresentativeInvitation
                   )

                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated

                   orderby u.firstName, u.lastName, m.profilePhotoId == null ? 1 : 0, u.createdTime, u.id

                   select m.id);

        private static readonly Func<ContactsDataContext, Guid, string, DateTime, Guid> GetContactByEmailAddress
            = CompiledQuery.Compile((ContactsDataContext dc, Guid fromId, string emailAddress, DateTime minLastSentTime)
                => (from m in dc.MemberEntities
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    
                    let isRepresentative = (from r in dc.RepresentativeEntities
                                            where r.representeeId == fromId
                                            && r.representativeId == m.id
                                            select r).Any()
                                            
                    let isRepresentee = (from r in dc.RepresentativeEntities
                                         where r.representeeId == m.id
                                         && r.representativeId == fromId
                                         select r).Any()
                                         
                    let isFirstDegree = (from l in dc.NetworkLinkEntities
                                         join fm in dc.MemberEntities on l.toNetworkerId equals fm.id
                                         join fu in dc.RegisteredUserEntities on fm.id equals fu.id
                                         where l.fromNetworkerId == fromId
                                         && l.toNetworkerId == m.id
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
                                         select m.id).Any()

                   let isSecondDegree = (from l1 in dc.NetworkLinkEntities
                                         join l2 in dc.NetworkLinkEntities on l1.toNetworkerId equals l2.fromNetworkerId
                                         join m1 in dc.MemberEntities on l2.fromNetworkerId equals m1.id
                                         join u1 in dc.RegisteredUserEntities on m1.id equals u1.id
                                         join m2 in dc.MemberEntities on l2.toNetworkerId equals m2.id
                                         join u2 in dc.RegisteredUserEntities on m2.id equals u2.id
                                         where l1.fromNetworkerId == fromId
                                         && l2.toNetworkerId == m.id
                                         && (u1.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                         && (u1.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                         &&
                                         (
                                            (m1.firstDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                            ||
                                            (from r in dc.RepresentativeEntities where r.representativeId == fromId && r.representeeId == m1.id select r).Any()
                                            ||
                                            (from r in dc.RepresentativeEntities where r.representeeId == fromId && r.representativeId == m1.id select r).Any()
                                         )
                                         && (m1.firstDegreeAccess & (int)PersonalVisibility.FriendsList) == (int)PersonalVisibility.FriendsList
                                         && (u2.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                                         && (u2.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                                         && (m2.secondDegreeAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name
                                         && l2.toNetworkerId != fromId
                                         && !(from l3 in dc.NetworkLinkEntities
                                              where l3.fromNetworkerId == fromId
                                              && l3.toNetworkerId == l2.toNetworkerId
                                              select l3).Any()
                                         && !(from im in dc.IgnoredNetworkMatchEntities
                                              where im.ignoredId == l2.toNetworkerId
                                              && im.ignorerId == fromId
                                              select im).Any()
                                         select l2).Any()

                   let isPublic = (m.publicAccess & (int)PersonalVisibility.Name) == (int)PersonalVisibility.Name

                   let hasNetworkingInvitation = (from ni in dc.NetworkInvitationEntities
                                                  join ur in dc.UserToUserRequestEntities on ni.id equals ur.id
                                                  where ni.inviteeId == fromId
                                                  && ni.inviterId == m.id
                                                  && ur.lastSentTime >= minLastSentTime
                                                  && ur.status == (byte)RequestStatus.Pending
                                                  select ni).Any()

                   let hasRepresentativeInvitation = (from ri in dc.RepresentativeInvitationEntities
                                                      join ur in dc.UserToUserRequestEntities on ri.id equals ur.id
                                                      where ri.inviteeId == fromId
                                                      && ri.inviterId == m.id
                                                      && ur.lastSentTime >= minLastSentTime
                                                      && ur.status == (byte)RequestStatus.Pending
                                                      select ri).Any()

                   where m.RegisteredUserEntity.emailAddress == emailAddress
                   &&
                   (
                        isFirstDegree
                        ||
                        isSecondDegree
                        ||
                        isPublic
                        ||
                        hasNetworkingInvitation
                        ||
                        hasRepresentativeInvitation
                   )

                   && (u.flags & (int)UserFlags.Disabled) == (int)UserFlags.None
                   && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated

                   orderby u.firstName, u.lastName, m.profilePhotoId == null ? 1 : 0, u.createdTime, u.id
                  
                   select m.id).SingleOrDefault());

        // Contact views.

        private static readonly Func<ContactsDataContext, GetContactsArgs, ILocationQuery, IQueryable<PersonalView>> GetViewsQuery
            = CompiledQuery.Compile((ContactsDataContext dc, GetContactsArgs args, ILocationQuery locationQuery)
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

        private static readonly Func<ContactsDataContext, GetContactsArgs, IQueryable<Tuple<Guid, Tuple<PersonalContactDegree, PersonalContactDegree>>>> GetContactDegreesQuery
            = CompiledQuery.Compile((ContactsDataContext dc, GetContactsArgs args)
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

        public MemberContactsRepository(IDataContextFactory dataContextFactory, ILocationQuery locationQuery)
            : base(dataContextFactory)
        {
            _locationQuery = locationQuery;
        }

        Guid? IMemberContactsRepository.GetRepresentativeContact(Guid fromId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var id = GetRepresentativeContact(dc, fromId);
                return id == Guid.Empty ? (Guid?)null : id;
            }
        }

        IDictionary<Guid, Guid?> IMemberContactsRepository.GetRepresentativeContacts(IEnumerable<Guid> fromIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var ids = GetRepresentativeContacts(dc, new SplitList<Guid>(fromIds).ToString()).ToDictionary(x => x.Item1, x => x.Item2);
                return (from i in fromIds select new { RepresenteeId = i, RepresentativeId = ids.ContainsKey(i) ? ids[i] : (Guid?)null }).ToDictionary(x => x.RepresenteeId, x => x.RepresentativeId);
            }
        }

        IList<Guid> IMemberContactsRepository.GetRepresenteeContacts(Guid fromId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRepresenteeContacts(dc, fromId).ToList();
            }
        }

        IList<Guid> IMemberContactsRepository.GetRepresenteeContacts(Guid fromId, char nameStartsWith)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRepresenteeContactsByName(dc, fromId, nameStartsWith + "%").ToList();
            }
        }

        bool IMemberContactsRepository.AreFirstDegreeContacts(Guid fromId, Guid toId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return AreFirstDegreeContacts(dc, fromId, toId);
            }
        }

        Guid? IMemberContactsRepository.GetFirstDegreeContact(Guid fromId, string emailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var id = GetFirstDegreeContactByEmailAddress(dc, fromId, emailAddress);
                return id == Guid.Empty ? (Guid?)null : id;
            }
        }

        IList<Guid> IMemberContactsRepository.GetFirstDegreeContacts(Guid fromId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFirstDegreeContacts(dc, fromId).ToList();
            }
        }

        IList<Guid> IMemberContactsRepository.GetFirstDegreeContacts(Guid fromId, string fullName, bool exactMatch)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from m in GetFirstDegreeContactsFunc(dc, fromId).Where(GetNameWhereClause(fullName, exactMatch))
                        select m.id).ToList();
            }
        }

        IList<Guid> IMemberContactsRepository.GetFirstDegreeContacts(Guid fromId, char nameStartsWith)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFirstDegreeContactsByName(dc, fromId, nameStartsWith + "%").ToList();
            }
        }

        IList<Guid> IMemberContactsRepository.GetFirstDegreeContacts(Guid fromId, bool withPhoto)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return withPhoto
                    ? GetFirstDegreeContactsWithPhoto(dc, fromId).ToList()
                    : GetFirstDegreeContactsWithoutPhoto(dc, fromId).ToList();
            }
        }

        IList<Guid> IMemberContactsRepository.GetFirstDegreeContacts(Guid fromId, PersonalVisibility visibility)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFirstDegreeContactsWithVisibility(dc, fromId, visibility).ToList();
            }
        }

        bool IMemberContactsRepository.AreSecondDegreeContacts(Guid fromId, Guid toId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return AreSecondDegreeContacts(dc, fromId, toId);
            }
        }

        IDictionary<Guid, IList<Guid>> IMemberContactsRepository.GetSecondDegreesContacts(Guid fromId, int minFirstDegreeContacts)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // Select all second degree contacts who are not also first degree contacts or the 'from' themselves.

                var secondDegreeContacts = GetSecondDegreeContacts(dc, fromId).ToArray();

                // Group the first degree contacts, which is done in memory to avoid multiple database hits.

                var contacts = from l in secondDegreeContacts
                               group l by l.Item2 into g
                               select new
                               {
                                   SecondDegreeContact = g.Key,
                                   FirstDegreeContacts = g.Select(l => l.Item1)
                               };

                // Filter.

                contacts = from c in contacts
                           where c.FirstDegreeContacts.Count() >= minFirstDegreeContacts
                           select c;

                // Order by total first degree contacts.

                return contacts.ToDictionary(c => c.SecondDegreeContact, c => (IList<Guid>)c.FirstDegreeContacts.ToList());
            }
        }

        Guid? IMemberContactsRepository.GetContact(Guid fromId, string emailAddress, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var id = GetContactByEmailAddress(dc, fromId, emailAddress, minLastSentTime);
                return id == Guid.Empty ? (Guid?) null : id;
            }
        }

        IList<Guid> IMemberContactsRepository.GetContacts(IEnumerable<Guid> toIds, string fullName, bool exactMatch)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var options = new DataLoadOptions();
                options.LoadWith<MemberEntity>(m => m.RegisteredUserEntity);
                dc.LoadOptions = options;

                // Generating the where clause on the fly so don't use compiled query.

                var query = from m in dc.MemberEntities
                            join u in dc.RegisteredUserEntities on m.id equals u.id
                            join i in dc.SplitGuids(SplitList<Guid>.Delimiter, new SplitList<Guid>(toIds).ToString()) on m.id equals i.value
                            select m;

                return (from m in query.Where(GetNameWhereClause(fullName, exactMatch))
                        select m.id).ToList();
            }
        }

        IList<Guid> IMemberContactsRepository.GetContacts(Guid fromId, IEnumerable<Guid> toIds, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredContacts(dc, fromId, new SplitList<Guid>(toIds).ToString(), minLastSentTime).ToList();
            }
        }

        IList<Guid> IMemberContactsRepository.GetContacts(Guid fromId, string fullName, bool exactMatch, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from m in GetContactsFunc(dc, fromId, minLastSentTime).Where(GetNameWhereClause(fullName, exactMatch))
                        select m.id).ToList();
            }
        }

        private IQueryable<PersonalView> GetViews(ContactsDataContext dc, Guid fromId, IEnumerable<Guid> toIds, DateTime minSentTime)
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

        private static IEnumerable<PersonalView> GetViews(ContactsDataContext dc, Guid fromId, IEnumerable<Member> tos, DateTime minSentTime)
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

        private static Expression<Func<MemberEntity, bool>> GetNameWhereClause(string fullName, bool exactMatch)
        {
            // Match on first and last names.

            Expression<Func<MemberEntity, bool>> whereClause;
            if (exactMatch)
                whereClause = m => m.RegisteredUserEntity.firstName == fullName
                    || m.RegisteredUserEntity.lastName == fullName;
            else
                whereClause = m => SqlMethods.Like(m.RegisteredUserEntity.firstName, fullName + "%")
                    || SqlMethods.Like(m.RegisteredUserEntity.lastName, fullName + "%");

            // Split the name into parts.

            var parts = fullName.Split(new[] { ' ' });
            if (parts.Length > 1)
            {
                for (var index = 1; index < parts.Length; ++index)
                {
                    var firstName = string.Join(" ", parts, 0, index);
                    var lastName = string.Join(" ", parts, index, parts.Length - index);

                    whereClause = exactMatch
                        ? whereClause.Or(m => m.RegisteredUserEntity.firstName == firstName && m.RegisteredUserEntity.lastName == lastName)
                        : whereClause.Or(m => SqlMethods.Like(m.RegisteredUserEntity.firstName, firstName + "%") && SqlMethods.Like(m.RegisteredUserEntity.lastName, lastName + "%"));
                }
            }

            return whereClause;
        }

        private ContactsDataContext CreateContext()
        {
            return CreateContext(c => new ContactsDataContext(c));
        }
    }
}
