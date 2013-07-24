using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Invitations;
using LinkMe.Domain.Roles.Invitations.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Representatives.Data
{
    public class RepresentativesRepository
        : Repository, IRepresentativesRepository
    {
        private struct GetInvitationsByInviteeArgs
        {
            public Guid InviteeId;
            public string InviteeEmailAddress;
            public DateTime MinLastSentTime;
        }

        private static readonly DataLoadOptions InvitationLoadOptions = DataOptions.CreateLoadOptions<RepresentativeInvitationEntity>(i => i.UserToUserRequestEntity);

        private static readonly Func<RepresentativesDataContext, Guid, Guid> GetRepresentativeId
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid representeeId)
                => (from r in dc.RepresentativeEntities
                    where r.representeeId == representeeId
                    select r.representativeId).SingleOrDefault());

        private static readonly Func<RepresentativesDataContext, string, IQueryable<Tuple<Guid, Guid>>> GetRepresentativeIds
            = CompiledQuery.Compile((RepresentativesDataContext dc, string representeeIds)
                => from r in dc.RepresentativeEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, representeeIds) on r.representeeId equals i.value
                   select new Tuple<Guid, Guid>(r.representeeId, r.representativeId));

        private static readonly Func<RepresentativesDataContext, Guid, IQueryable<Guid>> GetRepresenteeIds
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid representativeId)
                => from r in dc.RepresentativeEntities
                   where r.representativeId == representativeId
                   select r.representeeId);

        private static readonly Func<RepresentativesDataContext, Guid, string, IQueryable<Guid>> GetFilteredRepresenteeIds
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid representativeId, string representeeIds)
                => from r in dc.RepresentativeEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, representeeIds) on r.representeeId equals i.value
                   where r.representativeId == representativeId
                   select r.representeeId);

        private static readonly Func<RepresentativesDataContext, Guid, RepresentativeEntity> GetRepresentativeEntity
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid representateeId)
                => (from r in dc.RepresentativeEntities
                    where r.representeeId == representateeId
                    select r).SingleOrDefault());

        private static readonly Func<RepresentativesDataContext, Guid, RepresentativeInvitationEntity> GetInvitationEntity
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid id)
                => (from i in dc.RepresentativeInvitationEntities
                    where i.id == id
                    select i).SingleOrDefault());

        private static readonly Func<RepresentativesDataContext, Guid, IInvitationFactory, Invitation> GetInvitationQuery
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid id, IInvitationFactory factory)
                => (from i in dc.RepresentativeInvitationEntities
                    where i.id == id
                    select i.Map(factory)).SingleOrDefault());

        private static readonly Func<RepresentativesDataContext, Guid, string, IInvitationFactory, IQueryable<Invitation>> GetInvitationsByEmailAddressQuery
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid inviterId, string inviteeEmailAddress, IInvitationFactory factory)
                => from i in dc.RepresentativeInvitationEntities
                   where i.inviterId == inviterId
                   && i.inviteeEmailAddress == inviteeEmailAddress
                   select i.Map(factory));

        private static readonly Func<RepresentativesDataContext, Guid, Guid, IInvitationFactory, IQueryable<Invitation>> GetInvitationsQuery
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid inviterId, Guid inviteeId, IInvitationFactory factory)
                => from i in dc.RepresentativeInvitationEntities
                   where i.inviterId == inviterId
                   && i.inviteeId == inviteeId
                   select i.Map(factory));

        private static readonly Func<RepresentativesDataContext, Guid, DateTime, IInvitationFactory, IQueryable<Invitation>> GetInvitationsByInviterQuery
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid inviterId, DateTime minLastSentTime, IInvitationFactory factory)
                => from i in dc.RepresentativeInvitationEntities
                   where i.inviterId == inviterId
                   && i.UserToUserRequestEntity.status == (byte)RequestStatus.Pending
                   && i.UserToUserRequestEntity.lastSentTime > minLastSentTime
                   orderby i.UserToUserRequestEntity.lastSentTime
                   select i.Map(factory));

        private static readonly Func<RepresentativesDataContext, GetInvitationsByInviteeArgs, IInvitationFactory, IQueryable<Invitation>> GetInvitationsByInviteeQuery
            = CompiledQuery.Compile((RepresentativesDataContext dc, GetInvitationsByInviteeArgs args, IInvitationFactory factory)
                => from i in dc.RepresentativeInvitationEntities
                   where (i.inviteeId == args.InviteeId || i.inviteeEmailAddress == args.InviteeEmailAddress)
                   && i.UserToUserRequestEntity.status == (byte)RequestStatus.Pending
                   && i.UserToUserRequestEntity.lastSentTime > args.MinLastSentTime
                   orderby i.UserToUserRequestEntity.lastSentTime
                   select i.Map(factory));

        private static readonly Func<RepresentativesDataContext, Guid, DateTimeRange, int> GetInvitationCount
            = CompiledQuery.Compile((RepresentativesDataContext dc, Guid inviterId, DateTimeRange dateTimeRange)
                => (from i in dc.RepresentativeInvitationEntities
                    where i.inviterId == inviterId
                    && i.UserToUserRequestEntity.firstSentTime >= dateTimeRange.Start && i.UserToUserRequestEntity.firstSentTime < dateTimeRange.End
                    select i).Count());

        public RepresentativesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IRepresentativesRepository.CreateRepresentative(Guid representeeId, Guid representativeId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetRepresentativeEntity(dc, representeeId);
                if (entity != null)
                    entity.representativeId = representativeId;
                else
                    dc.RepresentativeEntities.InsertOnSubmit(new RepresentativeEntity { representeeId = representeeId, representativeId = representativeId });
                dc.SubmitChanges();
            }
        }

        void IRepresentativesRepository.DeleteRepresentative(Guid representeeId, Guid representativeId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetRepresentativeEntity(dc, representeeId);
                if (entity != null)
                    dc.RepresentativeEntities.DeleteOnSubmit(entity);
                dc.SubmitChanges();
            }
        }

        Guid? IRepresentativesRepository.GetRepresentativeId(Guid representeeId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var representativeId = GetRepresentativeId(dc, representeeId);
                return representativeId == Guid.Empty ? (Guid?)null : representativeId;
            }
        }

        IDictionary<Guid, Guid> IRepresentativesRepository.GetRepresentativeIds(IEnumerable<Guid> representeeIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRepresentativeIds(dc, new SplitList<Guid>(representeeIds).ToString()).ToDictionary(r => r.Item1, r => r.Item2);
            }
        }

        IList<Guid> IRepresentativesRepository.GetRepresenteeIds(Guid representativeId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRepresenteeIds(dc, representativeId).ToList();
            }
        }

        IList<Guid> IRepresentativesRepository.GetRepresenteeIds(Guid representativeId, IEnumerable<Guid> representeeIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredRepresenteeIds(dc, representativeId, new SplitList<Guid>(representeeIds).ToString()).ToList();
            }
        }

        void IInvitationsRepository<RepresentativeInvitation>.CreateInvitation(RepresentativeInvitation invitation)
        {
            using (var dc = CreateContext())
            {
                dc.RepresentativeInvitationEntities.InsertOnSubmit(invitation.Map());
                dc.SubmitChanges();
            }
        }

        void IInvitationsRepository<RepresentativeInvitation>.UpdateInvitation(RepresentativeInvitation invitation)
        {
            using (var dc = CreateContext())
            {
                var entity = GetInvitationEntity(dc, invitation.Id);
                if (entity != null)
                {
                    invitation.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        TRepresentativeInvitation IInvitationsRepository<RepresentativeInvitation>.GetInvitation<TRepresentativeInvitation>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitation<TRepresentativeInvitation>(dc, id);
            }
        }

        IList<TRepresentativeInvitation> IInvitationsRepository<RepresentativeInvitation>.GetInvitations<TRepresentativeInvitation>(Guid inviterId, string inviteeEmailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TRepresentativeInvitation>(dc, inviterId, inviteeEmailAddress).ToList();
            }
        }

        IList<TRepresentativeInvitation> IInvitationsRepository<RepresentativeInvitation>.GetInvitations<TRepresentativeInvitation>(Guid inviterId, Guid inviteeId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TRepresentativeInvitation>(dc, inviterId, inviteeId).ToList();
            }
        }

        IList<TRepresentativeInvitation> IInvitationsRepository<RepresentativeInvitation>.GetInvitations<TRepresentativeInvitation>(Guid inviterId, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TRepresentativeInvitation>(dc, inviterId, minLastSentTime).ToList();
            }
        }

        IList<TRepresentativeInvitation> IInvitationsRepository<RepresentativeInvitation>.GetInvitations<TRepresentativeInvitation>(Guid inviteeId, string inviteeEmailAddress, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TRepresentativeInvitation>(dc, inviteeId, inviteeEmailAddress, minLastSentTime).ToList();
            }
        }

        int IInvitationsRepository<RepresentativeInvitation>.GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitationCount(dc, inviterId, dateTimeRange);
            }
        }

        private static TInvitation GetInvitation<TInvitation>(RepresentativesDataContext dc, Guid id)
            where TInvitation : RepresentativeInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationQuery(dc, id, new InvitationFactory<TInvitation>()) as TInvitation;
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(RepresentativesDataContext dc, Guid inviterId, string inviteeEmailAddress)
            where TInvitation : RepresentativeInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationsByEmailAddressQuery(dc, inviterId, inviteeEmailAddress, new InvitationFactory<TInvitation>()).Cast<TInvitation>();
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(RepresentativesDataContext dc, Guid inviterId, Guid inviteeId)
            where TInvitation : RepresentativeInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationsQuery(dc, inviterId, inviteeId, new InvitationFactory<TInvitation>()).Cast<TInvitation>();
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(RepresentativesDataContext dc, Guid inviterId, DateTime minLastSentTime)
            where TInvitation : RepresentativeInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationsByInviterQuery(dc, inviterId, minLastSentTime, new InvitationFactory<TInvitation>()).Cast<TInvitation>();
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(RepresentativesDataContext dc, Guid inviteeId, string inviteeEmailAddress, DateTime minLastSentTime)
            where TInvitation : RepresentativeInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            var args = new GetInvitationsByInviteeArgs
            {
                InviteeEmailAddress = inviteeEmailAddress,
                InviteeId = inviteeId,
                MinLastSentTime = minLastSentTime
            };
            return GetInvitationsByInviteeQuery(dc, args, new InvitationFactory<TInvitation>()).Cast<TInvitation>();
        }

        private RepresentativesDataContext CreateContext()
        {
            return CreateContext(c => new RepresentativesDataContext(c));
        }
    }
}
