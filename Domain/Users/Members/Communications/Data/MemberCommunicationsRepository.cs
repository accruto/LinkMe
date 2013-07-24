using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Users.Members.Communications.Data
{
    internal enum UserFlags
    {
        Disabled = 0x04,
        Activated = 0x20,
    }

    public class MemberCommunicationsRepository
        : Repository, IMemberCommunicationsRepository
    {
        private static readonly Func<CommunicationsDataContext, Guid, DateTime, Range, IQueryable<Guid>> GetNotSentMemberIdsTakeQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime createdBefore, Range range)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    let lastSentTime = (from s in dc.CommunicationSettingEntities
                                        join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                                        where s.userId == m.id
                                        && d.definitionId == definitionId
                                        select d.lastSentTime).SingleOrDefault()
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     join fs in dc.CommunicationSettingEntities on fc.settingsId equals fs.id
                                     where fd.id == definitionId
                                     && fs.userId == u.id
                                     select fc.frequency).SingleOrDefault()
                    where !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int) UserFlags.Activated
                    && u.createdTime < createdBefore
                    && Equals(lastSentTime, null)
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id).Take(range.Take.Value));

        private static readonly Func<CommunicationsDataContext, Guid, DateTime, IQueryable<Guid>> GetNotSentMemberIdsQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime createdBefore)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    let lastSentTime = (from s in dc.CommunicationSettingEntities
                                        join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                                        where s.userId == m.id
                                        && d.definitionId == definitionId
                                        select d.lastSentTime).SingleOrDefault()
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     join fs in dc.CommunicationSettingEntities on fc.settingsId equals fs.id
                                     where fd.id == definitionId
                                     && fs.userId == u.id
                                     select fc.frequency).SingleOrDefault()
                    where !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && u.createdTime < createdBefore
                    && Equals(lastSentTime, null)
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id));

        private static readonly Func<CommunicationsDataContext, Guid, DateTime, Range, IQueryable<Guid>> GetNotSentMemberIdsSkipTakeQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime createdBefore, Range range)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    let lastSentTime = (from s in dc.CommunicationSettingEntities
                                        join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                                        where s.userId == m.id
                                        && d.definitionId == definitionId
                                        select d.lastSentTime).SingleOrDefault()
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     join fs in dc.CommunicationSettingEntities on fc.settingsId equals fs.id
                                     where fd.id == definitionId
                                     && fs.userId == u.id
                                     select fc.frequency).SingleOrDefault()
                    where !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && u.createdTime < createdBefore
                    && Equals(lastSentTime, null)
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id).Skip(range.Skip).Take(range.Take.Value));

        private static readonly Func<CommunicationsDataContext, Guid, DateTime, Range, IQueryable<Guid>> GetNotSentMemberIdsSkipQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime createdBefore, Range range)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    let lastSentTime = (from s in dc.CommunicationSettingEntities
                                        join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                                        where s.userId == m.id
                                        && d.definitionId == definitionId
                                        select d.lastSentTime).SingleOrDefault()
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     join fs in dc.CommunicationSettingEntities on fc.settingsId equals fs.id
                                     where fd.id == definitionId
                                     && fs.userId == u.id
                                     select fc.frequency).SingleOrDefault()
                    where !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && u.createdTime < createdBefore
                    && Equals(lastSentTime, null)
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id).Skip(range.Skip));

        private static readonly Func<CommunicationsDataContext, Guid, DateTime, Range, IQueryable<Guid>> GetSentMemberIdsTakeQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime lastSentBefore, Range range)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    join s in dc.CommunicationSettingEntities on u.id equals s.userId
                    join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     where fd.id == definitionId
                                     && fc.settingsId == s.id
                                     select fc.frequency).SingleOrDefault()
                    where d.definitionId == definitionId
                    && !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && d.lastSentTime < lastSentBefore
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id).Take(range.Take.Value));

        private static readonly Func<CommunicationsDataContext, Guid, DateTime, IQueryable<Guid>> GetSentMemberIdsQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime lastSentBefore)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    join s in dc.CommunicationSettingEntities on u.id equals s.userId
                    join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     where fd.id == definitionId
                                     && fc.settingsId == s.id
                                     select fc.frequency).SingleOrDefault()
                    where d.definitionId == definitionId
                    && !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && d.lastSentTime < lastSentBefore
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id));

        private static readonly Func<CommunicationsDataContext, Guid, DateTime, Range, IQueryable<Guid>> GetSentMemberIdsSkipTakeQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime lastSentBefore, Range range)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    join s in dc.CommunicationSettingEntities on u.id equals s.userId
                    join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     where fd.id == definitionId
                                     && fc.settingsId == s.id
                                     select fc.frequency).SingleOrDefault()
                    where d.definitionId == definitionId
                    && !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && d.lastSentTime < lastSentBefore
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id).Skip(range.Skip).Take(range.Take.Value));

        private static readonly Func<CommunicationsDataContext, Guid, DateTime, Range, IQueryable<Guid>> GetSentMemberIdsSkipQuery
            = CompiledQuery.Compile((CommunicationsDataContext dc, Guid definitionId, DateTime lastSentBefore, Range range)
                => (from u in dc.RegisteredUserEntities
                    join m in dc.MemberEntities on u.id equals m.id
                    join s in dc.CommunicationSettingEntities on u.id equals s.userId
                    join d in dc.CommunicationDefinitionSettingEntities on s.id equals d.settingsId
                    let frequency = (from fd in dc.CommunicationDefinitionEntities
                                     join fc in dc.CommunicationCategorySettingEntities on fd.categoryId equals fc.categoryId
                                     where fd.id == definitionId
                                     && fc.settingsId == s.id
                                     select fc.frequency).SingleOrDefault()
                    where d.definitionId == definitionId
                    && !(from c in dc.CommunityMemberEntities where c.id == m.id select c).Any()
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    && d.lastSentTime < lastSentBefore
                    && (Equals(frequency, null) || frequency != (byte)Frequency.Never)
                    orderby u.createdTime, u.emailAddress
                    select m.id).Skip(range.Skip));

        public MemberCommunicationsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Guid> IMemberCommunicationsRepository.GetNotSentMemberIds(Guid definitionId, DateTime createdBefore, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (range.Skip == 0)
                    return range.Take != null
                        ? GetNotSentMemberIdsTakeQuery(dc, definitionId, createdBefore, range).ToList()
                        : GetNotSentMemberIdsQuery(dc, definitionId, createdBefore).ToList();
                return range.Take != null
                    ? GetNotSentMemberIdsSkipTakeQuery(dc, definitionId, createdBefore, range).ToList()
                    : GetNotSentMemberIdsSkipQuery(dc, definitionId, createdBefore, range).ToList();
            }
        }

        IList<Guid> IMemberCommunicationsRepository.GetSentMemberIds(Guid definitionId, DateTime lastSentBefore, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (range.Skip == 0)
                    return range.Take != null
                        ? GetSentMemberIdsTakeQuery(dc, definitionId, lastSentBefore, range).ToList()
                        : GetSentMemberIdsQuery(dc, definitionId, lastSentBefore).ToList();
                return range.Take != null
                    ? GetSentMemberIdsSkipTakeQuery(dc, definitionId, lastSentBefore, range).ToList()
                    : GetSentMemberIdsSkipQuery(dc, definitionId, lastSentBefore, range).ToList();
            }
        }

        private CommunicationsDataContext CreateContext()
        {
            return CreateContext(c => new CommunicationsDataContext(c));
        }
    }
}