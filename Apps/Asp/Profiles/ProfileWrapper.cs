using System.Web.Profile;
using LinkMe.Apps.Agents.Profiles;

namespace LinkMe.Apps.Asp.Profiles
{
    public class Profile
        : ProfileBase
    {
        private const string MemberProfileKey = "MemberProfile";
        private const string EmployerProfileKey = "EmployerProfile";

        public MemberProfile MemberProfile
        {
            get { return Get<MemberProfile>(MemberProfileKey); }
            set { Set(MemberProfileKey, value); }
        }

        public EmployerProfile EmployerProfile
        {
            get { return Get<EmployerProfile>(EmployerProfileKey); }
            set { Set(EmployerProfileKey, value); }
        }

        protected TValue Get<TValue>(string key)
        {
            return (TValue)GetPropertyValue(key);
        }

        protected void Set<TValue>(string key, TValue value)
        {
            SetPropertyValue(key, value);
        }
    }

    public class ProfileWrapper
    {
        private readonly Profile _profile;

        public ProfileWrapper(ProfileBase profile)
        {
            _profile = profile as Profile;
        }

        public MemberProfile MemberProfile
        {
            get
            {
                return _profile.MemberProfile ?? new MemberProfile();
            }
            set
            {
                _profile.MemberProfile = value;
                _profile.Save();
            }
        }

        public EmployerProfile EmployerProfile
        {
            get
            {
                return _profile.EmployerProfile ?? new EmployerProfile();
            }
            set
            {
                _profile.EmployerProfile = value;
                _profile.Save();
            }
        }
    }
}
