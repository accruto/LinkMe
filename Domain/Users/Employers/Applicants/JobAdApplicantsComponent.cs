using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.Applicants
{
    public abstract class JobAdApplicantsComponent
    {
        protected static readonly int[] ListTypes = new[] { (int)ApplicantListType.Standard };
        protected readonly IContenderListsCommand _contenderListsCommand;
        protected readonly IContenderListsQuery _contenderListsQuery;

        protected JobAdApplicantsComponent(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery)
        {
            _contenderListsCommand = contenderListsCommand;
            _contenderListsQuery = contenderListsQuery;
        }

        protected ApplicantList EnsureList(IJobAd jobAd)
        {
            var list = GetList(jobAd);
            if (list == null)
            {
                list = new ApplicantList { Id = jobAd.Id, PosterId = jobAd.PosterId, ApplicantListType = ApplicantListType.Standard };
                _contenderListsCommand.CreateList(list);
            }

            return list;
        }

        protected ApplicantList GetList(IJobAd jobAd)
        {
            return _contenderListsQuery.GetList<ApplicantList>(jobAd.Id);
        }

        protected IEnumerable<ApplicantList> GetLists(Guid ownerId)
        {
            // Only return lists that are in fact applicant lists.

            return _contenderListsQuery.GetLists<ApplicantList>(ownerId, (int)ApplicantListType.Standard);
        }
    }
}