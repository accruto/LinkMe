using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Users.Members.Data
{
    public class MembersRepository
        : Repository, IMembersRepository
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IAffiliationItemsFactory _affiliationItemsFactory;

        private static readonly DataLoadOptions MemberLoadOptions = DataOptions.CreateLoadOptions<MemberEntity, AddressEntity>(m => m.AddressEntity, a => a.LocationReferenceEntity);

        private static readonly Func<MembersDataContext, Guid, MemberEntity> GetMemberEntityQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid id)
                => (from m in dc.MemberEntities
                    where m.id == id
                    select m).SingleOrDefault());

        private static readonly Func<MembersDataContext, Guid, ILocationQuery, Member> GetMemberQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid id, ILocationQuery locationQuery)
                => (from m in dc.MemberEntities
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                    from tc in tCommunityMemberEntities.DefaultIfEmpty()
                    where m.id == id
                    select m.Map(u, tc, locationQuery)).SingleOrDefault());

        private static readonly Func<MembersDataContext, string, ILocationQuery, Member> GetMemberByEmailAddressQuery
            = CompiledQuery.Compile((MembersDataContext dc, string emailAddress, ILocationQuery locationQuery)
                => (from m in dc.MemberEntities
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                    from tc in tCommunityMemberEntities.DefaultIfEmpty()
                    where u.emailAddress == emailAddress
                    select m.Map(u, tc, locationQuery)).SingleOrDefault());

        private static readonly Func<MembersDataContext, string, ILocationQuery, IQueryable<Member>> GetMembersQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ids, ILocationQuery locationQuery)
                => from m in dc.MemberEntities
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                   from tc in tCommunityMemberEntities.DefaultIfEmpty()
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on m.id equals i.value
                   select m.Map(u, tc, locationQuery));

        private static readonly Func<MembersDataContext, string, ILocationQuery, IQueryable<Member>> GetMembersByNameQuery
            = CompiledQuery.Compile((MembersDataContext dc, string fullName, ILocationQuery locationQuery)
                => from m in dc.MemberEntities
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                   from tc in tCommunityMemberEntities. DefaultIfEmpty()
                   where u.firstName + ' '  + u.lastName == fullName
                   select m.Map(u, tc, locationQuery));

        private static readonly Func<MembersDataContext, string, Range, ILocationQuery, IQueryable<Member>> GetMembersSkipTakeQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ids, Range range, ILocationQuery locationQuery)
                => (from m in dc.MemberEntities
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                    from tc in tCommunityMemberEntities.DefaultIfEmpty()
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on m.id equals i.value
                    select new { m, u, tc }).Skip(range.Skip).Take(range.Take.Value).Select(m => m.m.Map(m.u, m.tc, locationQuery)));

        private static readonly Func<MembersDataContext, string, Range, ILocationQuery, IQueryable<Member>> GetMembersTakeQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ids, Range range, ILocationQuery locationQuery)
                => (from m in dc.MemberEntities
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                    from tc in tCommunityMemberEntities.DefaultIfEmpty()
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on m.id equals i.value
                    select new { m, u, tc }).Take(range.Take.Value).Select(m => m.m.Map(m.u, m.tc, locationQuery)));

        private static readonly Func<MembersDataContext, string, Range, ILocationQuery, IQueryable<Member>> GetMembersSkipQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ids, Range range, ILocationQuery locationQuery)
                => (from m in dc.MemberEntities
                    join u in dc.RegisteredUserEntities on m.id equals u.id
                    join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                    from tc in tCommunityMemberEntities.DefaultIfEmpty()
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on m.id equals i.value
                    select new { m, u, tc }).Skip(range.Skip).Select(m => m.m.Map(m.u, m.tc, locationQuery)));

        private static readonly Func<MembersDataContext, IQueryable<Guid>> GetActiveMemberIds
            = CompiledQuery.Compile((MembersDataContext dc)
                => from m in dc.MemberEntities
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   where (u.flags & (int) UserFlags.Disabled) == 0
                   && (u.flags & (int) UserFlags.Activated) == 1
                   select m.id);

        private static readonly Func<MembersDataContext, string, IQueryable<string>> GetFullNames
            = CompiledQuery.Compile((MembersDataContext dc, string ids)
                => from m in dc.MemberEntities
                   join u in dc.RegisteredUserEntities on m.id equals u.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on u.id equals i.value
                   select u.firstName + " " + u.lastName);

        private static readonly Func<MembersDataContext, Guid, CommunityMemberEntity> GetCommunityMemberEntity
            = CompiledQuery.Compile((MembersDataContext dc, Guid id)
                => (from m in dc.CommunityMemberEntities
                    where m.id == id
                    select m).SingleOrDefault());

        private static readonly Func<MembersDataContext, Guid, Guid> GetAffiliateId
            = CompiledQuery.Compile((MembersDataContext dc, Guid id)
                => (from m in dc.CommunityMemberEntities
                    where m.id == id
                    select m.primaryCommunityId).SingleOrDefault());

        private static readonly Func<MembersDataContext, Guid, Guid, IQueryable<CommunityMemberDataEntity>> GetDataEntities
            = CompiledQuery.Compile((MembersDataContext dc, Guid memberId, Guid affiliateId)
                => from d in dc.CommunityMemberDataEntities
                   where d.memberId == memberId
                   && d.id == affiliateId
                   select d);

        private static readonly Func<MembersDataContext, Guid, Guid, IQueryable<CommunityMemberDataEntity>> GetItems
            = CompiledQuery.Compile((MembersDataContext dc, Guid memberId, Guid affiliateId)
                => from d in dc.CommunityMemberDataEntities
                   where d.memberId == memberId
                   && d.id == affiliateId
                   select d);

        public MembersRepository(IDataContextFactory dataContextFactory, ILocationQuery locationQuery, IAffiliationItemsFactory affiliationItemsFactory)
            : base(dataContextFactory)
        {
            _locationQuery = locationQuery;
            _affiliationItemsFactory = affiliationItemsFactory;
        }

        void IMembersRepository.CreateMember(Member member)
        {
            try
            {
                using (var dc = CreateContext())
                {
                    dc.MemberEntities.InsertOnSubmit(member.Map());
                    dc.SubmitChanges();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.RegisteredUserMemberEmailAddress'"))
                    throw new DuplicateUserException();
                throw;
            }
        }

        void IMembersRepository.UpdateMember(Member member)
        {
            try
            {
                using (var dc = CreateContext())
                {
                    var entity = GetMemberEntity(dc, member.Id);
                    if (entity != null)
                    {
                        // Delete the location if needed.

                        if (member.Address.Location == null)
                        {
                            if (entity.AddressEntity.LocationReferenceEntity != null)
                            {
                                dc.LocationReferenceEntities.DeleteOnSubmit(entity.AddressEntity.LocationReferenceEntity);
                                entity.AddressEntity.LocationReferenceEntity = null;
                            }
                        }

                        member.MapTo(entity);
                        dc.SubmitChanges();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.RegisteredUserMemberEmailAddress'"))
                    throw new DuplicateUserException();
                throw;
            }
        }

        Member IMembersRepository.GetMember(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMember(dc, id);
            }
        }

        Member IMembersRepository.GetMember(string emailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberByEmailAddress(dc, emailAddress);
            }
        }

        IList<Member> IMembersRepository.GetMembers(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMembers(dc, ids).ToList();
            }
        }

        IList<Member> IMembersRepository.GetMembers(string fullName)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMembers(dc, fullName).ToList();
            }
        }

        IList<Member> IMembersRepository.GetMembers(IEnumerable<Guid> ids, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMembers(dc, ids, range).ToList();
            }
        }

        IList<Member> IMembersRepository.GetMembers(IEnumerable<string> emailAddresses)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // Just do this for now.

                return (from e in emailAddresses select GetMemberByEmailAddress(dc, e)).ToList();
            }
        }

        IList<Guid> IMembersRepository.GetActiveMemberIds()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetActiveMemberIds(dc).ToList();
            }
        }

        IList<string> IMembersRepository.GetFullNames(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFullNames(dc, new SplitList<Guid>(ids).ToString()).ToList();
            }
        }

        void IMembersRepository.SetAffiliation(Guid memberId, Guid? affiliateId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetCommunityMemberEntity(dc, memberId);

                // If there is no entity then add it if needed.

                if (entity == null)
                {
                    if (affiliateId != null)
                        dc.CommunityMemberEntities.InsertOnSubmit(Mappings.MapMember(memberId, affiliateId.Value));
                }
                else
                {
                    // If the member does not have a community then delete the entity.

                    if (affiliateId == null)
                        dc.CommunityMemberEntities.DeleteOnSubmit(entity);
                    else
                        entity.primaryCommunityId = affiliateId.Value;
                }

                dc.SubmitChanges();
            }
        }

        Guid? IMembersRepository.GetAffiliateId(Guid memberId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var affiliateId = GetAffiliateId(dc, memberId);
                return affiliateId == Guid.Empty ? (Guid?) null : affiliateId;
            }
        }

        void IMembersRepository.SetAffiliationItems(Guid memberId, Guid affiliateId, AffiliationItems items)
        {
            using (var dc = CreateContext())
            {
                // Delete all previous data.

                var existingDataEntities = GetDataEntities(dc, memberId, affiliateId);
                dc.CommunityMemberDataEntities.DeleteAllOnSubmit(existingDataEntities);

                // Add the new ones in.

                var convertedItems = _affiliationItemsFactory.ConvertAffiliationItems(affiliateId, items);
                if (convertedItems != null)
                {
                    dc.CommunityMemberDataEntities.InsertAllOnSubmit(_affiliationItemsFactory.ConvertAffiliationItems(affiliateId, items).Map(memberId, affiliateId));
                    dc.SubmitChanges();
                }
            }
        }

        AffiliationItems IMembersRepository.GetAffiliationItems(Guid memberId, Guid affiliateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return _affiliationItemsFactory.ConvertAffiliationItems(affiliateId, GetItems(dc, memberId, affiliateId).ToDictionary(e => e.name, e => e.value as string));
            }
        }

        private static MemberEntity GetMemberEntity(MembersDataContext dc, Guid id)
        {
            dc.LoadOptions = MemberLoadOptions;
            return GetMemberEntityQuery(dc, id);
        }

        private Member GetMember(MembersDataContext dc, Guid id)
        {
            dc.LoadOptions = MemberLoadOptions;
            return GetMemberQuery(dc, id, _locationQuery);
        }

        private Member GetMemberByEmailAddress(MembersDataContext dc, string emailAddress)
        {
            dc.LoadOptions = MemberLoadOptions;
            return GetMemberByEmailAddressQuery(dc, emailAddress, _locationQuery);
        }

        private IEnumerable<Member> GetMembers(MembersDataContext dc, IEnumerable<Guid> ids)
        {
            dc.LoadOptions = MemberLoadOptions;
            return GetMembersQuery(dc, new SplitList<Guid>(ids.Distinct()).ToString(), _locationQuery);
        }

        private IEnumerable<Member> GetMembers(MembersDataContext dc, string fullName)
        {
            dc.LoadOptions = MemberLoadOptions;
            return GetMembersByNameQuery(dc, fullName, _locationQuery);
        }

        private IEnumerable<Member> GetMembers(MembersDataContext dc, IEnumerable<Guid> ids, Range range)
        {
            dc.LoadOptions = MemberLoadOptions;

            ids = ids.Distinct();
            if (range.Skip == 0)
                return range.Take != null
                    ? GetMembersTakeQuery(dc, new SplitList<Guid>(ids).ToString(), range, _locationQuery)
                    : GetMembersQuery(dc, new SplitList<Guid>(ids).ToString(), _locationQuery);
            return range.Take != null
                ? GetMembersSkipTakeQuery(dc, new SplitList<Guid>(ids).ToString(), range, _locationQuery)
                : GetMembersSkipQuery(dc, new SplitList<Guid>(ids).ToString(), range, _locationQuery);
        }

        private MembersDataContext CreateContext()
        {
            return CreateContext(c => new MembersDataContext(c));
        }
    }
}
