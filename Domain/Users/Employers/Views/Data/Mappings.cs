namespace LinkMe.Domain.Users.Employers.Views.Data
{
    internal static class Mappings
    {
        public static MemberViewingEntity Map(this MemberViewing viewing)
        {
            return new MemberViewingEntity
            {
                id = viewing.Id,
                time = viewing.Time,
                employerId = viewing.EmployerId,
                memberId = viewing.MemberId,
                jobAdId = viewing.JobAdId,
                channelId = viewing.ChannelId,
                appId = viewing.AppId,
            };
        }

        public static MemberViewing Map(this MemberViewingEntity entity)
        {
            return new MemberViewing
            {
                Id = entity.id,
                Time = entity.time,
                EmployerId = entity.employerId,
                MemberId = entity.memberId,
                JobAdId = entity.jobAdId,
                ChannelId = entity.channelId,
                AppId = entity.appId,
            };
        }

        public static MemberContactEntity Map(this MemberAccess access, bool isPartOfBulkOperation)
        {
            return new MemberContactEntity
            {
                id = access.Id,
                time = access.Time,
                reason = (int) access.Reason,
                employerId = access.EmployerId,
                memberId = access.MemberId,
                exercisedCreditId = access.ExercisedCreditId,
                partOfBulk = isPartOfBulkOperation,
                channelId = access.ChannelId,
                appId = access.AppId,
            };
        }

        public static MemberAccess Map(this MemberContactEntity entity)
        {
            return new MemberAccess
            {
                Id = entity.id,
                Time = entity.time,
                Reason = (MemberAccessReason)entity.reason,
                EmployerId = entity.employerId,
                MemberId = entity.memberId,
                ExercisedCreditId = entity.exercisedCreditId,
                ChannelId = entity.channelId,
                AppId = entity.appId,
            };
        }
   }
}