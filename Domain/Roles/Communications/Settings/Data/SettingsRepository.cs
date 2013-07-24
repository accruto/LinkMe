using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Communications.Settings.Data
{
    public class SettingsRepository
        : Repository, ISettingsRepository
    {
        private static readonly Func<SettingsDataContext, IQueryable<Definition>> GetDefinitions
            = CompiledQuery.Compile((SettingsDataContext dc)
                => from d in dc.CommunicationDefinitionEntities
                   where !d.CommunicationCategoryEntity.deleted
                   orderby d.name
                   select d.Map());

        private static readonly Func<SettingsDataContext, int, IQueryable<Definition>> GetUserDefinitions
            = CompiledQuery.Compile((SettingsDataContext dc, int userType)
                => from d in dc.CommunicationDefinitionEntities
                   where !d.CommunicationCategoryEntity.deleted
                   && (d.CommunicationCategoryEntity.roles & userType) == userType
                   orderby d.name
                   select d.Map());

        private static readonly Func<SettingsDataContext, Guid, Definition> GetDefinition
            = CompiledQuery.Compile((SettingsDataContext dc, Guid id)
                => (from d in dc.CommunicationDefinitionEntities
                    where !d.CommunicationCategoryEntity.deleted
                    && d.id == id
                    select d.Map()).SingleOrDefault());

        private static readonly Func<SettingsDataContext, string, Definition> GetDefinitionByName
            = CompiledQuery.Compile((SettingsDataContext dc, string name)
                => (from d in dc.CommunicationDefinitionEntities
                    where !d.CommunicationCategoryEntity.deleted
                    && d.name == name
                    select d.Map()).SingleOrDefault());

        private static readonly Func<SettingsDataContext, IQueryable<Category>> GetCategories
            = CompiledQuery.Compile((SettingsDataContext dc)
                => from c in dc.CommunicationCategoryEntities
                   where !c.deleted
                   orderby c.name
                   select c.Map());

        private static readonly Func<SettingsDataContext, int, IQueryable<Category>> GetUserCategories
            = CompiledQuery.Compile((SettingsDataContext dc, int userType)
                => from c in dc.CommunicationCategoryEntities
                   where !c.deleted
                   && (c.roles & userType) == userType
                   orderby c.name
                   select c.Map());

        private static readonly Func<SettingsDataContext, Guid, Category> GetCategory
            = CompiledQuery.Compile((SettingsDataContext dc, Guid id)
                => (from c in dc.CommunicationCategoryEntities
                    where !c.deleted
                    && c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<SettingsDataContext, string, Category> GetCategoryByName
            = CompiledQuery.Compile((SettingsDataContext dc, string name)
                => (from c in dc.CommunicationCategoryEntities
                    where !c.deleted
                    && c.name == name
                    select c.Map()).SingleOrDefault());

        private static readonly Func<SettingsDataContext, Guid, RecipientSettings> GetSettingsQuery
            = CompiledQuery.Compile((SettingsDataContext dc, Guid recipientId)
                => (from s in dc.CommunicationSettingEntities
                    where s.userId == recipientId
                    select s.Map()).SingleOrDefault());

        private static readonly Func<SettingsDataContext, Guid, IQueryable<DefinitionSettings>> GetAllDefinitionSettings
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId)
                => from d in dc.CommunicationDefinitionSettingEntities
                   where d.settingsId == settingsId
                   && !d.CommunicationDefinitionEntity.CommunicationCategoryEntity.deleted
                   select d.Map());

        private static readonly Func<SettingsDataContext, Guid, Guid, DefinitionSettings> GetDefinitionSettings
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId, Guid definitionId)
                => (from d in dc.CommunicationDefinitionSettingEntities
                    where d.definitionId == definitionId 
                    && d.settingsId == settingsId
                    select d.Map()).SingleOrDefault());

        private static readonly Func<SettingsDataContext, Guid, IQueryable<CommunicationDefinitionSettingEntity>> GetDefinitionSettingEntities
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId)
                => from d in dc.CommunicationDefinitionSettingEntities
                   where d.settingsId == settingsId
                   select d);

        private static readonly Func<SettingsDataContext, Guid, IQueryable<CategorySettings>> GetAllCategorySettings
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId)
                => from c in dc.CommunicationCategorySettingEntities
                   where c.settingsId == settingsId
                   && !c.CommunicationCategoryEntity.deleted
                   orderby c.CommunicationCategoryEntity.name
                   select c.Map());

        private static readonly Func<SettingsDataContext, Guid, Guid, CategorySettings> GetCategorySettings
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId, Guid categoryId)
                => (from s in dc.CommunicationCategorySettingEntities
                    where s.categoryId == categoryId
                    && s.settingsId == settingsId
                    select s.Map()).SingleOrDefault());

        private static readonly Func<SettingsDataContext, Guid, IQueryable<CommunicationCategorySettingEntity>> GetCategorySettingEntities
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId)
                => from s in dc.CommunicationCategorySettingEntities
                   where s.settingsId == settingsId
                   select s);

        private static readonly Func<SettingsDataContext, Guid, bool> CategoryExists
            = CompiledQuery.Compile((SettingsDataContext dc, Guid categoryId)
                => (from c in dc.CommunicationCategoryEntities
                    where c.id == categoryId
                    select c).Any());

        private static readonly Func<SettingsDataContext, Guid, Guid, CommunicationDefinitionSettingEntity> GetDefinitionSettingEntity
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId, Guid definitionId)
                => (from d in dc.CommunicationDefinitionSettingEntities
                    where d.settingsId == settingsId
                    && d.definitionId == definitionId
                    select d).SingleOrDefault());

        private static readonly Func<SettingsDataContext, Guid, Guid, CommunicationCategorySettingEntity> GetCategorySettingEntity
            = CompiledQuery.Compile((SettingsDataContext dc, Guid settingsId, Guid categoryId)
                => (from c in dc.CommunicationCategorySettingEntities
                    where c.settingsId == settingsId
                    && c.categoryId == categoryId
                    select c).SingleOrDefault());

        private static readonly Func<SettingsDataContext, Guid, CommunicationSettingEntity> GetSettingEntity
            = CompiledQuery.Compile((SettingsDataContext dc, Guid recipientId)
                => (from s in dc.CommunicationSettingEntities
                    where s.userId == recipientId
                    select s).SingleOrDefault());

        private static readonly Func<SettingsDataContext, Guid, NonMemberSettingEntity> GetNonMemberSettingsEntity
            = CompiledQuery.Compile((SettingsDataContext dc, Guid id)
                => (from s in dc.NonMemberSettingEntities
                    where s.id == id
                    select s).SingleOrDefault());

        private static readonly Func<SettingsDataContext, string, NonMemberSettings> GetNonMemberSettings
            = CompiledQuery.Compile((SettingsDataContext dc, string emailAddress)
                => (from s in dc.NonMemberSettingEntities
                    where s.emailAddress == emailAddress
                    select s.Map()).SingleOrDefault());

        public SettingsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void ISettingsRepository.CreateDefinition(Definition definition)
        {
            using (var dc = CreateContext())
            {
                dc.CommunicationDefinitionEntities.InsertOnSubmit(definition.Map());
                dc.SubmitChanges();
            }
        }

        Definition ISettingsRepository.GetDefinition(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDefinition(dc, id);
            }
        }

        Definition ISettingsRepository.GetDefinition(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDefinitionByName(dc, name);
            }
        }

        IList<Definition> ISettingsRepository.GetDefinitions()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDefinitions(dc).ToList();
            }
        }

        IList<Definition> ISettingsRepository.GetDefinitions(UserType userType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUserDefinitions(dc, (int)userType).ToList();
            }
        }

        IList<Category> ISettingsRepository.GetCategories()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCategories(dc).ToList();
            }
        }

        IList<Category> ISettingsRepository.GetCategories(UserType userType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUserCategories(dc, (int)userType).ToList();
            }
        }

        Category ISettingsRepository.GetCategory(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCategory(dc, id);
            }
        }

        Category ISettingsRepository.GetCategory(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCategoryByName(dc, name);
            }
        }

        RecipientSettings ISettingsRepository.GetSettings(Guid recipientId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetSettings(dc, recipientId);
            }
        }

        RecipientSettings ISettingsRepository.GetSettings(Guid recipientId, Guid definitionId, Guid? categoryId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetSettings(dc, recipientId, definitionId, categoryId);
            }
        }

        void ISettingsRepository.DeleteSettings(Guid recipientId)
        {
            using (var dc = CreateContext())
            {
                // Look for the settings.

                var entity = GetSettingEntity(dc, recipientId);
                if (entity == null)
                    return;

                var definitionEntities = GetDefinitionSettingEntities(dc, entity.id);
                if (definitionEntities != null)
                    dc.CommunicationDefinitionSettingEntities.DeleteAllOnSubmit(definitionEntities);

                var categoryEntities = GetCategorySettingEntities(dc, entity.id);
                if (categoryEntities != null)
                    dc.CommunicationCategorySettingEntities.DeleteAllOnSubmit(categoryEntities);

                dc.CommunicationSettingEntities.DeleteOnSubmit(entity);
                dc.SubmitChanges();
            }
        }

        void ISettingsRepository.SetFrequency(Guid recipientId, Guid categoryId, Frequency frequency)
        {
            using (var dc = CreateContext())
            {
                // If no category exists then ignore.

                if (!CategoryExists(dc, categoryId))
                    return;

                // Make sure settings exist.

                var settings = EnsureSettingEntity(dc, recipientId);

                // Make sure category settings exist.

                var categorySettings = EnsureCategorySettingEntity(dc, settings, categoryId);
                categorySettings.frequency = (byte)frequency;
                dc.SubmitChanges();
            }
        }

        void ISettingsRepository.SetLastSentTime(Guid recipientId, Guid definitionId, DateTime? lastSentTime)
        {
            using (var dc = CreateContext())
            {
                // Make sure settings exist.

                var settings = EnsureSettingEntity(dc, recipientId);

                // Make sure definition settings exist.

                var definitionSettings = EnsureDefinitionSettingEntity(dc, settings, definitionId);
                definitionSettings.lastSentTime = lastSentTime;
                dc.SubmitChanges();
            }
        }

        void ISettingsRepository.CreateNonMemberSettings(NonMemberSettings settings)
        {
            using (var dc = CreateContext())
            {
                dc.NonMemberSettingEntities.InsertOnSubmit(settings.Map());
                dc.SubmitChanges();
            }
        }

        void ISettingsRepository.UpdateNonMemberSettings(NonMemberSettings settings)
        {
            using (var dc = CreateContext())
            {
                var entity = GetNonMemberSettingsEntity(dc, settings.Id);
                if (entity != null)
                {
                    settings.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        NonMemberSettings ISettingsRepository.GetNonMemberSettings(string emailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetNonMemberSettings(dc, emailAddress);
            }
        }

        private static CommunicationDefinitionSettingEntity EnsureDefinitionSettingEntity(SettingsDataContext dc, CommunicationSettingEntity settings, Guid definitionId)
        {
            var definitionSettings = GetDefinitionSettingEntity(dc, settings.id, definitionId);
            if (definitionSettings == null)
            {
                definitionSettings = new CommunicationDefinitionSettingEntity { id = Guid.NewGuid(), definitionId = definitionId, settingsId = settings.id };
                dc.CommunicationDefinitionSettingEntities.InsertOnSubmit(definitionSettings);
            }

            return definitionSettings;
        }

        private static CommunicationCategorySettingEntity EnsureCategorySettingEntity(SettingsDataContext dc, CommunicationSettingEntity settings, Guid categoryId)
        {
            var categorySettings = GetCategorySettingEntity(dc, settings.id, categoryId);
            if (categorySettings == null)
            {
                categorySettings = new CommunicationCategorySettingEntity { id = Guid.NewGuid(), categoryId = categoryId, settingsId = settings.id };
                dc.CommunicationCategorySettingEntities.InsertOnSubmit(categorySettings);
            }

            return categorySettings;
        }

        private static CommunicationSettingEntity EnsureSettingEntity(SettingsDataContext dc, Guid recipientId)
        {
            var settings = GetSettingEntity(dc, recipientId);
            if (settings == null)
            {
                settings = new CommunicationSettingEntity { id = Guid.NewGuid(), userId = recipientId };
                dc.CommunicationSettingEntities.InsertOnSubmit(settings);
            }

            return settings;
        }

        private static RecipientSettings GetSettings(SettingsDataContext dc, Guid recipientId)
        {
            // Look for the settings.

            var settings = GetSettingsQuery(dc, recipientId);
            if (settings == null)
                return null;

            // Load them all.
            
            settings.DefinitionSettings = GetAllDefinitionSettings(dc, settings.Id).ToList();
            settings.CategorySettings = GetAllCategorySettings(dc, settings.Id).ToList();
            return settings;
        }

        private static RecipientSettings GetSettings(SettingsDataContext dc, Guid recipientId, Guid definitionId, Guid? categoryId)
        {
            // Look for the settings.

            var settings = GetSettingsQuery(dc, recipientId);
            if (settings == null)
                return null;

            // Load the child definition settings.

            settings.DefinitionSettings = new List<DefinitionSettings>();
            settings.CategorySettings = new List<CategorySettings>();

            var definitionSettings = GetDefinitionSettings(dc, settings.Id, definitionId);
            if (definitionSettings != null)
                settings.DefinitionSettings.Add(definitionSettings);

            if (categoryId != null)
            {
                var categorySettings = GetCategorySettings(dc, settings.Id, categoryId.Value);
                if (categorySettings != null)
                    settings.CategorySettings.Add(categorySettings);
            }

            return settings;
        }

        private SettingsDataContext CreateContext()
        {
            return CreateContext(c => new SettingsDataContext(c));
        }
    }
}
