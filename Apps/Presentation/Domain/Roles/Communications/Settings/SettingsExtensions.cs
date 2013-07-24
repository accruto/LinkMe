using LinkMe.Domain.Roles.Communications.Settings;

namespace LinkMe.Apps.Presentation.Domain.Roles.Communications.Settings
{
    public static class SettingsExtensions
    {
        public static string GetDisplayText(this Category category)
        {
            switch (category.Name)
            {
                // Members - Regulars

                case "MemberAlert":
                    return "Job alerts";

                case "MemberUpdate":
                    return "My LinkMe newsletter";

                case "SuggestedJobs":
                    return "Suggested jobs";

                // Members - Notifications

                case "MemberToMemberNotification":
                    return "From other members";

                case "Campaign":
                    return "General notices";

                case "MemberGroupNotification":
                    return "Related to groups you have joined";

                case "EmployerToMemberNotification":
                    return "From employers";

                case "PartnerNotification":
                    return "From selected LinkMe partners";

                // Employers

                case "EmployerNotification":
                    return "Notifications to employers";

                case "EmployerUpdate":
                    return "Consultant update";

                default:
                    return category.Name;
            }
        }
    }
}
