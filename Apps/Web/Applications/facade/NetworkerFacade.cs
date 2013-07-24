using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Apps.Agents.Users.Sessions.Queries;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Apps.Asp.Exceptions;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Domain.Location.Queries;
using Constants=LinkMe.Common.Constants;

namespace LinkMe.Web.Applications.Facade
{
	public static class NetworkerFacade
	{
	    private static readonly Regex DisallowedNameCharsRegex = new Regex(RegularExpressions.DisallowedNameCharPattern, RegexOptions.Compiled);

        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly IMemberContactsQuery _memberContactsQuery = Container.Current.Resolve<IMemberContactsQuery>();
        private static readonly IUserSessionsQuery _userSessionsQuery = Container.Current.Resolve<IUserSessionsQuery>();
        private static readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        public static ReadOnlyUrl GetProfilePhotoUrlOrDefault(Member member)
        {
            return member.GetPhotoUrlOrDefault();
        }

        public static IList<Member> FindFriendsFromEmails(Guid networkerId, string[] emailsToGet)
        {
            var ids = from e in emailsToGet
                      let id = _memberContactsQuery.GetFirstDegreeContact(networkerId, e)
                      where id != null
                      select id.Value;
            return _membersQuery.GetMembers(ids);
        }

        public static string CleanNetworkerNameQuery(string query)
        {
            if (query == null)
                return "";

            // Strip disallowed characters, trim and collapse spaces.

            query = DisallowedNameCharsRegex.Replace(query, "");

            return query.Trim().CollapseSpaces();
        }

	    public static string GetCandidateStatusText(CandidateStatus status)
	    {
            switch (status)
            {
                case CandidateStatus.AvailableNow:
                    return "Immediately available";

                case CandidateStatus.ActivelyLooking:
                    return "Actively looking for work";

                case CandidateStatus.OpenToOffers:
                    return "Not looking but happy to talk";

                case CandidateStatus.NotLooking:
                    return "Not looking for work";

                case CandidateStatus.Unspecified:
                    return "";

                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException("status", (int)status, typeof(CandidateStatus));
            }
	    }

	    public static string GetLastUpdatedText(DateTime? date)
	    {
            return (date == null ? "" : "Resume last updated on " + date.Value.ToString(Constants.DATE_FORMAT));
	    }

        public static string GetLastLoggedInText(Member member, DateTime? ifLaterThan)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            DateTime? lastLogin;
            try
            {
                lastLogin = _userSessionsQuery.GetLastLoginTime(member.Id);
            }
            catch (Exception ex)
            {
                // This is slow and could time out - don't fail completely.
                ExceptionManager.HandleException(ex, new StandardErrorHandler());
                return "";
            }

            if (lastLogin == null || (ifLaterThan != null && lastLogin.Value.Date <= ifLaterThan.Value.Date))
                return "";

            return "Last visited LinkMe on " + lastLogin.Value.ToString(Constants.DATE_FORMAT);
        }

        public static string GetEthnicStatusText(Member member, bool nonEmpty)
        {
            var activeFlags = EthnicStatusDisplay.Values
                .Where(item => (member.EthnicStatus & item) == item)
                .Select(item => item.GetDisplayText())
                .ToArray();

            if (activeFlags == null || activeFlags.Length == 0)
                return nonEmpty? "None" : string.Empty;

            return string.Join(", ", activeFlags);
        }

        public static string GetJoinDateText(DateTime date)
	    {
            return "Joined on " + date.ToString(Constants.DATE_FORMAT);
	    }

        public static void UpdateMemberAddress(Member member, Country newCountry, string newLocation)
        {
            _locationQuery.ResolvePostalSuburb(member.Address.Location, newCountry, newLocation);
        }
	}
}
