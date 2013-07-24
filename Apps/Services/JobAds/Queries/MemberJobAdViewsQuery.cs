using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Apps.Services.External.JXT.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Queries;

namespace LinkMe.Apps.Services.JobAds.Queries
{
    public class MemberJobAdViewsQuery
        : IMemberJobAdViewsQuery
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IJobAdProcessingQuery _jobAdProcessingQuery;
        private readonly ICareerOneQuery _careerOneQuery;
        private readonly IJobG8Query _jobG8Query;
        private readonly IJxtQuery _jxtQuery;
        private readonly IJobAdApplicationSubmissionsQuery _jobAdApplicationSubmissionsQuery;
        private readonly IJobAdViewsQuery _jobAdViewsQuery;
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery;

        public MemberJobAdViewsQuery(IJobAdsQuery jobAdsQuery, IEmployersQuery employersQuery, IJobAdProcessingQuery jobAdProcessingQuery, ICareerOneQuery careerOneQuery, IJobG8Query jobG8Query, IJxtQuery jxtQuery, IJobAdApplicationSubmissionsQuery jobAdApplicationSubmissionsQuery, IJobAdViewsQuery jobAdViewsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdFoldersQuery jobAdFoldersQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _employersQuery = employersQuery;
            _jobAdProcessingQuery = jobAdProcessingQuery;
            _careerOneQuery = careerOneQuery;
            _jobG8Query = jobG8Query;
            _jxtQuery = jxtQuery;
            _jobAdApplicationSubmissionsQuery = jobAdApplicationSubmissionsQuery;
            _jobAdViewsQuery = jobAdViewsQuery;
            _jobAdFlagListsQuery = jobAdFlagListsQuery;
            _jobAdFoldersQuery = jobAdFoldersQuery;
        }

