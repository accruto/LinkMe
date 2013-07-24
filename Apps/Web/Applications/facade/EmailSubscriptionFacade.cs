using System;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Web.Helper;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Web.Applications.Facade
{
    public static class EmailSubscriptionFacade
    {
        private static readonly EventSource _eventSource = new EventSource(typeof(EmailSubscriptionFacade));
        private static readonly IEmployersQuery _employersQuery = Container.Current.Resolve<IEmployersQuery>();
        private static readonly INonMemberSettingsQuery _nonMemberSettingsQuery = Container.Current.Resolve<INonMemberSettingsQuery>();
        private static readonly INonMemberSettingsCommand _nonMemberSettingsCommand = Container.Current.Resolve<INonMemberSettingsCommand>();

        public static bool IsUnsubscribedFromSuggestedCandidates(string emailAddress, out int registeredCount)
        {
            var employers = _employersQuery.GetEmployers(emailAddress);
            registeredCount = employers.Count;

            var nms = _nonMemberSettingsQuery.GetNonMemberSettings(emailAddress);
            return nms != null && nms.SuppressSuggestedCandidatesEmails;
        }

        public static bool UnusbscribeFromSuggestedCandidatesWithCheck(string emailAddress, out int registeredCount)
        {
            return ChangeSuggestedCandidatesSubscription(emailAddress, false, out registeredCount);
        }

        public static bool ChangeSuggestedCandidatesSubscription(string emailAddress, bool subscribe,
            out int registeredCount)
        {
            // Is this a registered employer?

            var employers = _employersQuery.GetEmployers(emailAddress);
            registeredCount = employers.Count;

            // Change the NonMemberSettings for that email address (regardless of whether they're registered).

            var nms = _nonMemberSettingsQuery.GetNonMemberSettings(emailAddress);
            if (nms != null)
            {
                if (subscribe != nms.SuppressSuggestedCandidatesEmails)
                    return false;
                nms.SuppressSuggestedCandidatesEmails = !subscribe;
                _nonMemberSettingsCommand.UpdateNonMemberSettings(nms);
            }
            else if (subscribe)
            {
                // "Subscribed" is the default state, so if there are no settings that's fine.
                return false;
            }
            else
            {
                nms = new NonMemberSettings
                {
                    EmailAddress = emailAddress,
                    SuppressSuggestedCandidatesEmails = true
                };
                _nonMemberSettingsCommand.CreateNonMemberSettings(nms);
            }

            return true;
        }
    }
}