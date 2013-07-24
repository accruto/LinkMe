using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Asp.Caches;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Areas.Shared;
using LinkMe.Web.Domain.Roles.JobAds;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public abstract class JobAdsController
        : MembersController
    {
        private static readonly EventSource EventSource = new EventSource<JobAdsController>();

        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery;
        private readonly IJobAdFoldersCommand _jobAdFoldersCommand;
        private readonly IJobAdViewsQuery _jobAdViewsQuery;
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;
        private readonly ICacheManager _cacheManager;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery;
        private readonly IFilesQuery _filesQuery;
        private readonly IExternalJobAdsQuery _externalJobAdsQuery;
        private const int MaxSuggestedJobsCount = 3;

        protected JobAdsController(IJobAdViewsQuery jobAdViewsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand, ICandidatesQuery candidatesQuery, ICandidateResumeFilesQuery candidateResumeFilesQuery, IFilesQuery filesQuery, ICacheManager cacheManager, IMemberStatusQuery memberStatusQuery, IResumesQuery resumesQuery, IExternalJobAdsQuery externalJobAdsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFoldersCommand jobAdFoldersCommand)
        {
            _jobAdViewsQuery = jobAdViewsQuery;
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
            _cacheManager = cacheManager;
            _memberStatusQuery = memberStatusQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _candidateResumeFilesQuery = candidateResumeFilesQuery;
            _filesQuery = filesQuery;
            _externalJobAdsQuery = externalJobAdsQuery;
            _jobAdFlagListsQuery = jobAdFlagListsQuery;
            _jobAdFoldersQuery = jobAdFoldersQuery;
            _jobAdFoldersCommand = jobAdFoldersCommand;
        }

        protected JobAdModel GetJobAdModel(IMember member, MemberJobAdView jobAd, IEmployer jobPoster)
        {
            return new JobAdModel
            {
                JobAd = jobAd,
                DistinctViewedCount = _jobAdViewsQuery.GetDistinctViewedCount(jobAd.Id),
                JobPoster = jobPoster,
                Applicant = GetApplicantModel(member),
                SuggestedJobs = GetSuggestedJobs(member, jobAd),
                CurrentSearch = MemberContext.CurrentSearch,
                ContactedLastWeek = _cacheManager.GetCachedItem<FeaturedStatistics>(HttpContext.Cache, CacheKeys.FeaturedStatistics).MemberAccesses,
                IntegratorName = jobAd.Integration.IntegratorUserId == null ? string.Empty : _externalJobAdsQuery.GetRedirectName(jobAd),
                Folders = GetFoldersModel(),
                Status = new JobAdStatusModel(),
                OrganisationCssFile = HttpContext.GetOrganisationJobAdCssFile(jobPoster.Organisation.Id),
            };
        }

        private FoldersModel GetFoldersModel()
        {
            var member = CurrentMember;

            if (member == null)
                return null;

            // Get the folders and their counts.

            var folderData = new Dictionary<Guid, FolderDataModel>();

            // Flag list.

            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            var count = _jobAdFlagListsQuery.GetFlaggedCount(member);
            folderData[flagList.Id] = new FolderDataModel { Count = count, CanRename = false };

            // Folders.

            var folders = _jobAdFoldersQuery.GetFolders(member);
            var counts = _jobAdFoldersQuery.GetInFolderCounts(member);
            var lastUsedTimes = _jobAdFoldersQuery.GetLastUsedTimes(member);

            foreach (var folder in folders)
            {
                folderData[folder.Id] = new FolderDataModel
                {
                    Count = GetCount(folder.Id, counts),
                    CanRename = _jobAdFoldersCommand.CanRenameFolder(member, folder),
                };
            }

            var comparer = new FolderComparer(lastUsedTimes);

            return new FoldersModel
            {
                FlagList = flagList,
                MobileFolder = folders.Single(f => f.FolderType == FolderType.Mobile),
                PrivateFolders = folders.Where(f => f.FolderType == FolderType.Private).OrderBy(f => f, comparer).ToList(),
                FolderData = folderData,
            };
        }

        private static int GetCount(Guid folderId, IDictionary<Guid, int> counts)
        {
            int count;
            return counts.TryGetValue(folderId, out count) ? count : 0;
        }

        private ApplicantModel GetApplicantModel(IMember member)
        {
            var candidate = member == null ? null : _candidatesQuery.GetCandidate(member.Id);

            return new ApplicantModel
            {
                HasResume = HasResume(candidate),
                LastUsedResumeFile = GetLastUsedResumeFile(member),
                ProfileCompletePercent = GetProfilePercentComplete(member, candidate),
            };
        }

        private int GetProfilePercentComplete(IMember member, ICandidate candidate)
        {
            if (member == null || candidate == null)
                return 0;
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return _memberStatusQuery.GetPercentComplete(member, candidate, resume);
        }

        private static bool HasResume(ICandidate candidate)
        {
            return candidate != null && candidate.ResumeId != null;
        }

        private ResumeFileModel GetLastUsedResumeFile(IHasId<Guid> member)
        {
            if (member == null)
                return null;
            var resumeFile = _candidateResumeFilesQuery.GetLastUsedResumeFile(member.Id);
            if (resumeFile == null)
                return null;
            var fileReference = _filesQuery.GetFileReference(resumeFile.FileReferenceId);
            if (fileReference == null)
                return null;

            return new ResumeFileModel
            {
                FileReferenceId = resumeFile.FileReferenceId,
                FileName = fileReference.FileName,
            };
        }

        private IList<MemberJobAdView> GetSuggestedJobs(IMember member, JobAdView jobAd)
        {
            const string method = "GetSuggestedJobs";

            try
            {
                // If the jobAd is closed and there is a query in the referrer then do a search.

                if (jobAd.Status == JobAdStatus.Closed)
                {
                    var q = HttpContext.Request.UrlReferrer == null
                        ? string.Empty
                        : HttpUtility.ParseQueryString(HttpContext.Request.UrlReferrer.Query)["q"];

                    if (!string.IsNullOrEmpty(q))
                    {
                        var criteria = new JobAdSearchCriteria();
                        criteria.SetKeywords(q);
                        return GetSuggestedJobs(member, _executeJobAdSearchCommand.Search(member, criteria, new Range(0, MaxSuggestedJobsCount)).Results);
                    }
                }

                // If the member is logged in then get suggested jobs.

                if (member != null)
                    return GetSuggestedJobs(member, _executeJobAdSearchCommand.SearchSuggested(member, null, new Range(0, MaxSuggestedJobsCount)).Results);

                // Otherwise get similar jobs to this one.

                return GetSuggestedJobs(null,  _executeJobAdSearchCommand.SearchSimilar(null, jobAd.Id, new Range(0, MaxSuggestedJobsCount)).Results);
            }
            catch (Exception ex)
            {
                // If there is a problem then log but don't stop the user doing what they need to do.

                EventSource.Raise(Event.Error, method, "Cannot get suggested jobs.", ex, new StandardErrorHandler(), Event.Arg("jobAdId", jobAd.Id));
                return new List<MemberJobAdView>();
            }
        }

        private IList<MemberJobAdView> GetSuggestedJobs(IMember member, JobAdSearchResults results)
        {
            return results.TotalMatches > 0
                ? _memberJobAdViewsQuery.GetMemberJobAdViews(member, results.JobAdIds)
                : new List<MemberJobAdView>();
        }
    }
}
