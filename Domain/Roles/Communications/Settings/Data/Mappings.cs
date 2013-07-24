using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.Communications.Settings.Data
{
    [Flags]
    internal enum NonMemberFlags
    {
        None = 0,
        SuppressSuggestedCandidatesEmails = 1
    }

    internal static class Mappings
    {
        private static readonly Frequency[] AvailablePeriodicFrequencies = new[] { Frequency.Daily, Frequency.Weekly, Frequency.Monthly };
        private static readonly Frequency[] AvailableNotificationFrequencies = new[] { Frequency.Immediately };
        private static readonly Frequency[] NeverFrequency = new[] { Frequency.Never };

        public static Definition Map(this CommunicationDefinitionEntity entity)
        {
            return new Definition
            {
                Id = entity.id,
                Name = entity.name,
                CategoryId = entity.categoryId,
            };
        }

        public static CommunicationDefinitionEntity Map(this Definition definition)
        {
            return new CommunicationDefinitionEntity
            {
                id = definition.Id,
                name = definition.Name,
                categoryId = definition.CategoryId,
            };
        }

        public static Category Map(this CommunicationCategoryEntity entity)
        {
            return new Category
            {
                Id = entity.id,
                Name = entity.name,
                DefaultFrequency = (Frequency)entity.defaultFrequency,
                AvailableFrequencies = GetAvailableFrequencies(entity.availableFrequencies, (Timing)entity.type),
                Timing = (Timing)entity.type,
                UserTypes = (UserType)entity.roles
            };
        }

        public static RecipientSettings Map(this CommunicationSettingEntity entity)
        {
            return new RecipientSettings
            {
                Id = entity.id,
            };
        }

        public static CategorySettings Map(this CommunicationCategorySettingEntity entity)
        {
            return new CategorySettings
            {
                CategoryId = entity.categoryId,
                Frequency = (Frequency?) entity.frequency,
            };
        }

        public static DefinitionSettings Map(this CommunicationDefinitionSettingEntity entity)
        {
            return new DefinitionSettings
            {
                DefinitionId = entity.definitionId,
                LastSentTime = entity.lastSentTime,
            };
        }

        public static NonMemberSettingEntity Map(this NonMemberSettings settings)
        {
            var entity = new NonMemberSettingEntity {id = settings.Id};
            settings.MapTo(entity);
            return entity;
        }

        public static void MapTo(this NonMemberSettings settings, NonMemberSettingEntity entity)
        {
            var flags = new NonMemberFlags();
            if (settings.SuppressSuggestedCandidatesEmails)
                flags = flags.SetFlag(NonMemberFlags.SuppressSuggestedCandidatesEmails);

            entity.emailAddress = settings.EmailAddress;
            entity.flags = (byte) flags;
        }

        public static NonMemberSettings Map(this NonMemberSettingEntity entity)
        {
            return new NonMemberSettings
            {
                Id = entity.id,
                EmailAddress = entity.emailAddress,
                SuppressSuggestedCandidatesEmails = ((NonMemberFlags) entity.flags).IsFlagSet(NonMemberFlags.SuppressSuggestedCandidatesEmails),
            };
        }

        private static IList<Frequency> GetAvailableFrequencies(string frequencies, Timing timing)
        {
            if (string.IsNullOrEmpty(frequencies))
            {
                return timing == Timing.Periodic
                    ? AvailablePeriodicFrequencies.Concat(NeverFrequency).ToArray()
                    : AvailableNotificationFrequencies.Concat(NeverFrequency).ToArray();
            }

            var availableFrequencies = from f in frequencies.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                                       select (Frequency) int.Parse(f);

            return timing == Timing.Periodic
                ? AvailablePeriodicFrequencies.Intersect(availableFrequencies).Concat(NeverFrequency).ToArray()
                : AvailableNotificationFrequencies.Intersect(availableFrequencies).Concat(NeverFrequency).ToArray();
        }
    }
}
