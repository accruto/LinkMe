using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Communications.Settings.Commands
{
    public class SettingsCommand
        : ISettingsCommand
    {
        private readonly ISettingsRepository _repository;

        [Publishes(PublishedEvents.CategoryFrequencyUpdated)]
        public event EventHandler<CategoryFrequencyEventArgs> CategoryFrequencyUpdated;

        public SettingsCommand(ISettingsRepository repository)
        {
            _repository = repository;
        }

        void ISettingsCommand.CreateDefinition(Definition definition)
        {
            definition.Prepare();
            definition.Validate();
            _repository.CreateDefinition(definition);
        }

        void ISettingsCommand.DeleteSettings(Guid recipientId)
        {
            _repository.DeleteSettings(recipientId);
        }

        void ISettingsCommand.SetFrequency(Guid recipientId, Guid categoryId, Frequency frequency)
        {
            _repository.SetFrequency(recipientId, categoryId, frequency);

            // Fire events.

            var handlers = CategoryFrequencyUpdated;
            if (handlers != null)
                handlers(this, new CategoryFrequencyEventArgs(recipientId, categoryId, frequency));
        }

        void ISettingsCommand.SetLastSentTime(Guid recipientId, Guid definitionId, DateTime? lastSentTime)
        {
            _repository.SetLastSentTime(recipientId, definitionId, lastSentTime);
        }
    }
}