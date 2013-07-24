using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Communications.Campaigns.Commands
{
    public class CampaignsCommand
        : ICampaignsCommand
    {
        private static readonly IDictionary<CampaignStatus, IEnumerable<CampaignStatus>> ValidStatusChanges = new Dictionary<CampaignStatus, IEnumerable<CampaignStatus>>
        {
            { CampaignStatus.Activated, new[] { CampaignStatus.Activated, CampaignStatus.Deleted, CampaignStatus.Running, CampaignStatus.Stopped } },
            { CampaignStatus.Deleted, new[] { CampaignStatus.Deleted } },
            { CampaignStatus.Draft, new[] { CampaignStatus.Activated, CampaignStatus.Deleted, CampaignStatus.Draft } },
            { CampaignStatus.Running, new[] { CampaignStatus.Running, CampaignStatus.Stopped } },
            { CampaignStatus.Stopped, new[] { CampaignStatus.Activated, CampaignStatus.Deleted, CampaignStatus.Stopped } }
        };

        private readonly ICampaignsRepository _repository;

        public CampaignsCommand(ICampaignsRepository repository)
        {
            _repository = repository;
        }

        void ICampaignsCommand.CreateCampaign(Campaign campaign)
        {
            // Pre-process it.

            campaign.Prepare();
            campaign.Validate();

            // Create it.

            _repository.CreateCampaign(campaign);
        }

        void ICampaignsCommand.UpdateCampaign(Campaign campaign)
        {
            // Pre-process it.

            campaign.Prepare();
            campaign.Validate();

            // Update it.

            _repository.UpdateCampaign(campaign);
        }

        void ICampaignsCommand.UpdateStatus(Campaign campaign, CampaignStatus status)
        {
            // Check that it is OK to change the status.

            ValidateStatus(campaign, status);

            // Update it.

            _repository.UpdateStatus(campaign.Id, status);
            campaign.Status = status;
        }

        void ICampaignsCommand.CreateTemplate(Guid campaignId, Template template)
        {
            // Pre-process it.

            template.Prepare();
            template.Validate();

            // Create it.

            _repository.CreateTemplate(campaignId, template);
        }

        void ICampaignsCommand.UpdateTemplate(Guid campaignId, Template template)
        {
            // Pre-process it.

            template.Validate();

            // Update it.

            _repository.UpdateTemplate(campaignId, template);
        }

        void ICampaignsCommand.UpdateCriteria(Guid campaignId, Criteria criteria)
        {
            criteria.Prepare();
            criteria.Validate();
            _repository.UpdateCriteria(campaignId, criteria);
        }

        private static void ValidateStatus(Campaign campaign, CampaignStatus status)
        {
            if (!ValidStatusChanges[campaign.Status].Contains(status))
                throw new ValidationErrorsException(new ChangedValidationError("Status", campaign.Status.ToString(), status.ToString()));
        }
    }
}