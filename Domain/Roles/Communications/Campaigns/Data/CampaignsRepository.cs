using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Criterias.Data;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Communications.Campaigns.Data
{
    public class CampaignsRepository
        : Repository, ICampaignsRepository
    {
        private readonly ICriteriaPersister _criteriaPersister;

        private static readonly DataLoadOptions CriteriaLoadOptions = DataOptions.CreateLoadOptions<CampaignCriteriaSetEntity>(c => c.CampaignCriteriaEntities);

        private static readonly Func<CampaignsDataContext, Guid, CampaignEntity> GetCampaignEntity
            = CompiledQuery.Compile((CampaignsDataContext dc, Guid id)
                => (from c in dc.CampaignEntities
                    where c.id == id
                    select c).SingleOrDefault());

        private static readonly Func<CampaignsDataContext, Guid, Campaign> GetCampaign
            = CompiledQuery.Compile((CampaignsDataContext dc, Guid id)
                => (from c in dc.CampaignEntities
                    where c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<CampaignsDataContext, string, Campaign> GetCampaignByName
            = CompiledQuery.Compile((CampaignsDataContext dc, string name)
                => (from c in dc.CampaignEntities
                    where c.name == name
                    select c.Map()).SingleOrDefault());

        private static readonly Func<CampaignsDataContext, CampaignCategory, int> GetCampaignCountByCategory
            = CompiledQuery.Compile((CampaignsDataContext dc, CampaignCategory category)
                => (from c in dc.CampaignEntities
                    where c.status != (int)CampaignStatus.Deleted
                    && c.category == (int)category
                    select c).Count());

        private static readonly Func<CampaignsDataContext, int> GetCampaignCount
            = CompiledQuery.Compile((CampaignsDataContext dc)
                => (from c in dc.CampaignEntities
                    where c.status != (int)CampaignStatus.Deleted
                    select c).Count());

        private static readonly Func<CampaignsDataContext, CampaignCategory, int, int, IQueryable<Campaign>> GetCampaignsByCategorySkipTakeQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, CampaignCategory category, int skip, int take)
                => from c in
                       (from c in dc.CampaignEntities
                        where c.status != (int)CampaignStatus.Deleted
                        && c.category == (int)category
                        orderby c.createdTime descending
                        select c).Skip(skip).Take(take)
                   select c.Map());

        private static readonly Func<CampaignsDataContext, CampaignCategory, int, IQueryable<Campaign>> GetCampaignsByCategorySkipQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, CampaignCategory category, int skip)
                => from c in
                       (from c in dc.CampaignEntities
                        where c.status != (int)CampaignStatus.Deleted
                        && c.category == (int)category
                        orderby c.createdTime descending
                        select c).Skip(skip)
                   select c.Map());

        private static readonly Func<CampaignsDataContext, CampaignCategory, int, IQueryable<Campaign>> GetCampaignsByCategoryTakeQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, CampaignCategory category, int take)
                => from c in
                       (from c in dc.CampaignEntities
                        where c.status != (int)CampaignStatus.Deleted
                        && c.category == (int)category
                        orderby c.createdTime descending
                        select c).Take(take)
                   select c.Map());

        private static readonly Func<CampaignsDataContext, CampaignCategory, IQueryable<Campaign>> GetCampaignsByCategoryQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, CampaignCategory category)
                => from c in dc.CampaignEntities
                   where c.status != (int) CampaignStatus.Deleted
                   && c.category == (int) category
                   orderby c.createdTime descending
                   select c.Map());

        private static readonly Func<CampaignsDataContext, CampaignCategory, CampaignStatus, IQueryable<Campaign>> GetCampaignsByCategoryStatus
            = CompiledQuery.Compile((CampaignsDataContext dc, CampaignCategory category, CampaignStatus status)
                => from c in dc.CampaignEntities
                   where c.status == (int)status
                   && c.category == (int)category
                   orderby c.createdTime descending
                   select c.Map());

        private static readonly Func<CampaignsDataContext, CampaignStatus, IQueryable<Campaign>> GetCampaignsByStatus
            = CompiledQuery.Compile((CampaignsDataContext dc, CampaignStatus status)
                => from c in dc.CampaignEntities
                   where c.status == (int)status
                   orderby c.createdTime descending
                   select c.Map());

        private static readonly Func<CampaignsDataContext, Range, IQueryable<Campaign>> GetCampaignsSkipTakeQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, Range range)
                => (from c in dc.CampaignEntities
                    where c.status != (int)CampaignStatus.Deleted
                    orderby c.createdTime descending
                    select c).Skip(range.Skip).Take(range.Take.Value).Select(c => c.Map()));

        private static readonly Func<CampaignsDataContext, Range, IQueryable<Campaign>> GetCampaignsSkipQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, Range range)
                => (from c in dc.CampaignEntities
                    where c.status != (int)CampaignStatus.Deleted
                    orderby c.createdTime descending
                    select c).Skip(range.Skip).Select(c => c.Map()));

        private static readonly Func<CampaignsDataContext, Range, IQueryable<Campaign>> GetCampaignsTakeQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, Range range)
                => (from c in dc.CampaignEntities
                    where c.status != (int)CampaignStatus.Deleted
                    orderby c.createdTime descending
                    select c).Take(range.Take.Value).Select(c => c.Map()));

        private static readonly Func<CampaignsDataContext, IQueryable<Campaign>> GetCampaignsQuery
            = CompiledQuery.Compile((CampaignsDataContext dc)
                => (from c in dc.CampaignEntities
                    where c.status != (int)CampaignStatus.Deleted
                    orderby c.createdTime descending
                    select c.Map()));

        private static readonly Func<CampaignsDataContext, Guid, Template> GetTemplate
            = CompiledQuery.Compile((CampaignsDataContext dc, Guid id)
                => (from t in dc.CampaignTemplateEntities
                    where t.id == id
                    select t.Map()).SingleOrDefault());

        private static readonly Func<CampaignsDataContext, Guid, ICriteriaPersister, Criteria> GetCriteriaQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, Guid campaignId, ICriteriaPersister criteriaPersister)
                => (from c in dc.CampaignEntities
                    where c.id == campaignId
                    select c.Map((from s in dc.CampaignCriteriaSetEntities where s.campaignId == c.id select s).SingleOrDefault(), criteriaPersister)).SingleOrDefault());

        private static readonly Func<CampaignsDataContext, Guid, CampaignCriteriaSetEntity> GetCampaignCriteriaSetEntityQuery
            = CompiledQuery.Compile((CampaignsDataContext dc, Guid campaignId)
                => (from c in dc.CampaignCriteriaSetEntities
                    where c.campaignId == campaignId
                    select c).SingleOrDefault());

        private static readonly Func<CampaignsDataContext, Guid, CampaignTemplateEntity> GetTemplateEntity
            = CompiledQuery.Compile((CampaignsDataContext dc, Guid id)
                => (from t in dc.CampaignTemplateEntities
                    where t.id == id
                    select t).SingleOrDefault());

        public CampaignsRepository(IDataContextFactory dataContextFactory, ICriteriaPersister criteriaPersister)
            : base(dataContextFactory)
        {
            _criteriaPersister = criteriaPersister;
        }

        void ICampaignsRepository.CreateCampaign(Campaign campaign)
        {
            using (var dc = CreateContext())
            {
                // Insert both the campaign and the associated template.

                dc.CampaignEntities.InsertOnSubmit(campaign.Map());

                try
                {
                    dc.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    CheckDuplicates(ex);
                    throw;
                }
            }
        }

        void ICampaignsRepository.UpdateCampaign(Campaign campaign)
        {
            using (var dc = CreateContext())
            {
                try
                {
                    // Update the campaign and the template.

                    var campaignEntity = GetCampaignEntity(dc, campaign.Id);
                    campaign.MapTo(campaignEntity);
                    
                    dc.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    CheckDuplicates(ex);
                    throw;
                }
            }
        }

        void ICampaignsRepository.UpdateStatus(Guid campaignId, CampaignStatus status)
        {
            using (var dc = CreateContext())
            {
                // Update just the campaign status.

                var entity = new CampaignEntity {id = campaignId, status = -1};
                dc.CampaignEntities.Attach(entity);
                entity.status = (int) status;
                dc.SubmitChanges();
            }
        }

        Campaign ICampaignsRepository.GetCampaign(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCampaign(dc, id);
            }
        }

        Campaign ICampaignsRepository.GetCampaign(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCampaignByName(dc, name);
            }
        }

        RangeResult<Campaign> ICampaignsRepository.GetCampaigns(CampaignCategory? category, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // Get the count first which should execute a 'SELECT COUNT(*) ...' type query.

                var total = category != null ? GetCampaignCountByCategory(dc, category.Value) : GetCampaignCount(dc);
                var campaigns = category != null ? GetCampaignsByCategory(dc, category.Value, range) : GetCampaigns(dc, range);

                // Get the actual results.

                return new RangeResult<Campaign>(total, campaigns.ToList());
            }
        }

        IList<Campaign> ICampaignsRepository.GetCampaigns(CampaignCategory? category, CampaignStatus status)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var campaigns = category != null ? GetCampaignsByCategoryStatus(dc, category.Value, status) : GetCampaignsByStatus(dc, status);
                return campaigns.ToList();
            }
        }

        void ICampaignsRepository.CreateTemplate(Guid campaignId, Template template)
        {
            using (var dc = CreateContext())
            {
                // Insert both the campaign and the associated template.

                dc.CampaignTemplateEntities.InsertOnSubmit(template.Map(campaignId));
                dc.SubmitChanges();
            }
        }

        void ICampaignsRepository.UpdateTemplate(Guid campaignId, Template template)
        {
            using (var dc = CreateContext())
            {
                var entity = GetTemplateEntity(dc, campaignId);
                template.MapTo(entity);
                dc.SubmitChanges();
            }
        }

        Template ICampaignsRepository.GetTemplate(Guid campaignId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetTemplate(dc, campaignId);
            }
        }

        void ICampaignsRepository.UpdateCriteria(Guid campaignId, Criteria criteria)
        {
            using (var dc = CreateContext())
            {
                // Remove all previous criteria.

                var entity = GetCampaignCriteriaSetEntity(dc, campaignId);
                if (entity != null)
                {
                    dc.CampaignCriteriaEntities.DeleteAllOnSubmit(entity.CampaignCriteriaEntities);
                    dc.CampaignCriteriaSetEntities.DeleteOnSubmit(entity);
                }

                // Insert new.

                entity = criteria.MapTo<CampaignCriteriaSetEntity, CampaignCriteriaEntity, Criteria>(_criteriaPersister, false);
                entity.campaignId = campaignId;
                dc.CampaignCriteriaSetEntities.InsertOnSubmit(entity);

                dc.SubmitChanges();
            }
        }

        Criteria ICampaignsRepository.GetCriteria(Guid campaignId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCriteria(dc, campaignId);
            }
        }

        private static void CheckDuplicates(SqlException ex)
        {
            if (ex.Message.StartsWith("Violation of UNIQUE KEY constraint 'UQ_Campaign_name'"))
                throw new ValidationErrorsException(new DuplicateValidationError("Name"));
        }

        private Criteria GetCriteria(CampaignsDataContext dc, Guid campaignId)
        {
            dc.LoadOptions = CriteriaLoadOptions;
            return GetCriteriaQuery(dc, campaignId, _criteriaPersister);
        }

        private static CampaignCriteriaSetEntity GetCampaignCriteriaSetEntity(CampaignsDataContext dc, Guid campaignId)
        {
            dc.LoadOptions = CriteriaLoadOptions;
            return GetCampaignCriteriaSetEntityQuery(dc, campaignId);
        }

        private static IQueryable<Campaign> GetCampaignsByCategory(CampaignsDataContext dc, CampaignCategory category, Range range)
        {
            if (range.Skip != 0)
                return range.Take != null
                    ? GetCampaignsByCategorySkipTakeQuery(dc, category, range.Skip, range.Take.Value)
                    : GetCampaignsByCategorySkipQuery(dc, category, range.Skip);
            return range.Take != null
                ? GetCampaignsByCategoryTakeQuery(dc, category, range.Take.Value)
                : GetCampaignsByCategoryQuery(dc, category);
        }

        private static IQueryable<Campaign> GetCampaigns(CampaignsDataContext dc, Range range)
        {
            if (range.Skip != 0)
                return range.Take != null
                    ? GetCampaignsSkipTakeQuery(dc, range)
                    : GetCampaignsSkipQuery(dc, range);
            return range.Take != null
                ? GetCampaignsTakeQuery(dc, range)
                : GetCampaignsQuery(dc);
        }

        private CampaignsDataContext CreateContext()
        {
            return CreateContext(c => new CampaignsDataContext(c));
        }
    }
}
