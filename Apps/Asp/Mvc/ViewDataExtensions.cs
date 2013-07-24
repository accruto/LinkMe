using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc
{
    public static class ViewDataExtensions
    {
        private const string CurrentRegisteredUserKey = "CurrentRegisteredUser";
        private const string CurrentAnonymousUserKey = "CurrentAnonymousUser";
        private const string ClientUrlKey = "ClientUrl";
        private const string ActivityContextKey = "ActivityContext";
        private const string ActiveUserTypeKey = "ActiveUserType";

        public static void SetCurrentRegisteredUser(this ViewDataDictionary viewData, RegisteredUser user)
        {
            viewData[CurrentRegisteredUserKey] = user;
        }

        public static RegisteredUser GetCurrentRegisteredUser(this ViewDataDictionary viewData)
        {
            return viewData[CurrentRegisteredUserKey] as RegisteredUser;
        }

        public static void SetCurrentAnonymousUser(this ViewDataDictionary viewData, AnonymousUser user)
        {
            viewData[CurrentAnonymousUserKey] = user;
        }

        public static AnonymousUser GetCurrentAnonymousUser(this ViewDataDictionary viewData)
        {
            return viewData[CurrentAnonymousUserKey] as AnonymousUser;
        }

        public static void SetActiveUserType(this ViewDataDictionary viewData, UserType userType)
        {
            viewData[ActiveUserTypeKey] = userType;
        }

        public static UserType GetActiveUserType(this ViewDataDictionary viewData)
        {
            // If the current user is set then use that.

            var currentUser = viewData.GetCurrentRegisteredUser();
            if (currentUser != null)
                return currentUser.UserType;
            return ((UserType?)viewData[ActiveUserTypeKey]) ?? UserType.Member;
        }

        public static ActivityContext GetActivityContext(this ViewDataDictionary viewData)
        {
            return viewData[ActivityContextKey] as ActivityContext;
        }

        public static void SetActivityContext(this ViewDataDictionary viewData)
        {
            if (viewData.GetActivityContext() == null)
                viewData[ActivityContextKey] = ActivityContext.Current;
        }

        public static ReadOnlyUrl GetClientUrl(this ViewDataDictionary viewData)
        {
            return viewData[ClientUrlKey] as ReadOnlyUrl;
        }

        public static void SetClientUrl(this ViewDataDictionary viewData, HttpContextBase httpContext)
        {
            if (viewData.GetClientUrl() == null)
                viewData[ClientUrlKey] = httpContext.GetClientUrl();
        }
    }
}
