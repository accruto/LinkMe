using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.JobAds.Data
{
    [Flags]
    internal enum JobPosterFlags
    {
        None = 0,
        /// <summary>
        /// Show the Suggested Candidates page after this user posts a job ad.
        /// </summary>
        ShowSuggestedCandidatesPage = 1,
        // ReceiveSuggestedCandidatesEmails is obsolete - NonMemberSettings.SuppressSuggestedCandidatesEmails
        // is now always used for this, even if the user is a registered employer.
        // ReceiveSuggestedCandidatesEmails = 2,
        /// <summary>
        /// Email Suggested Candidates to the contact email address when this user posts a job ad, unless the
        /// contact person has opted out via EmployerFlags.ReceiveSuggestedCandidatesEmails or
        /// UnregisteredEmailFlags.SuppressSuggestedCandidatesEmails.
        /// </summary>
        SendSuggestedCandidatesForAdsPosted = 4
    }

    internal partial class ContactDetailsEntity
        : IContactDetailsEntity
    {
    }

    internal partial class LocationReferenceEntity
        : ILocationReferenceEntity
    {
    }

    internal interface IHaveIntegrationEntity
    {
        Guid? integratorUserId { get; set; }
        string integratorReferenceId { get; set; }
        string externalReferenceId { get; set; }
        string externalApplyUrl { get; set; }
    }

    internal interface IHaveApplicationRequirementsEntity
    {
        string jobg8ApplyForm { get; set; }
    }

    internal interface IHaveDescriptionEntity
    {
        string positionTitle { get; set; }
        string employerCompanyName { get; set; }
        string summary { get; set; }
        string content { get; set; }
        string bulletPoints { get; set; }
        byte jobTypes { get; set; }
        string packageDetails { get; set; }
        bool residencyRequired { get; set; }
        EntitySet<JobAdLocationEntity> JobAdLocationEntities { get; set; }
        EntitySet<JobAdIndustryEntity> JobAdIndustryEntities { get; set; }
        decimal? minSalary { get; set; }
        decimal? maxSalary { get; set; }
        byte salaryRateType { get; set; }
        decimal? minParsedSalary { get; set; }
        decimal? maxParsedSalary { get; set; }
    }

    internal partial class JobAdEntity
        : IHaveContactDetailsEntity<ContactDetailsEntity>, IHaveIntegrationEntity, IHaveApplicationRequirementsEntity, IHaveDescriptionEntity
    {
        string IHaveContactDetailsEntity<ContactDetailsEntity>.companyName
        {
            get { return contactCompanyName; }
            set { contactCompanyName = value; }
        }
    }

    internal partial class JobAdsDataContext
        : IHaveContactDetailsEntities<ContactDetailsEntity>
    {
        Table<ContactDetailsEntity> IHaveContactDetailsEntities<ContactDetailsEntity>.ContactDetailsEntities
        {
            get { return ContactDetailsEntities; }
        }
    }

    internal static class Mappings
    {
        private const char BulletPointSeparator = '\n';

        public static JobAdEntry Map(this JobAdEntity entity)
        {
            return entity.MapTo<JobAdEntry>();
        }

        public static JobAd Map(this JobAdEntity entity, ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            var jobAd = entity.MapTo<JobAd>();
            entity.MapTo(jobAd.Description, locationQuery, industriesQuery);
            return jobAd;
        }

        private static T MapTo<T>(this JobAdEntity entity)
            where T : JobAdEntry, new()
        {
            var t = new T
            {
                Id = entity.id,
                PosterId = entity.jobPosterId,
                CreatedTime = entity.createdTime,
                LastUpdatedTime = entity.lastUpdatedTime,
                ExpiryTime = entity.expiryTime,
                Title = entity.title,
                Status = (JobAdStatus)entity.status,
                FeatureBoost = (JobAdFeatureBoost) entity.isFeatured,
                Features = (JobAdFeatures)entity.features,
                ContactDetails = ((IHaveContactDetailsEntity<ContactDetailsEntity>)entity).Map(),
                LogoId = entity.brandingLogoImageId,
                Visibility =
                {
                    HideContactDetails = entity.hideContactDetails,
                    HideCompany = entity.hideCompany,
                },
            };

            ((IHaveIntegrationEntity)entity).MapTo(t.Integration);
            return t;
        }

        public static JobAdEntity Map(this JobAd jobAd)
        {
            var entity = new JobAdEntity
            {
                id = jobAd.Id,
                jobPosterId = jobAd.PosterId,
                createdTime = jobAd.CreatedTime,
                status = (byte) jobAd.Status,
            };

            jobAd.MapTo(entity);
            return entity;
        }

        public static void MapTo(this JobAdEntry jobAd, JobAdEntity entity)
        {
            entity.expiryTime = jobAd.ExpiryTime;
            entity.lastUpdatedTime = jobAd.LastUpdatedTime;
            entity.title = jobAd.Title;
            entity.isFeatured = (byte) jobAd.FeatureBoost;
            entity.features = (int) jobAd.Features;
            entity.brandingLogoImageId = jobAd.LogoId;
            entity.hideContactDetails = jobAd.Visibility.HideContactDetails;
            entity.hideCompany = jobAd.Visibility.HideCompany;

            ((IHaveContactDetails)jobAd).MapTo(entity);
            jobAd.MapTo((IHaveIntegrationEntity)entity);
        }

        public static void MapTo(this JobAd jobAd, JobAdEntity entity)
        {
            ((JobAdEntry)jobAd).MapTo(entity);
            jobAd.MapTo((IHaveDescriptionEntity)entity);
        }

        private static void MapTo(this IHaveIntegrationEntity entity, JobAdIntegration integration)
        {
            integration.IntegratorUserId = entity.integratorUserId;
            integration.IntegratorReferenceId = entity.integratorReferenceId;
            integration.ExternalReferenceId = entity.externalReferenceId;
            integration.ExternalApplyUrl = entity.externalApplyUrl;
            ((IHaveApplicationRequirementsEntity) entity).MapTo(integration);
        }

        private static void MapTo(this JobAdEntry jobAd, IHaveIntegrationEntity entity)
        {
            entity.integratorUserId = jobAd.Integration.IntegratorUserId;
            entity.externalReferenceId = jobAd.Integration.ExternalReferenceId;
            entity.externalApplyUrl = jobAd.Integration.ExternalApplyUrl;
            entity.integratorReferenceId = jobAd.Integration.IntegratorReferenceId;
        }

        private static void MapTo(this IHaveDescriptionEntity entity, JobAdDescription description, ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            description.PositionTitle = entity.positionTitle;
            description.CompanyName = entity.employerCompanyName;
            description.Summary = entity.summary;
            description.Content = entity.content;
            description.BulletPoints = TextUtil.StringToArrayForPersistence(BulletPointSeparator, entity.bulletPoints, true);
            description.JobTypes = (JobTypes) entity.jobTypes;
            description.Package = entity.packageDetails;
            description.ResidencyRequired = entity.residencyRequired;
            description.Location = entity.JobAdLocationEntities.Map(locationQuery);
            description.Industries = entity.JobAdIndustryEntities.Map(industriesQuery);
            description.Salary = new Salary
            {
                LowerBound = entity.minSalary,
                UpperBound = entity.maxSalary,
                Rate = (SalaryRate)entity.salaryRateType,
                Currency = Currency.AUD
            };
            description.ParsedSalary = new Salary
            {
                LowerBound = entity.minParsedSalary,
                UpperBound = entity.maxParsedSalary,
                Rate = (SalaryRate)entity.salaryRateType,
                Currency = Currency.AUD
            };
        }

        private static LocationReference Map(this IList<JobAdLocationEntity> entities, ILocationQuery locationQuery)
        {
            if (entities == null || entities.Count == 0)
                return null;

            // Even though it is a collection in the database only one location per ad is supported.

            return entities[0].LocationReferenceEntity.Map(locationQuery);
        }

        private static EntitySet<JobAdLocationEntity> Map(this LocationReference locationReference, Guid jobAdId)
        {
            if (locationReference == null)
                return null;

            // Even though it is a collection in the database only one location per ad is supported.

            return new EntitySet<JobAdLocationEntity>
            {
                new JobAdLocationEntity
                {
                    jobAdId = jobAdId,
                    LocationReferenceEntity = locationReference.MapTo<LocationReferenceEntity>()
                }
            };
        }

        private static IList<Industry> Map(this ICollection<JobAdIndustryEntity> entities, IIndustriesQuery industriesQuery)
        {
            if (entities == null || entities.Count == 0)
                return null;

            return industriesQuery.GetIndustries(from e in entities select e.industryId);
        }

        private static void MapTo(this JobAd jobAd, IHaveDescriptionEntity entity)
        {
            entity.employerCompanyName = jobAd.Description.CompanyName;
            entity.content = jobAd.Description.Content;
            entity.summary = jobAd.Description.Summary;
            entity.bulletPoints = TextUtil.StringFromArrayForPersistence(BulletPointSeparator, jobAd.Description.BulletPoints == null ? null : jobAd.Description.BulletPoints.ToArray());
            entity.jobTypes = (byte) jobAd.Description.JobTypes;
            entity.packageDetails = jobAd.Description.Package;
            entity.positionTitle = jobAd.Description.PositionTitle;
            entity.residencyRequired = jobAd.Description.ResidencyRequired;
            entity.minSalary = jobAd.Description.Salary == null ? null : jobAd.Description.Salary.LowerBound;
            entity.maxSalary = jobAd.Description.Salary == null ? null : jobAd.Description.Salary.UpperBound;
            entity.minParsedSalary = jobAd.Description.ParsedSalary == null ? null : jobAd.Description.ParsedSalary.LowerBound;
            entity.maxParsedSalary = jobAd.Description.ParsedSalary == null ? null : jobAd.Description.ParsedSalary.UpperBound;
            entity.salaryRateType =
                (byte)
                (jobAd.Description.Salary == null || jobAd.Description.Salary.IsEmpty
                     ? jobAd.Description.ParsedSalary == null || jobAd.Description.ParsedSalary.IsEmpty
                           ? 0
                           : jobAd.Description.ParsedSalary.Rate
                     : jobAd.Description.Salary.Rate);
            entity.JobAdIndustryEntities = jobAd.Description.Industries.Map(jobAd.Id);

            // Update the location in place if able.

            if (jobAd.Description.Location != null)
            {
                if (entity.JobAdLocationEntities == null || entity.JobAdLocationEntities.Count != 1)
                    entity.JobAdLocationEntities = jobAd.Description.Location.Map(jobAd.Id);
                else
                    jobAd.Description.Location.MapTo(entity.JobAdLocationEntities[0].LocationReferenceEntity);
            }
        }

        private static EntitySet<JobAdIndustryEntity> Map(this ICollection<Industry> industries, Guid jobAdId)
        {
            if (industries == null || industries.Count == 0)
                return null;

            var set = new EntitySet<JobAdIndustryEntity>();
            set.AddRange(from i in industries select new JobAdIndustryEntity {jobAdId = jobAdId, industryId = i.Id});
            return set;
        }

        public static JobPosterEntity Map(this JobPoster poster)
        {
            var entity = new JobPosterEntity
            {
                id = poster.Id,
            };
            poster.MapTo(entity);
            return entity;
        }

        public static void MapTo(this JobPoster poster, JobPosterEntity entity)
        {
            var flags = new JobPosterFlags();
            if (poster.SendSuggestedCandidates)
                flags = flags.SetFlag(JobPosterFlags.SendSuggestedCandidatesForAdsPosted);
            if (poster.ShowSuggestedCandidates)
                flags = flags.SetFlag(JobPosterFlags.ShowSuggestedCandidatesPage);
            entity.flags = (byte) flags;
        }

        public static JobPoster Map(this JobPosterEntity entity)
        {
            return new JobPoster
            {
                Id = entity.id,
                SendSuggestedCandidates = ((JobPosterFlags)entity.flags).IsFlagSet(JobPosterFlags.SendSuggestedCandidatesForAdsPosted),
                ShowSuggestedCandidates = ((JobPosterFlags)entity.flags).IsFlagSet(JobPosterFlags.ShowSuggestedCandidatesPage),
            };
        }

        public static JobAdViewingEntity Map(this JobAdViewing viewing)
        {
            return new JobAdViewingEntity
            {
                id = viewing.Id,
                time = viewing.Time,
                viewerId = viewing.ViewerId,
                jobAdId = viewing.JobAdId,
            };
        }

        public static JobAdStatusEntity Map(this JobAdStatusChange change, Guid jobAdId)
        {
            var entity = new JobAdStatusEntity
            {
                id = change.Id,
                jobAdId = jobAdId,
            };

            change.MapTo(entity);
            return entity;
        }

        public static void MapTo(this JobAdStatusChange change, JobAdStatusEntity entity)
        {
            entity.previousStatus = (byte) change.PreviousStatus;
            entity.newStatus = (byte) change.NewStatus;
            entity.time = change.Time;
        }

        public static JobAdStatusChange Map(this JobAdStatusEntity entity)
        {
            return new JobAdStatusChange
            {
                Id = entity.id,
                Time = entity.time,
                PreviousStatus = (JobAdStatus) entity.previousStatus,
                NewStatus = (JobAdStatus) entity.newStatus,
            };
        }

        public static T MapTo<T>(this JobAdListEntity entity)
            where T : JobAdList, new()
        {
            if (entity == null)
                return null;

            return new T
            {
                Id = entity.id,
                Name = entity.name,
                OwnerId = entity.ownerId,
                IsDeleted = entity.isDeleted,
                CreatedTime = entity.createdTime,
                ListType = entity.listType,
            };
        }

        public static JobAdListEntity Map(this JobAdList list)
        {
            var entity = new JobAdListEntity { id = list.Id };
            list.MapTo(entity);
            return entity;
        }

        public static void MapTo(this JobAdList list, JobAdListEntity entity)
        {
            entity.name = list.Name;
            entity.ownerId = list.OwnerId;
            entity.isDeleted = list.IsDeleted;
            entity.createdTime = list.CreatedTime;
            entity.listType = list.ListType;
        }

        public static T MapTo<T>(this JobAdListEntryEntity entity)
            where T : JobAdListEntry, new()
        {
            if (entity == null)
                return null;

            return new T
            {
                ListId = entity.jobAdListId,
                CreatedTime = entity.createdTime,
                JobAdId = entity.jobAdId,
            };
        }

        public static JobAdListEntryEntity Map(this JobAdListEntry entry)
        {
            return new JobAdListEntryEntity
            {
                jobAdListId = entry.ListId,
                jobAdId = entry.JobAdId,
                createdTime = entry.CreatedTime,
            };
        }

        public static JobAdNoteEntity Map(this JobAdNote note)
        {
            var entity = new JobAdNoteEntity
            {
                id = note.Id,
                createdTime = note.CreatedTime,
                jobAdId = note.JobAdId,
            };
            note.MapTo(entity);

            return entity;
        }

        public static void MapTo(this JobAdNote note, JobAdNoteEntity entity)
        {
            entity.lastUpdatedTime = note.UpdatedTime;
            entity.text = note.Text;
            entity.ownerId = note.OwnerId;
        }


        public static T MapTo<T>(this JobAdNoteEntity entity) where T : JobAdNote, new()
        {
            if (entity == null)
                return null;

            return new T
               {
                   Id = entity.id,
                   CreatedTime = entity.createdTime,
                   JobAdId = entity.jobAdId,
                   UpdatedTime = entity.lastUpdatedTime,
                   Text = entity.text,
                   OwnerId = entity.ownerId,
               };
        }
    }
}
