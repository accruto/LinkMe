using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Apps.Agents.Communications.Emails.PartnerEmails;
using LinkMe.Apps.Agents.Communications.Emails.Queries;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Agents.Communications.Emails.Commands
{
    public class EmailsCommand
        : IEmailsCommand
    {
        private static readonly EventSource EventSource = new EventSource<EmailsCommand>();
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly ICommunicationEngine _communicationEngine;
        private readonly ITemplateEngine _templateEngine;
        private readonly IAffiliateEmailsQuery _affiliateEmailsQuery;
        private readonly IUserEmailsQuery _userEmailsQuery;

        private readonly ICommunicationUser _memberServicesUser;
        private readonly ICommunicationUser _employerServicesUser;
        private readonly ICommunicationUser _systemUser;
        private readonly ICommunicationUser _returnUser;
        private readonly ICommunicationUser _allStaffUser;
        private readonly ICommunicationUser _redStarResume;

        public EmailsCommand(
            ISettingsQuery settingsQuery,
            ISettingsCommand settingsCommand,
            ICommunicationEngine communicationEngine,
            ITemplateEngine templateEngine,
            IAffiliateEmailsQuery affiliateEmailsQuery,
            IUserEmailsQuery userEmailsQuery,
            string memberServicesAddress,
            string employerServicesAddress,
            string systemAddress,
            string returnAddress,
            string servicesDisplayName,
            string allStaffAddress,
            string allStaffDisplayName,
            string redStarResumeAddress,
            string redStarResumeDisplayName)
        {
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;
            _communicationEngine = communicationEngine;
            _templateEngine = templateEngine;
            _affiliateEmailsQuery = affiliateEmailsQuery;
            _userEmailsQuery = userEmailsQuery;

            _memberServicesUser = new EmailUser(memberServicesAddress, servicesDisplayName, null);
            _employerServicesUser = new EmailUser(employerServicesAddress, servicesDisplayName, null);
            _systemUser = new EmailUser(systemAddress, servicesDisplayName, null);
            _allStaffUser = new EmailUser(allStaffAddress, allStaffDisplayName, null);
            _redStarResume = new EmailUser(redStarResumeAddress, redStarResumeDisplayName, null);
            _returnUser = new EmailUser(returnAddress, servicesDisplayName, null);
        }

        bool IEmailsCommand.TrySend(TemplateEmail email)
        {
            return TrySend(new[] { email }, null, false, null) != 0;
        }

        bool IEmailsCommand.TrySend(TemplateEmail email, bool ignoreChecks)
        {
            return TrySend(new[] { email }, null, ignoreChecks, null) != 0;
        }

        bool IEmailsCommand.TrySend(TemplateEmail email, DateTime? notIfLastSentLaterThanThis)
        {
            return TrySend(new[] { email }, null, false, notIfLastSentLaterThanThis) != 0;
        }

        int IEmailsCommand.TrySend(IEnumerable<TemplateEmail> emails, TemplateContentItem templateContentItem, bool ignoreChecks)
        {
            return TrySend(emails, templateContentItem, ignoreChecks, null);
        }

        int IEmailsCommand.TrySend(IEnumerable<TemplateEmail> emails, TemplateContentItem templateContentItem, DateTime? notIfLastSentLaterThanThis)
        {
            return TrySend(emails, templateContentItem, false, notIfLastSentLaterThanThis);
        }

        Communication IEmailsCommand.GeneratePreview(TemplateEmail email)
        {
            var copyItem = GetCopyItem(email, email.Definition, email.Category, null);
            return email.CreateCommunication(copyItem);
        }

        Communication IEmailsCommand.GeneratePreview(TemplateEmail email, TemplateContentItem templateContentItem)
        {
            var copyItemEngine = _templateEngine.GetCopyItemEngine(templateContentItem);
            var context = new TemplateContext { Id = email.Id };
            var copyItem = _templateEngine.GetCopyItem(copyItemEngine, context, email.Properties, null);
            return email.CreateCommunication(copyItem);
        }

        private int TrySend(IEnumerable<TemplateEmail> emails, TemplateContentItem templateContentItem, bool ignorechecks, DateTime? notIfLastSentLaterThanThis)
        {
            const string method = "Send";

            // Keep track of engines.

            var totalSent = 0;
            var definitions = new Dictionary<string, Definition>();
            var categories = new Dictionary<Guid, Category>();
            var copyItemEngines = new Dictionary<Guid, CopyItemEngine>();
            foreach (var email in emails)
            {
                try
                {
                    var affiliateId = _affiliateEmailsQuery.GetAffiliateId(email);
                    SetContacts(email, affiliateId);

                    // Get the definition for this communication.

                    var definition = GetDefinition(email.Definition, definitions);
                    var category = GetCategory(email.Category, definition, categories);

                    // If the user who is being sent the communication is not enabled then they should not get it.

                    if (_userEmailsQuery.ShouldSend(email.To, definition, category, email.RequiresActivation, ignorechecks, notIfLastSentLaterThanThis))
                    {
                        // To include the unsubscribe piece there must be a category and a user.

                        email.Properties.Add("Category", category == null ? string.Empty : category.Name);
                        email.Properties.Add("IncludeUnsubscribe", category != null && email.To.Id != Guid.Empty);

                        // Get the copy for the various parts of the communication.

                        var copyItem = templateContentItem == null
                            ? GetCopyItem(email, definition.Name, category == null ? null : category.Name, affiliateId)
                            : GetCopyItem(email, affiliateId, templateContentItem, copyItemEngines);

                        // Send it.

                        Send(email.CreateCommunication(copyItem), definition);
                        ++totalSent;
                    }
                }
                catch (Exception ex)
                {
                    EventSource.Raise(Event.Error, method, "Cannot send a communication.", ex, null, Event.Arg("Id", email.To.Id), Event.Arg("EmailType", email.GetType().FullName));
                }
            }

            return totalSent;
        }

        private void SetContacts(TemplateEmail email, Guid? affiliateId)
        {
            email.Return = GetReturnUser(affiliateId);

            if (email is UserEmail)
            {
                if (email.From == null)
                    email.From = GetReturnUser(affiliateId);
            }

            if (email is InternalEmail)
            {
                if (email.From == null)
                    email.From = _systemUser;

                if (email is EmployerServicesEmail)
                    email.To = GetEmployerServicesUser(affiliateId);
                else if (email is MemberServicesEmail)
                    email.To = GetMemberServicesUser(affiliateId);
                else if (email is ServicesEmail)
                    email.To = GetServicesUser(email.From, affiliateId);
                else if (email is AllStaffEmail)
                    email.To = _allStaffUser;
                else if (email is SalesEmail)
                    email.To = GetEmployerServicesUser(affiliateId);
                else if (email is NewResourceQuestionEmail)
                    email.To = _redStarResume;
            }

            // Make sure they are set to something.

            if (email.From == null)
                email.From = _systemUser;
            if (email.To == null)
                email.To = _systemUser;
        }

        private ICommunicationUser GetServicesUser(ICommunicationUser from, Guid? affiliateId)
        {
            ICommunicationUser user = null;
            if (affiliateId != null)
                user = _affiliateEmailsQuery.GetServicesUser(from, affiliateId);
            return user ?? (from.UserType == UserType.Employer ? _employerServicesUser : _memberServicesUser);
        }

        private ICommunicationUser GetEmployerServicesUser(Guid? affiliateId)
        {
            ICommunicationUser user = null;
            if (affiliateId != null)
                user = _affiliateEmailsQuery.GetEmployerServicesUser(affiliateId);
            return user ?? _employerServicesUser;
        }

        private ICommunicationUser GetMemberServicesUser(Guid? affiliateId)
        {
            ICommunicationUser user = null;
            if (affiliateId != null)
                user = _affiliateEmailsQuery.GetMemberServicesUser(affiliateId);
            return user ?? _memberServicesUser;
        }

        private ICommunicationUser GetReturnUser(Guid? affiliateId)
        {
            ICommunicationUser user = null;
            if (affiliateId != null)
                user = _affiliateEmailsQuery.GetReturnUser(affiliateId);
            return user ?? _returnUser;
        }

        protected ICommunicationUser GetServicesRecipient(ICommunicationUser recipient, Guid? affiliateId)
        {
            switch (recipient.UserType)
            {
                case UserType.Employer:
                    return GetEmployerServicesUser(affiliateId);

                case UserType.Custodian:
                    return _systemUser;

                default:
                    return GetMemberServicesUser(affiliateId);
            }
        }

        private Definition GetDefinition(string definitionName, IDictionary<string, Definition> definitions)
        {
            if (string.IsNullOrEmpty(definitionName))
                return null;

            Definition definition;
            if (!definitions.TryGetValue(definitionName, out definition))
            {
                definition = _settingsQuery.GetDefinition(definitionName) ?? new Definition { Name = definitionName };
                definitions[definitionName] = definition;
            }

            return definition;
        }

        private Category GetCategory(string categoryName, Definition definition, IDictionary<Guid, Category> categories)
        {
            if (categoryName == null)
            {
                // Use the definition's category.

                if (definition == null)
                    return null;

                Category category;
                if (!categories.TryGetValue(definition.CategoryId, out category))
                {
                    category = _settingsQuery.GetCategory(definition.CategoryId);
                    categories[definition.CategoryId] = category;
                }

                return category;
            }

            // An empty category name means do not use any category.

            if (categoryName.Length == 0)
                return null;

            // Use the category name.

            return _settingsQuery.GetCategory(categoryName);
        }

        private CopyItem GetCopyItem(TemplateEmail email, string definition, string category, Guid? affiliateId)
        {
            // Query the template engine for the copy.

            var context = new TemplateContext
            {
                Id = email.Id,
                Definition = definition,
                Category = category,
                VerticalId = affiliateId,
                UserId = email.To.Id == Guid.Empty ? (Guid?)null : email.To.Id
            };

            var item = _templateEngine.GetCopyItem(context, email.Properties, null);
            if (item == null)
                throw new ApplicationException("Failed to find a copy item for '" + email.Definition + "'.");
            return item;
        }

        private CopyItem GetCopyItem(TemplateEmail email, Guid? affiliateId, TemplateContentItem templateContentItem, IDictionary<Guid, CopyItemEngine> copyItemEngines)
        {
            // Check the collection first.

            CopyItemEngine copyItemEngine;
            if (!copyItemEngines.TryGetValue(affiliateId ?? Guid.Empty, out copyItemEngine))
            {
                // Get a new one.

                templateContentItem.VerticalId = affiliateId;
                copyItemEngine = _templateEngine.GetCopyItemEngine(templateContentItem);
                copyItemEngines[affiliateId ?? Guid.Empty] = copyItemEngine;
            }

            // Query the template engine for the copy.

            var context = new TemplateContext { Id = email.Id, UserId = email.To.Id };
            var item = _templateEngine.GetCopyItem(copyItemEngine, context, email.Properties, null);
            if (item == null)
                throw new ApplicationException("Failed to find a copy item for '" + email.Definition + "'.");
            return item;
        }

        private void Send(Communication communication, Definition definition)
        {
            const string method = "Send";

            // Send it using the engine.

            _communicationEngine.Send(communication);

            // Update the last sent time.

            if (definition != null && definition.Id != default(Guid))
                _settingsCommand.SetLastSentTime(communication.To.Id, definition.Id, DateTime.Now);

            // Track it.

            EventSource.Raise(
                Event.CommunicationTracking,
                method,
                communication.Definition + " communication sent.",
                Event.Arg(typeof(CommunicationTrackingType).Name, CommunicationTrackingType.Sent),
                Event.Arg("Id", communication.Id),
                Event.Arg("Definition", communication.Definition),
                Event.Arg("Affiliate", communication.AffiliateId),
                Event.Arg("UserId", communication.To.Id),
                Event.Arg("EmailAddress", communication.To.EmailAddress));
        }
    }
}