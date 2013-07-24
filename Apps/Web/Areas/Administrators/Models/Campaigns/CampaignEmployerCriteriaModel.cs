using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Query.Search.Employers;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public class CampaignEmployerCriteriaReference
    {
        private readonly IList<Industry> _industries;

        public CampaignEmployerCriteriaReference(IList<Industry> industries)
        {
            _industries = industries;
        }

        public IList<Industry> Industries
        {
            get { return _industries; }
        }
    }

    public class CampaignEmployerCriteriaModel
        : CampaignCriteriaModel
    {
        private readonly CampaignEmployerCriteriaReference _reference;

        public CampaignEmployerCriteriaModel(Campaign campaign, EmployerSearchCriteria criteria, bool isReadOnly, IList<Industry> industries)
            : base(campaign, criteria, isReadOnly)
        {
            _reference = new CampaignEmployerCriteriaReference(industries);
        }

        public new OrganisationEmployerSearchCriteria Criteria
        {
            get { return (OrganisationEmployerSearchCriteria)base.Criteria; }
        }

        public CampaignEmployerCriteriaReference Reference
        {
            get { return _reference; }
        }
    }
}