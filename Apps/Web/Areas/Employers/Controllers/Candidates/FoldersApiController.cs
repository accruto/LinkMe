using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    public class FoldersApiController
        : EmployersApiController
    {
        private readonly ICandidateFoldersCommand _candidateFoldersCommand;
        private readonly ICandidateFoldersQuery _candidateFoldersQuery;
        private readonly ICandidateListsCommand _candidateListsCommand;
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery;

        public FoldersApiController(ICandidateFoldersCommand candidateFoldersCommand, ICandidateFoldersQuery candidateFoldersQuery, ICandidateListsCommand candidateListsCommand, ICandidateFlagListsQuery candidateFlagListsQuery)
        {
            _candidateFoldersCommand = candidateFoldersCommand;
            _candidateFoldersQuery = candidateFoldersQuery;
            _candidateListsCommand = candidateListsCommand;
            _candidateFlagListsQuery = candidateFlagListsQuery;
        }

        [HttpPost]
        public ActionResult Folders()
        {
            var employer = CurrentEmployer;

            // Treat the flag list as a special folder.

            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            var count = _candidateFlagListsQuery.GetFlaggedCount(employer);

            // Get the folders and their counts.
            
            var folders = _candidateFoldersQuery.GetFolders(employer);
            var counts = _candidateFoldersQuery.GetInFolderCounts(employer);
            var lastUsedTimes = _candidateFoldersQuery.GetLastUsedTimes(employer);

            var flagListModels = flagList == null
                ? new FolderModel[0]
                : new []
                  {
                      new FolderModel
                      {
                          Id = flagList.Id,
                          Name = null,
                          Type = flagList.FlagListType.ToString(),
                          CanDelete = false,
                          CanRename = false,
                          Count = count
                      }
                  };

            var folderModels = folders
                .OrderBy(f => f, new FolderComparer(lastUsedTimes))
                .Select(f => new FolderModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    Type = f.FolderType.ToString(),
                    CanDelete = _candidateFoldersCommand.CanDeleteFolder(employer, f),
                    CanRename = _candidateFoldersCommand.CanRenameFolder(employer, f),
                    Count = GetCount(f.Id, counts),
                });

            return Json(new JsonFoldersModel { Folders = flagListModels.Concat(folderModels).ToList() });
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult AddCandidates(Guid folderId, Guid[] candidateIds)
        {
            try
            {
                // Look for the folder.

                var employer = CurrentEmployer;
                var folder = _candidateFoldersQuery.GetFolder(employer, folderId);
                if (folder == null)
                    return JsonNotFound("folder");

                // Add candidates.

                var count = _candidateListsCommand.AddCandidatesToFolder(employer, folder, candidateIds);
                return Json(new JsonListCountModel { Id = folderId, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RemoveCandidates(Guid folderId, Guid[] candidateIds)
        {
            try
            {
                // Look for the folder.

                var employer = CurrentEmployer;
                var folder = _candidateFoldersQuery.GetFolder(employer, folderId);
                if (folder == null)
                    return JsonNotFound("folder");

                // Remove candidates.

                var count = _candidateListsCommand.RemoveCandidatesFromFolder(employer, folder, candidateIds);
                return Json(new JsonListCountModel { Id = folderId, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult NewFolder(string name, bool isShared, Guid[] candidateIds)
        {
            try
            {
                // Create the folder.

                var employer = CurrentEmployer;
                var folder = new CandidateFolder
                {
                    Name = name,
                    OrganisationId = isShared ? employer.Organisation.Id : (Guid?) null,
                    RecruiterId = employer.Id
                };

                if (isShared)
                    _candidateFoldersCommand.CreateSharedFolder(employer, folder);
                else
                    _candidateFoldersCommand.CreatePrivateFolder(employer, folder);

                // Add candidates.

                if (candidateIds != null && candidateIds.Length > 0)
                    _candidateListsCommand.AddCandidatesToFolder(employer, folder, candidateIds);

                return Json(new JsonFolderModel
                {
                    Folder = new FolderModel
                    {
                        Id = folder.Id,
                        Name = folder.Name,
                        Type = (isShared ? FolderType.Shared : FolderType.Private).ToString(),
                        CanRename = _candidateFoldersCommand.CanRenameFolder(employer, folder),
                        CanDelete = _candidateFoldersCommand.CanDeleteFolder(employer, folder),
                        Count = candidateIds == null ? 0 : candidateIds.Length,
                    }
                });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult DeleteFolder(Guid folderId)
        {
            try
            {
                // Delete the folder.

                var employer = CurrentEmployer;
                _candidateFoldersCommand.DeleteFolder(employer, folderId);

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RenameFolder(Guid folderId, string name)
        {
            try
            {
                // Rename the folder.

                var employer = CurrentEmployer;
                var folder = _candidateFoldersQuery.GetFolder(employer, folderId);
                if (folder == null)
                    return JsonNotFound("folder");

                _candidateFoldersCommand.RenameFolder(employer, folder, name);

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private static int GetCount(Guid folderId, IDictionary<Guid, int> counts)
        {
            int count;
            return counts.TryGetValue(folderId, out count) ? count : 0;
        }
    }
}
