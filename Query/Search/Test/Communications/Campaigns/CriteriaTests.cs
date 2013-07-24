using System;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;

namespace LinkMe.Query.Search.Test.Communications.Campaigns
{
    public abstract class CriteriaTests
        : TestClass
    {
        private const string CampaignNameFormat = "My new campaign{0}";
        private const string TemplateSubjectFormat = "The subject{0} of the template";
        private const string TemplateBodyFormat = "The body{0} of the template";

        protected ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        protected ICampaignsQuery _campaignsQuery = Resolve<ICampaignsQuery>();

        protected static Campaign CreateTestCampaign(int index, CampaignCategory category)
        {
            return new Campaign
            {
                Name = string.Format(CampaignNameFormat, index),
                Category = category,
                CreatedBy = Guid.NewGuid(),
/*                Template = new Template
                {
                    Subject = string.Format(TemplateSubjectFormat, index),
                    Body = string.Format(TemplateBodyFormat, index)
                },*/
            };
        }
    }
}