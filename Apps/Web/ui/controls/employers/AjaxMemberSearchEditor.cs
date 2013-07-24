using System;
using AjaxPro;
using LinkMe.Apps.Presentation.Domain.Users.Members.Search;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Web.Applications.Ajax;

namespace LinkMe.Web.UI.Controls.Employers
{
    public class AjaxMemberSearchEditor : AjaxEditorBase
    {
        private static readonly IMemberSearchesCommand _memberSearchesCommand = Container.Current.Resolve<IMemberSearchesCommand>();
        private static readonly IMemberSearchesQuery _memberSearchesQuery = Container.Current.Resolve<IMemberSearchesQuery>();

        [AjaxMethod]
        public AjaxResult GetMemberSearchDisplayHtml(string memberSearchIdStr)
        {
            
            if (string.IsNullOrEmpty(memberSearchIdStr))
                throw new ArgumentException("The member search ID must be specified.", "memberSearchIdStr");

            try
            {
                EnsureEmployerLoggedIn();
                var memberSearch = _memberSearchesQuery.GetMemberSearch(new Guid(memberSearchIdStr));
                return new AjaxResult(AjaxResultCode.SUCCESS, memberSearch.GetDisplayHtml());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [AjaxMethod]
        public AjaxResult SetMemberSearchName(string memberSearchIdStr, string newName)
        {
            if (string.IsNullOrEmpty(memberSearchIdStr))
                throw new ArgumentException("The member search ID must be specified.", "memberSearchIdStr");

            try
            {
                EnsureEmployerLoggedIn();

                var memberSearch = _memberSearchesQuery.GetMemberSearch(new Guid(memberSearchIdStr));
                memberSearch.Name = newName;
                _memberSearchesCommand.UpdateMemberSearch(LoggedInEmployer, memberSearch);
                return new AjaxResult(AjaxResultCode.SUCCESS);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
