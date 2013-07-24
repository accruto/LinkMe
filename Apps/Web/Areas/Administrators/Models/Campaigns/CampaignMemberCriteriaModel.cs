using System.Collections.Generic;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public class CampaignMemberCriteriaReference
    {
        private readonly IList<Industry> _industries;
        private readonly IList<Community> _communities;
        private readonly IList<Country> _countries;

        public CampaignMemberCriteriaReference(IList<Industry> industries, IList<Community> communities, IList<Country> countries)
        {
            _industries = industries;
            _communities = communities;
            _countries = countries;
        }

        public IList<Industry> Industries
        {
            get { return _industries; }
        }

        public IList<Community> Communities
        {
            get { return _communities; }
        }

        public IList<Country> Countries
        {
            get { return _countries; }
        }
    }

    public class CampaignMemberCriteriaModel
        : CampaignCriteriaModel
    {
        private readonly CampaignMemberCriteriaReference _reference;

        public CampaignMemberCriteriaModel(Campaign campaign, MemberSearchCriteria criteria, bool isReadOnly, IList<Industry> industries, IList<Community> communities, IList<Country> countries)
            : base(campaign, criteria, isReadOnly)
        {
            _reference = new CampaignMemberCriteriaReference(industries, communities, countries);
        }

        public new MemberSearchCriteria Criteria
        {
            get { return (MemberSearchCriteria)base.Criteria; }
        }

        public CampaignMemberCriteriaReference Reference
        {
            get { return _reference; }
        }
    }
}