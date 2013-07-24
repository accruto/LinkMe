using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Invitations;
using LinkMe.Domain.Roles.Invitations.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Networking.Data
{
    public class NetworkingRepository
        : Repository, INetworkingRepository
    {
        private struct GetInvitationsByInviteeArgs
        {
            public Guid InviteeId;
            public string InviteeEmailAddress;
            public DateTime MinLastSentTime;
        }

        private static readonly DataLoadOptions InvitationLoadOptions = DataOptions.CreateLoadOptions<NetworkInvitationEntity>(i => i.UserToUserRequestEntity);

        private static readonly Func<NetworkingDataContext, Guid, Guid, bool> AreFirstDegreeLinks
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid fromId, Guid toId)
                => (from l in dc.NetworkLinkEntities
                    where l.fromNetworkerId == fromId
                    && l.toNetworkerId == toId
                    select l).Any());

        private static readonly Func<NetworkingDataContext, Guid, IQueryable<Guid>> GetFirstDegreeLinks
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid fromId)
                => from l in dc.NetworkLinkEntities
                   where l.fromNetworkerId == fromId
                   select l.toNetworkerId);

        private static readonly Func<NetworkingDataContext, Guid, Guid, NetworkLinkEntity> GetLinkEntity
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid fromId, Guid toId)
                => (from l in dc.NetworkLinkEntities
                    where l.fromNetworkerId == fromId && l.toNetworkerId == toId
                    select l).SingleOrDefault());

        private static readonly Func<NetworkingDataContext, Guid, Guid, IgnoredNetworkMatchEntity> GetIgnoredMatchEntity
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid fromId, Guid toId)
                => (from i in dc.IgnoredNetworkMatchEntities
                    where i.ignorerId == fromId
                    && i.ignoredId == toId
                    select i).SingleOrDefault());

        private static readonly Func<NetworkingDataContext, Guid, NetworkInvitationEntity> GetInvitationEntity
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid id)
                => (from i in dc.NetworkInvitationEntities
                    where i.id == id
                    select i).SingleOrDefault());

        private static readonly Func<NetworkingDataContext, Guid, IInvitationFactory, Invitation> GetInvitationQuery
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid id, IInvitationFactory factory)
                => (from i in dc.NetworkInvitationEntities
                    where i.id == id
                    select i.Map(factory)).SingleOrDefault());

        private static readonly Func<NetworkingDataContext, Guid, string, IInvitationFactory, IQueryable<Invitation>> GetInvitationsByEmailAddressQuery
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid inviterId, string inviteeEmailAddress, IInvitationFactory factory)
                => from i in dc.NetworkInvitationEntities
                   where i.inviterId == inviterId
                   && i.inviteeEmailAddress == inviteeEmailAddress
                   select i.Map(factory));

        private static readonly Func<NetworkingDataContext, Guid, Guid, IInvitationFactory, IQueryable<Invitation>> GetInvitationsQuery
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid inviterId, Guid inviteeId, IInvitationFactory factory)
                => from i in dc.NetworkInvitationEntities
                   where i.inviterId == inviterId
                   && i.inviteeId == inviteeId
                   select i.Map(factory));
        
        private static readonly Func<NetworkingDataContext, Guid, DateTime, IInvitationFactory, IQueryable<Invitation>> GetInvitationsByInviterQuery
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid inviterId, DateTime minLastSentTime, IInvitationFactory factory)
                => from i in dc.NetworkInvitationEntities
                   where i.inviterId == inviterId
                   && i.UserToUserRequestEntity.status == (byte)RequestStatus.Pending
                   && i.UserToUserRequestEntity.lastSentTime > minLastSentTime
                   orderby i.UserToUserRequestEntity.lastSentTime
                   select i.Map(factory));

        private static readonly Func<NetworkingDataContext, GetInvitationsByInviteeArgs, IInvitationFactory, IQueryable<Invitation>> GetInvitationsByInviteeQuery
            = CompiledQuery.Compile((NetworkingDataContext dc, GetInvitationsByInviteeArgs args, IInvitationFactory factory)
                => from i in dc.NetworkInvitationEntities
                   where (i.inviteeId == args.InviteeId || i.inviteeEmailAddress == args.InviteeEmailAddress)
                   && i.UserToUserRequestEntity.status == (byte)RequestStatus.Pending
                   && i.UserToUserRequestEntity.lastSentTime > args.MinLastSentTime
                   orderby i.UserToUserRequestEntity.lastSentTime
                   select i.Map(factory));

        private static readonly Func<NetworkingDataContext, Guid, DateTimeRange, int> GetInvitationCount
            = CompiledQuery.Compile((NetworkingDataContext dc, Guid inviterId, DateTimeRange dateTimeRange)
                => (from i in dc.NetworkInvitationEntities
                    where i.inviterId == inviterId
                    && i.UserToUserRequestEntity.firstSentTime >= dateTimeRange.Start.Value && i.UserToUserRequestEntity.firstSentTime < dateTimeRange.End.Value
                    select i).Count());

        public NetworkingRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        bool INetworkingRepository.AreFirstDegreeLinked(Guid fromId, Guid toId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return AreFirstDegreeLinks(dc, fromId, toId);
            }
        }

        IList<Guid> INetworkingRepository.GetFirstDegreeLinks(Guid fromId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFirstDegreeLinks(dc, fromId).ToList();
            }
        }

        void INetworkingRepository.CreateFirstDegreeLink(Guid fromId, Guid toId)
        {
            using (var dc = CreateContext())
            {
                // Need both records.

                var time = DateTime.Now;

                var entity = GetLinkEntity(dc, fromId, toId);
                if (entity == null)
                    dc.NetworkLinkEntities.InsertOnSubmit(Mappings.CreateNetworkLinkEntity(fromId, toId, time));

                entity = GetLinkEntity(dc, toId, fromId);
                if (entity == null)
                    dc.NetworkLinkEntities.InsertOnSubmit(Mappings.CreateNetworkLinkEntity(toId, fromId, time));

                dc.SubmitChanges();
            }
        }

        void INetworkingRepository.DeleteFirstDegreeLink(Guid fromId, Guid toId)
        {
            using (var dc = CreateContext())
            {
                // Need both records.

                var entity = GetLinkEntity(dc, fromId, toId);
                if (entity != null)
                    dc.NetworkLinkEntities.DeleteOnSubmit(entity);

                entity = GetLinkEntity(dc, toId, fromId);
                if (entity != null)
                    dc.NetworkLinkEntities.DeleteOnSubmit(entity);

                dc.SubmitChanges();
            }
        }

        void INetworkingRepository.IgnoreSecondDegreeLink(Guid fromId, Guid toId)
        {
            using (var dc = CreateContext())
            {
                // Check whether already ignored.

                var entity = GetIgnoredMatchEntity(dc, fromId, toId);
                if (entity == null)
                {
                    dc.IgnoredNetworkMatchEntities.InsertOnSubmit(Mappings.CreateIgnoredNetworkMatchEntity(fromId, toId));
                    dc.SubmitChanges();
                }
            }
        }

        void IInvitationsRepository<NetworkingInvitation>.CreateInvitation(NetworkingInvitation invitation)
        {
            using (var dc = CreateContext())
            {
                dc.NetworkInvitationEntities.InsertOnSubmit(invitation.Map());
                dc.SubmitChanges();
            }
        }

        void IInvitationsRepository<NetworkingInvitation>.UpdateInvitation(NetworkingInvitation invitation)
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

        TNetworkingInvitation IInvitationsRepository<NetworkingInvitation>.GetInvitation<TNetworkingInvitation>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitation<TNetworkingInvitation>(dc, id);
            }
        }

        IList<TNetworkingInvitation> IInvitationsRepository<NetworkingInvitation>.GetInvitations<TNetworkingInvitation>(Guid inviterId, string inviteeEmailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TNetworkingInvitation>(dc, inviterId, inviteeEmailAddress).ToList();
            }
        }

        IList<TNetworkingInvitation> IInvitationsRepository<NetworkingInvitation>.GetInvitations<TNetworkingInvitation>(Guid inviterId, Guid inviteeId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TNetworkingInvitation>(dc, inviterId, inviteeId).ToList();
            }
        }

        IList<TNetworkingInvitation> IInvitationsRepository<NetworkingInvitation>.GetInvitations<TNetworkingInvitation>(Guid inviterId, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TNetworkingInvitation>(dc, inviterId, minLastSentTime).ToList();
            }
        }

        IList<TNetworkingInvitation> IInvitationsRepository<NetworkingInvitation>.GetInvitations<TNetworkingInvitation>(Guid inviteeId, string inviteeEmailAddress, DateTime minLastSentTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitations<TNetworkingInvitation>(dc, inviteeId, inviteeEmailAddress, minLastSentTime).ToList();
            }
        }

        int IInvitationsRepository<NetworkingInvitation>.GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetInvitationCount(dc, inviterId, dateTimeRange);
            }
        }

        private static TInvitation GetInvitation<TInvitation>(NetworkingDataContext dc, Guid id)
            where TInvitation : NetworkingInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationQuery(dc, id, new InvitationFactory<TInvitation>()) as TInvitation;
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(NetworkingDataContext dc, Guid inviterId, string inviteeEmailAddress)
            where TInvitation : NetworkingInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationsByEmailAddressQuery(dc, inviterId, inviteeEmailAddress, new InvitationFactory<TInvitation>()).Cast<TInvitation>();
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(NetworkingDataContext dc, Guid inviterId, Guid inviteeId)
            where TInvitation : NetworkingInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationsQuery(dc, inviterId, inviteeId, new InvitationFactory<TInvitation>()).Cast<TInvitation>();
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(NetworkingDataContext dc, Guid inviterId, DateTime minLastSentTime)
            where TInvitation : NetworkingInvitation, new()
        {
            dc.LoadOptions = InvitationLoadOptions;
            return GetInvitationsByInviterQuery(dc, inviterId, minLastSentTime, new InvitationFactory<TInvitation>()).Cast<TInvitation>();
        }

        private static IQueryable<TInvitation> GetInvitations<TInvitation>(NetworkingDataContext dc, Guid inviteeId, string inviteeEmailAddress, DateTime minLastSentTime)
            where TInvitation : NetworkingInvitation, new()
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

        private NetworkingDataContext CreateContext()
        {
            return CreateContext(c => new NetworkingDataContext(c));
        }
    }
}
