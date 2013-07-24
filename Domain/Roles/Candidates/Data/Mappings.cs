using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;

namespace LinkMe.Domain.Roles.Candidates.Data
{
    internal partial class LocationReferenceEntity
        : ILocationReferenceEntity
    {
    }

    internal static class Mappings
    {
        public static CandidateEntity Map(this Candidate candidate)
        {
            var entity = new CandidateEntity { id = candidate.Id };
            candidate.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Candidate candidate, CandidateEntity entity)
        {
            entity.status = (byte) candidate.Status;
            entity.lastEditedTime = candidate.LastUpdatedTime;

            entity.desiredJobTitle = candidate.DesiredJobTitle;
            entity.desiredJobTypes = (byte) candidate.DesiredJobTypes;
            entity.desiredSalaryLower = candidate.DesiredSalary == null ? null : candidate.DesiredSalary.LowerBound;
            entity.desiredSalaryUpper = candidate.DesiredSalary == null ? null : candidate.DesiredSalary.UpperBound;
            entity.desiredSalaryRateType = (byte) (candidate.DesiredSalary == null ? default(SalaryRate) : candidate.DesiredSalary.Rate);
            entity.relocationPreference = (byte) candidate.RelocationPreference;
            entity.highestEducationLevel = (byte?) candidate.HighestEducationLevel;
            entity.recentSeniority = (byte?) candidate.RecentSeniority;
            entity.recentProfession = (byte?) candidate.RecentProfession;
            entity.visaStatus = (byte?) candidate.VisaStatus;
        }

        public static Candidate Map(this CandidateEntity entity, Guid? resumeId, IIndustriesQuery industriesQuery)
        {
            return new Candidate
            {
                Id = entity.id,
                LastUpdatedTime = entity.lastEditedTime,
                Status = (CandidateStatus) entity.status,
                
                DesiredJobTitle = entity.desiredJobTitle,
                DesiredJobTypes = (JobTypes) entity.desiredJobTypes,
                DesiredSalary = (entity.desiredSalaryLower == null && entity.desiredSalaryUpper == null)
                    ? null
                    : new Salary { LowerBound = entity.desiredSalaryLower, UpperBound = entity.desiredSalaryUpper, Rate = (SalaryRate) entity.desiredSalaryRateType, Currency = Currency.AUD },
                    
                RelocationPreference = (RelocationPreference) entity.relocationPreference,
                
                HighestEducationLevel = (EducationLevel?) entity.highestEducationLevel,
                RecentSeniority = (Seniority?) entity.recentSeniority,
                RecentProfession = (Profession?) entity.recentProfession,
                VisaStatus = (VisaStatus?) entity.visaStatus,

                ResumeId = resumeId,
            };
        }

        public static EntitySet<RelocationLocationEntity> Map(this IEnumerable<LocationReference> relocationLocations, Guid candidateId)
        {
            var set = new EntitySet<RelocationLocationEntity>();
            set.AddRange(from l in relocationLocations select new RelocationLocationEntity { id = Guid.NewGuid(), candidateId = candidateId, LocationReferenceEntity = l.MapTo<LocationReferenceEntity>() });
            return set;
        }

        public static EntitySet<CandidateIndustryEntity> Map(this IEnumerable<Industry> industries, Guid candidateId)
        {
            var set = new EntitySet<CandidateIndustryEntity>();
            set.AddRange(from i in industries select new CandidateIndustryEntity { industryId = i.Id, candidateId = candidateId });
            return set;
        }

        public static CandidateDiaryEntity Map(this Diary diary)
        {
            return new CandidateDiaryEntity
            {
                id = diary.Id
            };
        }

        public static Diary Map(this CandidateDiaryEntity entity)
        {
            return new Diary
            {
                Id = entity.id
            };
        }

        public static DiaryEntry Map(this CandidateDiaryEntryEntity entity)
        {
            return new DiaryEntry
            {
                Id = entity.id,
                IsDeleted = entity.deleted,
                Title = entity.title,
                Description = entity.description,
                StartTime = entity.startTime,
                EndTime = entity.endTime,
                TotalHours = entity.totalHours,
            };
        }

        public static CandidateDiaryEntryEntity Map(this DiaryEntry entry, Guid diaryId)
        {
            var entity = new CandidateDiaryEntryEntity
            {
                id = entry.Id,
                diaryId = diaryId,
                deleted = false,
            };

            entry.MapTo(entity);
            return entity;
        }

        public static void MapTo(this DiaryEntry entry, CandidateDiaryEntryEntity entity)
        {
            entity.title = entry.Title;
            entity.description = entry.Description;
            entity.startTime = entry.StartTime;
            entity.endTime = entry.EndTime;
            entity.totalHours = entry.TotalHours;
        }

        public static ResumeFileReference Map(this CandidateResumeFileEntity entity)
        {
            return new ResumeFileReference
            {
                Id = entity.id,
                FileReferenceId = entity.fileId,
                LastUsedTime = entity.lastUsedTime,
                UploadedTime = entity.uploadedTime,
            };
        }

        public static CandidateResumeFileEntity Map(this ResumeFileReference resumeFileReference, Guid candidateId)
        {
            var entity = new CandidateResumeFileEntity
            {
                id = resumeFileReference.Id,
                candidateId = candidateId,
            };
            resumeFileReference.MapTo(entity);
            return entity;
        }

        public static void MapTo(this ResumeFileReference resumeFileReference, CandidateResumeFileEntity entity)
        {
            entity.fileId = resumeFileReference.FileReferenceId;
            entity.lastUsedTime = resumeFileReference.LastUsedTime;
            entity.uploadedTime = resumeFileReference.UploadedTime;
        }
    }
}
