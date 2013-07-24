using System;
using System.Collections.Generic;
using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Recruiters;

namespace LinkMe.Web.Service
{
    public class GetOrganisationNames
        : AutoSuggestWebServiceHandler
    {
        private static readonly IExecuteOrganisationSearchCommand _executeOrganisationSearchCommand = Container.Current.Resolve<IExecuteOrganisationSearchCommand>();

        public const string FullNameParameter = "fullName";

        protected override int DefaultMaxResults
        {
            get { return 20; }
        }

        protected override IList<string> GetSuggestionList(HttpContext context, int maxResults)
        {
            if (GetAdministratorId(context) == null)
                return null;

            var fullName = context.Request[FullNameParameter];
            return string.IsNullOrEmpty(fullName)
                ? null
                : _executeOrganisationSearchCommand.GetOrganisationFullNames(fullName, maxResults);
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }
    }
}