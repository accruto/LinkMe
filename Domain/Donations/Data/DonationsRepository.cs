using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Donations.Data
{
    public class DonationsRepository
        : Repository, IDonationsRepository
    {
        private static readonly DataLoadOptions RequestLoadOptions = DataOptions.CreateLoadOptions<DonationRequestEntity>(r => r.DonationRecipientEntity);

        private static readonly Func<DonationsDataContext, IQueryable<DonationRecipient>> GetRecipients
            = CompiledQuery.Compile((DonationsDataContext dc)
                => from r in dc.DonationRecipientEntities
                   where r.isActive
                   orderby r.displayName
                   select r.Map());

        private static readonly Func<DonationsDataContext, Guid, DonationRecipient> GetRecipient
            = CompiledQuery.Compile((DonationsDataContext dc, Guid id)
                => (from r in dc.DonationRecipientEntities
                    where r.id == id
                    select r.Map()).SingleOrDefault());

        private static readonly Func<DonationsDataContext, string, DonationRecipient> GetRecipientByName
            = CompiledQuery.Compile((DonationsDataContext dc, string name)
                => (from r in dc.DonationRecipientEntities
                    where r.displayName == name
                    select r.Map()).SingleOrDefault());

        private static readonly Func<DonationsDataContext, Guid, DonationRequest> GetRequestQuery
            = CompiledQuery.Compile((DonationsDataContext dc, Guid id)
                => (from r in dc.DonationRequestEntities
                    where r.id == id
                    select r.Map()).SingleOrDefault());

        private static readonly Func<DonationsDataContext, Guid, decimal, DonationRequest> GetRequestByRecipientIdQuery
            = CompiledQuery.Compile((DonationsDataContext dc, Guid recipientId, decimal amount)
                => (from r in dc.DonationRequestEntities
                    where r.donationRecipientId == recipientId
                    && r.amount == amount
                    select r.Map()).SingleOrDefault());

        private static readonly Func<DonationsDataContext, Guid, Donation> GetDonation
            = CompiledQuery.Compile((DonationsDataContext dc, Guid id)
                => (from i in dc.NetworkInvitationEntities
                    where i.id == id
                    && !Equals(i.donationRequestId, null)
                    select i.Map()).SingleOrDefault());

        private static readonly Func<DonationsDataContext, string, IQueryable<Donation>> GetDonations
            = CompiledQuery.Compile((DonationsDataContext dc, string ids)
                => from i in dc.NetworkInvitationEntities
                   join d in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on i.id equals d.value
                   where !Equals(i.donationRequestId, null)
                   select i.Map());

        private static readonly Func<DonationsDataContext, Guid, NetworkInvitationEntity> GetNetworkInvitationEntity
            = CompiledQuery.Compile((DonationsDataContext dc, Guid id)
                => (from i in dc.NetworkInvitationEntities
                    where i.id == id
                    select i).SingleOrDefault());

        public DonationsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<DonationRecipient> IDonationsRepository.GetRecipients()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecipients(dc).ToList();
            }
        }

        DonationRecipient IDonationsRepository.GetRecipient(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecipient(dc, id);
            }
        }

        DonationRecipient IDonationsRepository.GetRecipient(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecipientByName(dc, name);
            }
        }

        void IDonationsRepository.CreateRequest(DonationRequest request)
        {
            using (var dc = CreateContext())
            {
                dc.DonationRequestEntities.InsertOnSubmit(request.Map());
                dc.SubmitChanges();
            }
        }

        DonationRequest IDonationsRepository.GetRequest(Guid id)
        {
            using (var dc = CreateContext())
            {
                return GetRequest(dc, id);
            }
        }

        DonationRequest IDonationsRepository.GetRequest(Guid recipientId, decimal amount)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRequestByRecipientId(dc, recipientId, amount);
            }
        }

        void IDonationsRepository.CreateDonation(Donation donation)
        {
            using (var dc = CreateContext())
            {
                // Only update what is already there.

                var entity = GetNetworkInvitationEntity(dc, donation.Id);
                if (entity != null)
                {
                    entity.donationRequestId = donation.RequestId;
                    dc.SubmitChanges();
                }
            }
        }

        Donation IDonationsRepository.GetDonation(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDonation(dc, id);
            }
        }

        IList<Donation> IDonationsRepository.GetDonations(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDonations(dc, new SplitList<Guid>(ids).ToString()).ToList();
            }
        }

        private static DonationRequest GetRequest(DonationsDataContext dc, Guid id)
        {
            dc.LoadOptions = RequestLoadOptions;
            return GetRequestQuery(dc, id);
        }

        private static DonationRequest GetRequestByRecipientId(DonationsDataContext dc, Guid recipientId, decimal amount)
        {
            dc.LoadOptions = RequestLoadOptions;
            return GetRequestByRecipientIdQuery(dc, recipientId, amount);
        }

        private DonationsDataContext CreateContext()
        {
            return CreateContext(c => new DonationsDataContext(c));
        }
    }
}
