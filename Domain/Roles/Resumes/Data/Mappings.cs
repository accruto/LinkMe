using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Data;

namespace LinkMe.Domain.Roles.Resumes.Data
{
    internal partial class ResumeJobEntity
        : IHavePartialDateRangeEntity
    {
    }

    internal partial class ResumeSchoolEntity
        : IHavePartialCompletionDateEntity
    {
    }

    internal partial class ParsedResumeEntity
        : IHavePartialDateEntity, IHaveEmailAddressesEntity, IHavePhoneNumbersEntity
    {
        DateTime? IHavePartialDateEntity.date
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        byte? IHavePartialDateEntity.dateParts
        {
            get { return dateOfBirthParts; }
            set { dateOfBirthParts = value; }
        }

        string IHavePhoneNumbersEntity.primaryPhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        byte? IHavePhoneNumbersEntity.primaryPhoneNumberType
        {
            get { return phoneNumberType; }
            set { phoneNumberType = value; }
        }

        string IHavePhoneNumbersEntity.secondaryPhoneNumber
        {
            get { return secondaryPhoneNumber; }
            set { secondaryPhoneNumber = value; }
        }

        byte? IHavePhoneNumbersEntity.secondaryPhoneNumberType
        {
            get { return secondaryPhoneNumberType; }
            set { secondaryPhoneNumberType = value; }
        }

        string IHavePhoneNumbersEntity.tertiaryPhoneNumber
        {
            get { return null; }
            set { }
        }

        byte? IHavePhoneNumbersEntity.tertiaryPhoneNumberType
        {
            get { return null; }
            set { }
        }

        string IHaveEmailAddressesEntity.primaryEmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        bool IHaveEmailAddressesEntity.primaryEmailAddressVerified
        {
            get { return false; }
            set { }
        }

        string IHaveEmailAddressesEntity.secondaryEmailAddress
        {
            get { return secondaryEmailAddress; }
            set { secondaryEmailAddress = value; }
        }

        bool? IHaveEmailAddressesEntity.secondaryEmailAddressVerified
        {
            get { return null; }
            set { }
        }
    }

    internal static class Mappings
    {
        public static ResumeEntity Map(this Resume resume)
        {
            var entity = new ResumeEntity { id = resume.Id };
            resume.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Resume resume, ResumeEntity entity)
        {
            entity.createdTime = resume.CreatedTime;
            entity.lastEditedTime = resume.LastUpdatedTime;
            entity.affiliations = resume.Affiliations;
            entity.citizenship = resume.Citizenship;
            entity.interests = resume.Interests;
            entity.objective = resume.Objective;
            entity.other = resume.Other;
            entity.professional = resume.Professional;
            entity.referees = resume.Referees;
            entity.skills = resume.Skills;
            entity.summary = resume.Summary;
            entity.ResumeJobEntities = resume.Jobs.Map(resume.Id);
            entity.ResumeSchoolEntities = resume.Schools.Map(resume.Id);

            var awards = resume.Awards == null
                ? null
                : (from a in resume.Awards where a != null && !string.IsNullOrEmpty(a.Trim()) select a).ToArray();
            entity.awards = awards == null || awards.Length == 0 ? null : string.Join("\n", awards);

            var courses = resume.Courses == null
                ? null
                : (from c in resume.Courses where c != null && !string.IsNullOrEmpty(c.Trim()) select c).ToArray();
            entity.courses = courses == null || courses.Length == 0 ? null : string.Join("\n", courses);
        }

        public static Resume Map(this ResumeEntity entity)
        {
            return new Resume
            {
                Id = entity.id,
                CreatedTime = entity.createdTime,
                LastUpdatedTime = entity.lastEditedTime,
                Affiliations = entity.affiliations,
                Citizenship = entity.citizenship,
                Interests = entity.interests,
                Objective = entity.objective,
                Other = entity.other,
                Professional = entity.professional,
                Referees = entity.referees,
                Skills = entity.skills,
                Summary = entity.summary,
                Awards = entity.awards == null ? null : entity.awards.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries),
                Courses = entity.courses == null ? null : entity.courses.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries),
            };
        }

        public static ParsedResumeEntity Map(this ParsedResume parsedResume)
        {
            var entity = new ParsedResumeEntity
            {
                id = parsedResume.Id,
                firstName = parsedResume.FirstName,
                lastName = parsedResume.LastName,
                street = parsedResume.Address == null ? null : parsedResume.Address.Street,
                location = parsedResume.Address == null ? null : parsedResume.Address.Location,
                ResumeEntity = parsedResume.Resume == null ? null : parsedResume.Resume.Map()
            };

            parsedResume.MapTo((IHavePhoneNumbersEntity)entity);
            parsedResume.MapTo((IHaveEmailAddressesEntity)entity);
            parsedResume.DateOfBirth.MapTo(entity);
            return entity;
        }

        public static ParsedResume Map(this ParsedResumeEntity entity)
        {
            var parsedResume = new ParsedResume
            {
                Id = entity.id,
                FirstName = entity.firstName,
                LastName = entity.lastName,
                Address = string.IsNullOrEmpty(entity.street) && string.IsNullOrEmpty(entity.location)
                    ? null
                    : new ParsedAddress { Street = entity.street, Location = entity.location },
                DateOfBirth = ((IHavePartialDateEntity)entity).Map(),
                Resume = entity.ResumeEntity == null ? null : entity.ResumeEntity.Map()
            };

            entity.MapTo((IHavePhoneNumbers)parsedResume);
            entity.MapTo((IHaveEmailAddresses)parsedResume);
            return parsedResume;
        }

        private static EntitySet<ResumeJobEntity> Map(this ICollection<Job> jobs, Guid resumeId)
        {
            if (jobs == null || jobs.Count == 0)
                return null;

            var set = new EntitySet<ResumeJobEntity>();
            set.AddRange(from j in jobs select j.Map(resumeId));
            return set;
        }

        private static ResumeJobEntity Map(this IJob job, Guid resumeId)
        {
            var entity = new ResumeJobEntity
            {
                id = job.Id,
                resumeId = resumeId,
                company = job.Company,
                description = job.Description,
                title = job.Title,
            };

            job.Dates.MapTo(entity);
            return entity;
        }

        public static Job Map(this ResumeJobEntity entity)
        {
            return new Job
            {
                Id = entity.id,
                Company = entity.company,
                Description = entity.description,
                Title = entity.title,
                Dates = ((IHavePartialDateRangeEntity)entity).Map(),
            };
        }

        private static EntitySet<ResumeSchoolEntity> Map(this ICollection<School> schools, Guid resumeId)
        {
            if (schools == null || schools.Count == 0)
                return null;

            var set = new EntitySet<ResumeSchoolEntity>();
            set.AddRange(from j in schools select j.Map(resumeId));
            return set;
        }

        private static ResumeSchoolEntity Map(this ISchool school, Guid resumeId)
        {
            var entity = new ResumeSchoolEntity
            {
                id = school.Id,
                resumeId = resumeId,
                city = school.City,
                country = school.Country,
                degree = school.Degree,
                description = school.Description,
                institution = school.Institution,
                major = school.Major,
            };

            school.CompletionDate.MapTo(entity);
            return entity;
        }

        public static School Map(this ResumeSchoolEntity entity)
        {
            return new School
            {
                Id = entity.id,
                City = entity.city,
                Country = entity.country,
                Degree = entity.degree,
                Description = entity.description,
                Institution = entity.institution,
                Major = entity.major,
                CompletionDate = ((IHavePartialCompletionDateEntity)entity).Map(),
            };
        }
    }
}
