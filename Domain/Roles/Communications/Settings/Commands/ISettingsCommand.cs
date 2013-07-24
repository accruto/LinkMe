using System;

namespace LinkMe.Domain.Roles.Communications.Settings.Commands
{
    public interface ISettingsCommand
    {
        void CreateDefinition(Definition definition);

        void DeleteSettings(Guid recipientId);
        void SetFrequency(Guid recipientId, Guid categoryId, Frequency frequency);
        void SetLastSentTime(Guid recipientId, Guid definitionId, DateTime? lastSentTime);
    }
}