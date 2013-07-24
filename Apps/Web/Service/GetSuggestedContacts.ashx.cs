using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Web.Applications.Facade;

namespace LinkMe.Web.Service
{
    public class GetSuggestedContacts : AutoSuggestWebServiceHandler, IReadOnlySessionState
    {
        public const string NameParam = "name";
        public const string SessionKey = "KEY_FRIENDS_LIST";

        protected override IList<string> GetSuggestionList(HttpContext context, int maxResults)
        {
            if (GetMemberId(context) == null)
                return null;

            var fragment = context.Request.Params["name"];
            var sessWrapper = new HttpSessionWrapper(GetType(), context.Session);
            var names = (IList<string>)sessWrapper.GetValue(SessionKey);

            if (names.IsNullOrEmpty())
                return null;

            return names.GetNames(fragment);
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }
    }
}
