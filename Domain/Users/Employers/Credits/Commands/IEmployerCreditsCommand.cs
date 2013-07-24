using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Domain.Users.Employers.Credits.Commands
{
    public interface IEmployerCreditsCommand
    {
        Guid? ExerciseContactCredit(IEmployer employer, ProfessionalView view);
        void CheckCanExerciseContactCredit(IEmployer employer, ProfessionalView view);

        IDictionary<Guid, Guid?> ExerciseContactCredits(IEmployer employer, ProfessionalViews views);
        void CheckCanExerciseContactCredits(IEmployer employer, ProfessionalViews views);

        Guid? ExerciseApplicantCredit(InternalApplication application, IJobAd jobAd);
        Guid? ExerciseJobAdCredit(JobAdEntry jobAd);
    }
}