        JobAdView IMemberJobAdViewsQuery.GetJobAdView(Guid jobAdId)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            return jobAd == null ? null : GetJobAdView(jobAd);
        }

        JobAdView IMemberJobAdViewsQuery.GetJobAdView(JobAd jobAd)
        {
            return GetJobAdView(jobAd);
        }

        IList<JobAdView> IMemberJobAdViewsQuery.GetJobAdViews(IEnumerable<Guid> jobAdIds)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds);
            return (from j in jobAds
                    select new JobAdView(j, _jobAdProcessingQuery.GetJobAdProcessing(j))).ToList();
        }

        MemberJobAdView IMemberJobAdViewsQuery.GetMemberJobAdView(IMember member, Guid jobAdId)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            return jobAd == null ? null : GetMemberJobAdView(member, jobAd);
        }

        MemberJobAdView IMemberJobAdViewsQuery.GetMemberJobAdView(IMember member, JobAd jobAd)
        {
            return GetMemberJobAdView(member, jobAd);
        }

        IList<MemberJobAdView> IMemberJobAdViewsQuery.GetMemberJobAdViews(IMember member, IEnumerable<Guid> jobAdIds)
        {
            var jobAdIdList = jobAdIds.ToList();
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIdList);

            var employers = _employersQuery.GetEmployers(from j in jobAds select j.PosterId).ToDictionary(e => e.Id, e => e);
            var viewed = member == null
                ? new List<Guid>()
                : _jobAdViewsQuery.GetViewedJobAdIds(member.Id, jobAdIdList);
            var applied = member == null
                ? new List<Guid>()
                : _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(member.Id, jobAdIdList);
            var flagged = member == null
                ? new List<Guid>()
                : _jobAdFlagListsQuery.GetFlaggedJobAdIds(member, jobAdIdList);
            var inMobileFolder = member == null
                ? new List<Guid>()
                : _jobAdFoldersQuery.GetInMobileFolderJobAdIds(member, jobAdIdList);

            return (from j in jobAds
                    select new MemberJobAdView(
                        j,
                        _jobAdProcessingQuery.GetJobAdProcessing(j),
                        GetContactDetails(employers[j.PosterId], j),
                        GetEmployerCompanyName(j),
                        viewed.Contains(j.Id),
                        applied.Contains(j.Id),
                        flagged.Contains(j.Id),
                        inMobileFolder.Contains(j.Id))).ToList();
        }

        private JobAdView GetJobAdView(JobAd jobAd)
        {
            return new JobAdView(jobAd, _jobAdProcessingQuery.GetJobAdProcessing(jobAd));
        }

        private MemberJobAdView GetMemberJobAdView(IMember member, JobAd jobAd)
        {
            return new MemberJobAdView(
                jobAd,
                _jobAdProcessingQuery.GetJobAdProcessing(jobAd),
                GetContactDetails(_employersQuery.GetEmployer(jobAd.PosterId), jobAd),
                GetEmployerCompanyName(jobAd),
                member != null && _jobAdViewsQuery.HasViewedJobAd(member.Id, jobAd.Id),
                member != null && _jobAdApplicationSubmissionsQuery.HasSubmittedApplication(member.Id, jobAd.Id),
                member != null && _jobAdFlagListsQuery.IsFlagged(member, jobAd.Id),
                member != null && _jobAdFoldersQuery.IsInMobileFolder(member, jobAd.Id));
        }

        private ContactDetails GetContactDetails(IEmployer poster, JobAdEntry jobAd)
        {
            // This should only be returned for these cases.

            if (jobAd.Integration.IntegratorUserId == null)
                return GetContactDetails(jobAd.ContactDetails, GetContactCompanyName(poster, jobAd));

            if (jobAd.Integration.IntegratorUserId.Value == _careerOneQuery.GetIntegratorUser().Id
                || jobAd.Integration.IntegratorUserId.Value == _jobG8Query.GetIntegratorUser().Id
                || jobAd.Integration.IntegratorUserId.Value == _jxtQuery.GetIntegratorUser().Id)
                return GetContactDetails(null, GetContactCompanyName(poster, jobAd));

            return GetContactDetails(jobAd.ContactDetails, GetContactCompanyName(poster, jobAd));
        }

        private static ContactDetails GetContactDetails(ContactDetails contactDetails, string companyName)
        {
            // Combine them.

            if (contactDetails == null && string.IsNullOrEmpty(companyName))
                return null;

            if (contactDetails == null)
                return new ContactDetails { CompanyName = companyName };

            return new ContactDetails
            {
                Id = contactDetails.Id,
                FirstName = contactDetails.FirstName,
                LastName = contactDetails.LastName,
                CompanyName = companyName,
                EmailAddress = contactDetails.EmailAddress,
                FaxNumber = contactDetails.FaxNumber,
                PhoneNumber = contactDetails.PhoneNumber,
                SecondaryEmailAddresses = contactDetails.SecondaryEmailAddresses,
            };
        }

        private string GetContactCompanyName(IEmployer poster, JobAdEntry jobAd)
        {
            // Special cases.

            if (jobAd.Integration.IntegratorUserId == null)
                return poster != null ? poster.Organisation.FullName : null;

            if (jobAd.Integration.IntegratorUserId.Value == _careerOneQuery.GetIntegratorUser().Id
                || jobAd.Integration.IntegratorUserId.Value == _jobG8Query.GetIntegratorUser().Id
                || jobAd.Integration.IntegratorUserId.Value == _jxtQuery.GetIntegratorUser().Id)
                return jobAd.ContactDetails != null ? jobAd.ContactDetails.CompanyName : null;

            return poster != null ? poster.Organisation.FullName : null;
        }

        private string GetEmployerCompanyName(JobAd jobAd)
        {
            // This should only be returned for these cases.

            if (jobAd.Integration.IntegratorUserId == null)
                return jobAd.Description.CompanyName;

            return jobAd.Integration.IntegratorUserId.Value == _careerOneQuery.GetIntegratorUser().Id
                ? jobAd.Description.CompanyName
                : null;
        }
    }
}
