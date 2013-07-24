using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public class ContendersQuery
        : IContendersQuery
    {
        private readonly IContenderListsRepository _repository;
        private readonly IExercisedCreditsQuery _exercisedCreditsQuery;

        public ContendersQuery(IContenderListsRepository repository, IExercisedCreditsQuery exercisedCreditsQuery)
        {
            _repository = repository;
            _exercisedCreditsQuery = exercisedCreditsQuery;
        }

        ProfessionalContactDegree IContendersQuery.GetContactDegree(OrganisationHierarchyPath organisationHierarchyPath, Guid contenderId)
        {
            return GetContactDegree(organisationHierarchyPath, contenderId);
        }

        IList<Guid> IContendersQuery.GetApplicants(OrganisationHierarchyPath organisationHierarchyPath)
        {
            return GetApplicants(organisationHierarchyPath).ToList();
        }

        IList<Guid> IContendersQuery.GetApplicants(OrganisationHierarchyPath organisationHierarchyPath, IEnumerable<Guid> contenderIds)
        {
            return GetApplicants(organisationHierarchyPath, contenderIds);
        }

        bool IContendersQuery.IsApplicant(OrganisationHierarchyPath organisationHierarchyPath, Guid contenderId)
        {
            return IsApplicant(organisationHierarchyPath, contenderId);
        }

        private ProfessionalContactDegree GetContactDegree(OrganisationHierarchyPath organisationHierarchyPath, Guid contenderId)
        {
            return IsApplicant(organisationHierarchyPath, contenderId)
                       ? ProfessionalContactDegree.Applicant
                       : _exercisedCreditsQuery.HasExercisedCredit<ContactCredit>(organisationHierarchyPath, contenderId)
                             ? ProfessionalContactDegree.Contacted
                             : ProfessionalContactDegree.NotContacted;
        }

        private bool IsApplicant(OrganisationHierarchyPath organisationHierarchyPath, Guid contenderId)
        {
            // Before the organisational hierarchy was in place we didn't explicitly record using credits for applicants.
            // Use the credits to find those applicants to jobs in the organisational hierarchy
            // but also use the repository to look for those who applied to jobs from the recruiter themselves before
            // this was put in place.

            if (_exercisedCreditsQuery.HasExercisedCredit<ApplicantCredit>(organisationHierarchyPath, contenderId))
                return true;

            if (organisationHierarchyPath.RecruiterId != null)
                return _repository.IsOwnerApplicant(organisationHierarchyPath.RecruiterId.Value, contenderId);

            return false;
        }

        private IEnumerable<Guid> GetApplicants(OrganisationHierarchyPath organisationHierarchyPath)
        {
            var applicants = from c in _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(organisationHierarchyPath) select c.ExercisedOnId.Value;
            if (organisationHierarchyPath.RecruiterId != null)
                applicants = applicants.Concat(_repository.GetOwnerApplicants(organisationHierarchyPath.RecruiterId.Value));
            return applicants.Distinct().ToList();
        }

        private IList<Guid> GetApplicants(OrganisationHierarchyPath organisationHierarchyPath, IEnumerable<Guid> contenderIds)
        {
            var applicants = from c in _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(organisationHierarchyPath, contenderIds) select c.ExercisedOnId.Value;
            if (organisationHierarchyPath.RecruiterId != null)
                applicants = applicants.Concat(_repository.GetOwnerApplicants(organisationHierarchyPath.RecruiterId.Value, contenderIds));
            return applicants.Distinct().ToList();
        }
    }
}