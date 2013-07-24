using System;
using System.Data.Linq;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Criterias.Data;

namespace LinkMe.Domain.Roles.Communications.Campaigns.Data
{
    internal partial class CampaignCriteriaEntity
        : ICriteriaEntity
    {
    }

    internal partial class CampaignCriteriaSetEntity
        : ICriteriaSetEntity<CampaignCriteriaEntity>
    {
        public string type { get; set; }

        public EntitySet<CampaignCriteriaEntity> Entities
        {
            get { return CampaignCriteriaEntities; }
            set { CampaignCriteriaEntities = value; }
        }
    }

    internal static class Mappings
    {
        public static CampaignEntity Map(this Campaign campaign)
        {
            var entity = new CampaignEntity
            {
                id = campaign.Id,
                createdTime = campaign.CreatedTime,
                createdBy = campaign.CreatedBy,
                status = (int) campaign.Status,
            };

            campaign.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Campaign campaign, CampaignEntity entity)
        {
            entity.name = campaign.Name;
            entity.category = (int) campaign.Category;
            entity.communicationCategoryId = campaign.CommunicationCategoryId;
            entity.communicationDefinitionId = campaign.CommunicationDefinitionId;
            entity.query = campaign.Query;
        }

        public static Campaign Map(this CampaignEntity entity)
        {
            return new Campaign
            {
                Id = entity.id,
                Name = entity.name,
                CreatedTime = entity.createdTime,
                CreatedBy = entity.createdBy,
                Status = (CampaignStatus)entity.status,
                Category = (CampaignCategory)entity.category,
                CommunicationCategoryId = entity.communicationCategoryId,
                CommunicationDefinitionId = entity.communicationDefinitionId,
                Query = entity.query,
            };
        }

        public static CampaignTemplateEntity Map(this Template template, Guid campaignId)
        {
            var entity = new CampaignTemplateEntity { id = campaignId };
            template.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Template template, CampaignTemplateEntity entity)
        {
            entity.subject = template.Subject;
            entity.body = template.Body;
        }

        public static Template Map(this CampaignTemplateEntity entity)
        {
            return new Template
            {
                Subject = entity.subject,
                Body = entity.body
            };
        }

        public static Criteria Map(this CampaignEntity entity, CampaignCriteriaSetEntity criteriaSetEntity, ICriteriaPersister criteriaPersister)
        {
            var type = ((CampaignCategory) entity.category).ToString();
            return criteriaSetEntity == null
                ? criteriaPersister.CreateCriteria(type)
                : criteriaSetEntity.MapTo<CampaignCriteriaSetEntity, CampaignCriteriaEntity, Criteria>(type, criteriaPersister, false);
        }
    }
}
