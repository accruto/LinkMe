using System;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public interface IJobAdExportCommand
    {
        void CreateJobSearchId(Guid jobAdId, long vacancyId);
        long? GetJobSearchId(Guid jobAdId);
        void DeleteJobSearchId(Guid jobAdId);
    }
}