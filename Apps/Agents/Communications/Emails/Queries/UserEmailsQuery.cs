using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Communications.Emails.Queries
{
    public class UserEmailsQuery
        : IUserEmailsQuery
    {
        private static readonly EventSource EventSource = new EventSource<UserEmailsQuery>();
        private readonly ISettingsQuery _settingsQuery;
        private readonly IRecruitersQuery _recruitersQuery;

        private class EffectiveRecipientSettings
        {
            public Timing Timing { get; set; }
            public DateTime? LastSentTime { get; set; }
            public Frequency Frequency { get; set; }
        }

        public UserEmailsQuery(ISettingsQuery settingsQuery, IRecruitersQuery recruitersQuery)
        {
            _settingsQuery = settingsQuery;
            _recruitersQuery = recruitersQuery;
        }

        bool IUserEmailsQuery.ShouldSend(ICommunicationUser to, Definition definition, Category category, bool requiresActivation, bool ignoreChecks, DateTime? notIfLastSentLaterThanThis)
        {
            const string method = "ShouldSend";

            // If the user is disabled then they shouldn't get any email.

            if (!to.IsEnabled)
            {
                EventSource.Raise(Event.Warning, method, "Tried to send an email to a disabled user.", Event.Arg("Id", to.Id), Event.Arg("Definition", definition == null ? string.Empty : definition.Name));
                return false;
            }

            // If the email requires activation and the user is not activated then don't send.

            if (requiresActivation && (!to.IsActivated || !to.IsEmailAddressVerified))
            {
                EventSource.Raise(Event.Warning, method, "Tried to send an email to a deactivated or unverified user.", Event.Arg("Id", to.Id), Event.Arg("Definition", definition == null ? string.Empty : definition.Name));
                return false;
            }

            // Determine whether in fact this communication should be sent.

            if (!ignoreChecks)
            {
                // Check the users settings.

                var settings = GetEffectiveSettings(to, definition, category);
                if (!ShouldSend(settings, notIfLastSentLaterThanThis))
                    return false;
            }

            return true;
        }

        private EffectiveRecipientSettings GetEffectiveSettings(IHasId<Guid> user, Definition definition, Category category)
        {
            if (user == null || definition == null)
                return new EffectiveRecipientSettings { Timing = Timing.Notification, LastSentTime = null, Frequency = Frequency.Immediately };

            // An employer's organisation can stop the email.

            if (user is IEmployer && category != null && category.UserTypes.IsFlagSet(UserType.Employer))
            {
                var settings = GetEffectiveEmployerSettings(user.Id, category);
                if (settings != null)
                    return settings;
            }

            return GetEffectiveSettings(user.Id, definition, category);
        }

        private EffectiveRecipientSettings GetEffectiveEmployerSettings(Guid employerId, Category category)
        {
            var hierarchyPath = _recruitersQuery.GetOrganisationHierarchyPath(employerId);

            foreach (var organisationId in hierarchyPath.Skip(1))
            {
                var settings = _settingsQuery.GetSettings(organisationId);
                if (settings != null)
                {
                    var categorySettings = (from s in settings.CategorySettings where s.CategoryId == category.Id select s).SingleOrDefault();
                    if (categorySettings != null && categorySettings.Frequency == Frequency.Never)
                        return new EffectiveRecipientSettings { Timing = category.Timing, Frequency = Frequency.Never, LastSentTime = null };
                }
            }

            return null;
        }

        private EffectiveRecipientSettings GetEffectiveSettings(Guid recipientId, Definition definition, Category category)
        {
            var settings = _settingsQuery.GetSettings(recipientId, definition.Id, category == null ? (Guid?) null : category.Id);
            var effectiveSettings = new EffectiveRecipientSettings();

            // Category.

            if (settings != null && settings.CategorySettings.Count > 0)
            {
                var categorySettings = settings.CategorySettings[0];
                effectiveSettings.Timing = category != null ? category.Timing : Timing.Notification;
                effectiveSettings.Frequency = categorySettings.Frequency ?? (category != null ? category.DefaultFrequency : Frequency.Immediately);
            }
            else
            {
                effectiveSettings.Timing = category != null ? category.Timing : Timing.Notification;
                effectiveSettings.Frequency = category != null ? category.DefaultFrequency : Frequency.Immediately;
            }

            // Definition.

            if (settings != null && settings.DefinitionSettings.Count > 0)
                effectiveSettings.LastSentTime = settings.DefinitionSettings[0].LastSentTime;

            return effectiveSettings;
        }

        private static bool ShouldSend(EffectiveRecipientSettings settings, DateTime? notIfLastSentLaterThanThis)
        {
            // Check the category settings.

            if (!CategoryCheck(settings))
                return false;

            // Check the definition settings.

            if (!DefinitionCheck(settings, notIfLastSentLaterThanThis))
                return false;

            return true;
        }

        private static bool DefinitionCheck(EffectiveRecipientSettings settings, DateTime? notIfLastSentLaterThanThis)
        {
            // If the user has received a communication of this type recently then don't send.

            if (settings.LastSentTime != null && notIfLastSentLaterThanThis != null)
            {
                if (settings.LastSentTime.Value > notIfLastSentLaterThanThis.Value)
                    return false;
            }

            return true;
        }

        private static bool CategoryCheck(EffectiveRecipientSettings settings)
        {
            switch (settings.Timing)
            {
                case Timing.Notification:

                    // If the user has explicitly set a notification to never then don't send it.

                    if (settings.Frequency == Frequency.Never)
                        return false;
                    break;

                case Timing.Periodic:

                    // Never means don't send.

                    if (settings.Frequency == Frequency.Never)
                        return false;

                    // If it is to be sent immediately then send it.

                    if (settings.Frequency == Frequency.Immediately)
                        return true;

                    // If it hasn't been sent then send now.

                    if (settings.LastSentTime == null)
                        return true;

                    // If it was last sent within the period then don't send now.

                    var lastSentDate = settings.LastSentTime.Value.Date;
                    int days;
                    switch (settings.Frequency)
                    {
                        case Frequency.Monthly:
                            days = 30;
                            break;

                        case Frequency.Weekly:
                            days = 7;
                            break;

                        default:
                            days = 1;
                            break;
                    }

                    if (DateTime.Now.Date.AddDays(-1 * days) < lastSentDate)
                        return false;
                    break;
            }

            return true;
        }
    }
}
