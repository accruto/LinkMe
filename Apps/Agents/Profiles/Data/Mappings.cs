using System;

namespace LinkMe.Apps.Agents.Profiles.Data
{
    public interface IUserProfileEntity
    {
        DateTime? updatedTermsReminderTime { get; set; }
        bool hideUpdatedTermsReminder { get; set; }
    }

    internal partial class MemberProfileEntity
        : IUserProfileEntity
    {
    }

    internal partial class EmployerProfileEntity
        : IUserProfileEntity
    {
    }

    internal static class Mappings
    {
        public static EmployerProfile Map(this EmployerProfileEntity entity)
        {
            var profile = new EmployerProfile
            {
                HideCreditReminder = entity.hideCreditReminder,
                HideBulkCreditReminder = entity.hideBulkCreditReminder,
            };
            entity.MapTo(profile);
            return profile;
        }

        public static EmployerProfileEntity Map(this EmployerProfile profile, Guid employerId)
        {
            var entity = new EmployerProfileEntity { employerId = employerId };
            profile.MapTo(entity);
            return entity;
        }

        public static void MapTo(this EmployerProfile profile, EmployerProfileEntity entity)
        {
            entity.hideCreditReminder = profile.HideCreditReminder;
            entity.hideBulkCreditReminder = profile.HideBulkCreditReminder;
            ((UserProfile)profile).MapTo(entity);
        }

        public static MemberProfile Map(this MemberProfileEntity entity)
        {
            var profile = new MemberProfile
            {
                UpdateStatusReminder = { FirstShownTime = entity.updateProfileReminderTime, Hide = entity.hideUpdateProfileReminder },
            };
            entity.MapTo(profile);
            return profile;
        }

        public static MemberProfileEntity Map(this MemberProfile profile, Guid memberId)
        {
            var entity = new MemberProfileEntity { memberId = memberId };
            profile.MapTo(entity);
            return entity;
        }

        public static void MapTo(this MemberProfile profile, MemberProfileEntity entity)
        {
            entity.updateProfileReminderTime = profile.UpdateStatusReminder.FirstShownTime;
            entity.hideUpdateProfileReminder = profile.UpdateStatusReminder.Hide;
            ((UserProfile)profile).MapTo(entity);
        }

        private static void MapTo(this IUserProfileEntity entity, UserProfile profile)
        {
            profile.UpdatedTermsReminder.FirstShownTime = entity.updatedTermsReminderTime;
            profile.UpdatedTermsReminder.Hide = entity.hideUpdatedTermsReminder;
        }

        private static void MapTo(this UserProfile profile, IUserProfileEntity entity)
        {
            entity.updatedTermsReminderTime = profile.UpdatedTermsReminder.FirstShownTime;
            entity.hideUpdatedTermsReminder = profile.UpdatedTermsReminder.Hide;
        }
    }
}
