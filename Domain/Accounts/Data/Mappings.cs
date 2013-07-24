namespace LinkMe.Domain.Accounts.Data
{
    internal enum UserFlags
    {
        None = 0x00,
        MustChangePassword = 0x02,
        Disabled = 0x04,
        Activated = 0x20,
    }

    internal static class Mappings
    {
        public static UserAccountAction Map(this UserEnablementEntity entity)
        {
            return new UserAccountAction
            {
                UserId = entity.userId,
                ActionedById = entity.enabledById,
                Time = entity.time
            };
        }

        public static UserAccountAction Map(this UserDisablementEntity entity)
        {
            return new UserAccountAction
            {
                UserId = entity.userId,
                ActionedById = entity.disabledById,
                Time = entity.time
            };
        }

        public static UserAccountAction Map(this UserActivationEntity entity)
        {
            return new UserAccountAction
            {
                UserId = entity.userId,
                ActionedById = entity.activatedById,
                Time = entity.time
            };
        }

        public static UserAccountAction Map(this UserDeactivationEntity entity)
        {
            return new UserAccountAction
            {
                UserId = entity.userId,
                ActionedById = entity.deactivatedById,
                Time = entity.time
            };
        }

        public static string GetReason(DeactivationReason? reason)
        {
            if (reason == null)
                return null;

            // Translates reasons into equivalent old descriptions in database.

            switch (reason.Value)
            {
                case DeactivationReason.Employer:
                    return "HideFromEmployer";

                case DeactivationReason.NotLooking:
                    return "NoLongerLooking";

                case DeactivationReason.Emails:
                    return "EmailSpam";

                case DeactivationReason.JobAlerts:
                    return "JobAlertSpam";

                case DeactivationReason.NotUseful:
                    return "NotUseful";

                case DeactivationReason.NoJobFound:
                    return "NotFoundJob";

                case DeactivationReason.NoContactsFound:
                    return "NotBuiltNetwork";

                case DeactivationReason.DifferentLogin:
                    return "Rejoining";

                default:
                    return "Other";
            }
        }
    }
}
