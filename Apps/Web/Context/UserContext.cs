using System.Web;
using LinkMe.Apps.Agents.Profiles;
using LinkMe.Apps.Asp.Profiles;
using LinkMe.Apps.Asp.Sessions;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.Context
{
    public abstract class UserContext
    {
        private readonly SessionWrapper _session;
        private readonly ProfileWrapper _profile;

        protected UserContext(HttpContextBase context)
        {
            _session = new SessionWrapper(context.Session);
            _profile = new ProfileWrapper(context.Profile);
        }

        protected UserContext(HttpContext context)
        {
            _session = new SessionWrapper(new HttpSessionStateWrapper(context.Session));
            _profile = new ProfileWrapper(context.Profile);
        }

        protected SessionWrapper Session
        {
            get { return _session; }
        }

        protected ProfileWrapper Profile
        {
            get { return _profile; }
        }

        protected bool ShowUpdatedTermsReminder(string profileKey, UserType userType)
        {
            // No longer showing the prompt.

            return false;
            /*
            switch (userType)
            {
                case UserType.Member:
                    var memberProfile = Profile.Get<MemberProfile>(profileKey) ?? new MemberProfile();
                    if (!ShowUpdatedTermsReminder(memberProfile))
                        return false;

                    // Save that it has now been shown in response to this call.

                    memberProfile.UpdatedTermsReminder.FirstShownTime = DateTime.Now;
                    Profile.Set(profileKey, memberProfile);
                    return true;

                case UserType.Employer:
                    var employerProfile = Profile.Get<EmployerProfile>(profileKey) ?? new EmployerProfile();
                    if (!ShowUpdatedTermsReminder(employerProfile))
                        return false;

                    // Save that it has now been shown in response to this call.

                    employerProfile.UpdatedTermsReminder.FirstShownTime = DateTime.Now;
                    Profile.Set(profileKey, employerProfile);
                    return true;

                default:
                    return false;
            }
             */
        }

        private static bool ShowUpdatedTermsReminder(UserProfile profile)
        {
            // Don't show it if it has been hidden ...

            if (profile.UpdatedTermsReminder.Hide)
                return false;

            // ... or it has already been shown.

            return profile.UpdatedTermsReminder.FirstShownTime == null;
        }
    }
}