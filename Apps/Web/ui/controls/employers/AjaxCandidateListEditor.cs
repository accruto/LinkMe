using System;
using AjaxPro;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Applications.Ajax;

namespace LinkMe.Web.UI.Controls.Employers
{
    public class AjaxCandidateListEditor : AjaxEditorBase
    {
        private static readonly ICandidateFoldersCommand _candidateFoldersCommand = Container.Current.Resolve<ICandidateFoldersCommand>();
        private static readonly ICandidateFoldersQuery _candidateFoldersQuery = Container.Current.Resolve<ICandidateFoldersQuery>();

        [AjaxMethod]
        public AjaxResult SetFolderName(string folderIdStr, string newName)
        {
            if (string.IsNullOrEmpty(folderIdStr))
                throw new ArgumentException("The folder ID must be specified.", "folderIdStr");

            try
            {
                EnsureEmployerLoggedIn();

                var folder = _candidateFoldersQuery.GetFolder(LoggedInEmployer, new Guid(folderIdStr));
                _candidateFoldersCommand.RenameFolder(LoggedInEmployer, folder, newName);
                return new AjaxResult(AjaxResultCode.SUCCESS);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
