using System;

namespace LinkMe.Domain.Roles.Communications.Settings
{
    public static class PublishedEvents
    {
        public const string CategoryFrequencyUpdated = "LinkMe.Domain.Roles.Communications.Settings.CategoryFrequencyUpdated";
    }

    public class CategoryFrequencyEventArgs
        : EventArgs
    {
        public readonly Guid UserId;
        public readonly Guid CategoryId;
        public readonly Frequency Frequency;

        public CategoryFrequencyEventArgs(Guid userId, Guid categoryId, Frequency frequency)
        {
            UserId = userId;
            CategoryId = categoryId;
            Frequency = frequency;
        }
    }
}
