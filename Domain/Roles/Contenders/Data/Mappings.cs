using System;

namespace LinkMe.Domain.Roles.Contenders.Data
{
    internal static class Mappings
    {
        public static T MapTo<T>(this CandidateListEntity entity)
            where T : ContenderList, new()
        {
            if (entity == null)
                return null;

            return new T
            {
                Id = entity.id,
                Name = entity.name,
                OwnerId = entity.ownerId,
                SharedWithId = entity.sharedWithId,
                CreatedTime = entity.createdTime,
                IsDeleted = entity.isDeleted,
                ListType = entity.listType,
            };
        }

        public static CandidateListEntity Map(this ContenderList list)
        {
            var entity = new CandidateListEntity {id = list.Id};
            list.MapTo(entity);
            return entity;
        }

        public static void MapTo(this ContenderList list, CandidateListEntity entity)
        {
            entity.name = list.Name;
            entity.ownerId = list.OwnerId;
            entity.sharedWithId = list.SharedWithId;
            entity.createdTime = list.CreatedTime;
            entity.isDeleted = list.IsDeleted;
            entity.listType = list.ListType;
        }

        public static T MapTo<T>(this CandidateListEntryEntity entity)
            where T : ContenderListEntry, new()
        {
            if (entity == null)
                return null;

            return new T
            {
                ListId = entity.candidateListId,
                CreatedTime = entity.createdTime,
                ContenderId = entity.candidateId,
                ApplicationId = entity.jobApplicationId,
                ApplicantStatus = (ApplicantStatus?)entity.jobApplicationStatus,
            };
        }

        public static CandidateListEntryEntity Map(this ContenderListEntry entry, Guid listId)
        {
            return new CandidateListEntryEntity
            {
                candidateListId = listId,
                candidateId = entry.ContenderId,
                createdTime = entry.CreatedTime,
                jobApplicationId = entry.ApplicationId,
                jobApplicationStatus = (byte)entry.ApplicantStatus,
            };
        }

        public static InternalApplication Map(this JobApplicationEntity entity)
        {
            return new InternalApplication
            {
                Id = entity.id,
                ApplicantId = entity.applicantId,
                CoverLetterText = entity.coverLetterText,
                CreatedTime = entity.createdTime,
                PositionId = entity.jobAdId,
                ResumeId = entity.resumeId,
                ResumeFileId = entity.resumeAttachmentFileId,
                IsPending = entity.isPending,
                IsPositionFeatured = entity.isFeatured,
            };
        }

        public static JobApplicationEntity Map(this InternalApplication application)
        {
            var entity = new JobApplicationEntity { id = application.Id };
            application.MapTo(entity);
            return entity;
        }

        public static void MapTo(this InternalApplication application, JobApplicationEntity entity)
        {
            entity.id = application.Id;
            entity.applicantId = application.ApplicantId;
            entity.coverLetterText = application.CoverLetterText;
            entity.createdTime = application.CreatedTime;
            entity.jobAdId = application.PositionId;
            entity.resumeId = application.ResumeId;
            entity.resumeAttachmentFileId = application.ResumeFileId;
            entity.isPending = application.IsPending;
            entity.isFeatured = application.IsPositionFeatured;
        }

        public static ExternalApplicationEntity Map(this ExternalApplication application)
        {
            var entity = new ExternalApplicationEntity {id = application.Id};
            application.MapTo(entity);
            return entity;
        }

        public static void MapTo(this ExternalApplication application, ExternalApplicationEntity entity)
        {
            entity.createdTime = application.CreatedTime;
            entity.positionId = application.PositionId;
            entity.applicantId = application.ApplicantId;
        }

        public static ExternalApplication Map(this ExternalApplicationEntity entity)
        {
            return new ExternalApplication
            {
                Id = entity.id,
                ApplicantId = entity.applicantId,
                CreatedTime = entity.createdTime,
                PositionId = entity.positionId,
            };
        }

        public static CandidateNoteEntity Map(this ContenderNote note)
        {
            var entity = new CandidateNoteEntity
            {
                id = note.Id,
                createdTime = note.CreatedTime,
                candidateId = note.ContenderId,
                searcherId = note.OwnerId,
                sharedWithId = note.SharedWithId,
            };
            note.MapTo(entity);
            return entity;
        }

        public static void MapTo(this ContenderNote note, CandidateNoteEntity entity)
        {
            entity.lastUpdatedTime = note.UpdatedTime;
            entity.text = note.Text;
            entity.sharedWithId = note.SharedWithId;
        }

        public static T MapTo<T>(this CandidateNoteEntity entity)
            where T : ContenderNote, new()
        {
            if (entity == null)
                return null;

            return new T
            {
                Id = entity.id,
                CreatedTime = entity.createdTime,
                UpdatedTime = entity.lastUpdatedTime,
                Text = entity.text,
                ContenderId = entity.candidateId,
                OwnerId = entity.searcherId,
                SharedWithId = entity.sharedWithId,
            };
        }
    }
}