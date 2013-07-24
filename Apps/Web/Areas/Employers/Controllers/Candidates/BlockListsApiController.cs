using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    public class BlockListsApiController
        : EmployersApiController
    {
        private readonly ICandidateListsCommand _candidateListsCommand;
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery;

        public BlockListsApiController(ICandidateListsCommand candidateListsCommand, ICandidateBlockListsQuery candidateBlockListsQuery)
        {
            _candidateListsCommand = candidateListsCommand;
            _candidateBlockListsQuery = candidateBlockListsQuery;
        }

        [HttpPost]
        public ActionResult BlockLists()
        {
            var employer = CurrentEmployer;

            // Get the blocklists and their counts.

            var blockLists = _candidateBlockListsQuery.GetBlockLists(employer);
            var counts = _candidateBlockListsQuery.GetBlockedCounts(employer);

            var models = blockLists
                .OrderBy(b => b, new BlockListComparer())
                .Select(b => new BlockListModel
                {
                    Id = b.Id,
                    Type = b.BlockListType.ToString(),
                    Count = GetCount(b.Id, counts),
                });

            return Json(new JsonBlockListsModel { BlockLists = models.ToList() });
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult AddCandidates(Guid blockListId, Guid[] candidateIds)
        {
            try
            {
                // Look for the blocklist.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetBlockList(employer, blockListId);
                if (blockList == null)
                    return JsonNotFound("blocklist");

                // Add candidates.

                var count = _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, candidateIds);
                return Json(new JsonListCountModel { Id = blockListId, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RemoveCandidates(Guid blockListId, Guid[] candidateIds)
        {
            try
            {
                // Look for the blocklist.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetBlockList(employer, blockListId);
                if (blockList == null)
                    return JsonNotFound("blocklist");

                // Remove candidates.

                var count = _candidateListsCommand.RemoveCandidatesFromBlockList(employer, blockList, candidateIds);
                return Json(new JsonListCountModel { Id = blockListId, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult BlockTemporaryCandidates(Guid[] candidateIds)
        {
            try
            {
                // Look for the blocklist.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);

                // Add candidates.

                var count = _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, candidateIds);
                return Json(new JsonListCountModel { Id = blockList.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult UnblockTemporaryCandidates(Guid[] candidateIds)
        {
            try
            {
                // Look for the blocklist.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);

                // Remove candidates.

                var count = _candidateListsCommand.RemoveCandidatesFromBlockList(employer, blockList, candidateIds);
                return Json(new JsonListCountModel { Id = blockList.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult UnblockAllTemporaryCandidates()
        {
            try
            {
                // Look for the blocklist.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);

                // Remove candidates.

                var count = _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer, blockList);
                return Json(new JsonListCountModel { Id = blockList.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult BlockPermanentCandidates(Guid[] candidateIds)
        {
            try
            {
                // Look for the blocklist.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

                // Add candidates.

                var count = _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, candidateIds);
                return Json(new JsonListCountModel { Id = blockList.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult UnblockPermanentCandidates(Guid[] candidateIds)
        {
            try
            {
                // Look for the blockList.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

                // Remove candidates.

                var count = _candidateListsCommand.RemoveCandidatesFromBlockList(employer, blockList, candidateIds);
                return Json(new JsonListCountModel { Id = blockList.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult UnblockAllPermanentCandidates()
        {
            try
            {
                // Look for the blockList.

                var employer = CurrentEmployer;
                var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

                // Remove candidates.

                var count = _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer, blockList);
                return Json(new JsonListCountModel { Id = blockList.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private static int GetCount(Guid blockListId, IDictionary<Guid, int> counts)
        {
            int count;
            return counts.TryGetValue(blockListId, out count) ? count : 0;
        }
    }
}
