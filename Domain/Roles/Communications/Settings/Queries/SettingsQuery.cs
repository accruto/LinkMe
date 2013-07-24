using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Communications.Settings.Queries
{
    public class SettingsQuery
        : ISettingsQuery
    {
        private readonly ISettingsRepository _repository;

        public SettingsQuery(ISettingsRepository repository)
        {
            _repository = repository;
        }

        Category ISettingsQuery.GetCategory(Guid id)
        {
            return _repository.GetCategory(id);
        }

        Category ISettingsQuery.GetCategory(string name)
        {
            return string.IsNullOrEmpty(name) ? null : _repository.GetCategory(name);
        }

        IList<Category> ISettingsQuery.GetCategories()
        {
            return _repository.GetCategories();
        }

        IList<Category> ISettingsQuery.GetCategories(UserType userType)
        {
            return _repository.GetCategories(userType);
        }

        public Definition GetDefinition(Guid id)
        {
            return _repository.GetDefinition(id);
        }

        Definition ISettingsQuery.GetDefinition(string name)
        {
            return string.IsNullOrEmpty(name) ? null : _repository.GetDefinition(name);
        }

        IList<Definition> ISettingsQuery.GetDefinitions()
        {
            return _repository.GetDefinitions();
        }

        IList<Definition> ISettingsQuery.GetDefinitions(UserType userType)
        {
            return _repository.GetDefinitions(userType);
        }

        RecipientSettings ISettingsQuery.GetSettings(Guid recipientId)
        {
            return _repository.GetSettings(recipientId);
        }

        RecipientSettings ISettingsQuery.GetSettings(Guid recipientId, Guid definitionId, Guid? categoryId)
        {
            return _repository.GetSettings(recipientId, definitionId, categoryId);
        }
    }
}