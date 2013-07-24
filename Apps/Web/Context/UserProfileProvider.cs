using System;
using System.Collections;
using System.Configuration;
using System.Web.Profile;
using LinkMe.Apps.Agents.Profiles;
using LinkMe.Apps.Agents.Profiles.Commands;
using LinkMe.Apps.Agents.Profiles.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.Context
{
    public class UserProfileProvider
        : ProfileProvider
    {
        private const string IsAuthenticated = "IsAuthenticated";
        private const string UserName = "UserName";
        private const string EmployerProfile = "EmployerProfile";
        private const string MemberProfile = "MemberProfile";

        private readonly IProfilesCommand _profilesCommand = Container.Current.Resolve<IProfilesCommand>();
        private readonly IProfilesQuery _profilesQuery = Container.Current.Resolve<IProfilesQuery>();

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            // If anonymous or the user id cannot be gotten then simply return the default each time.

            var isAuthenticated = (bool)context[IsAuthenticated];
            if (!isAuthenticated)
                return GetDefaultPropertyValues(collection);

            var userId = GetUserId(context);
            return userId == Guid.Empty
                ? GetDefaultPropertyValues(collection)
                : GetPropertyValues(userId, collection);
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            // Only save for authenticated users.

            var isAuthenticated = (bool)context[IsAuthenticated];
            if (!isAuthenticated)
                return;

            var userId = GetUserId(context);
            if (userId == Guid.Empty)
                return;

            SetPropertyValues(userId, collection);
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        private static Guid GetUserId(IDictionary context)
        {
            try
            {
                return new Guid((string)context[UserName]);
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        private static SettingsPropertyValueCollection GetDefaultPropertyValues(IEnumerable collection)
        {
            var properties = new SettingsPropertyValueCollection();
            foreach (SettingsProperty property in collection)
            {
                var value = new SettingsPropertyValue(property);

                switch (property.Name)
                {
                    case EmployerProfile:
                        value.PropertyValue = new EmployerProfile();
                        break;

                    case MemberProfile:
                        value.PropertyValue = new MemberProfile();
                        break;
                }

                properties.Add(value);
            }

            return properties;
        }

        private SettingsPropertyValueCollection GetPropertyValues(Guid userId, IEnumerable collection)
        {
            var properties = new SettingsPropertyValueCollection();
            foreach (SettingsProperty property in collection)
            {
                var value = new SettingsPropertyValue(property);

                switch (property.Name)
                {
                    case EmployerProfile:
                        value.PropertyValue = _profilesQuery.GetEmployerProfile(userId) ?? new EmployerProfile();
                        break;

                    case MemberProfile:
                        value.PropertyValue = _profilesQuery.GetMemberProfile(userId) ?? new MemberProfile();
                        break;
                }

                properties.Add(value);
            }

            return properties;
        }

        private void SetPropertyValues(Guid userId, IEnumerable collection)
        {
            foreach (SettingsPropertyValue property in collection)
            {
                switch (property.Name)
                {
                    case EmployerProfile:
                        var employerProfile = property.PropertyValue as EmployerProfile;
                        if (employerProfile != null)
                            _profilesCommand.UpdateEmployerProfile(userId, employerProfile);
                        break;

                    case MemberProfile:
                        var memberProfile = property.PropertyValue as MemberProfile;
                        if (memberProfile != null)
                            _profilesCommand.UpdateMemberProfile(userId, memberProfile);
                        break;
                }
            }
        }
    }
}