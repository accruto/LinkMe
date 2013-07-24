using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public interface IInternalApplicationsCommand
    {
        Guid CreateApplication(IMember member, IJobAd jobAd, string coverLetterText);
        Guid CreateApplicationWithLastUsedResume(IMember member, IJobAd jobAd, string coverLetterText);
        Guid CreateApplicationWithResume(IMember member, IJobAd jobAd, Guid fileReferenceId, bool useForProfile, string coverLetterText);

        void UpdateApplication(IJobAd jobAd, InternalApplication application, IEnumerable<ApplicationAnswer> answers);

        Guid SubmitApplication(IMember member, IJobAd jobAd, string coverLetterText);
        Guid SubmitApplicationWithLastUsedResume(IMember member, IJobAd jobAd, string coverLetterText);
        Guid SubmitApplicationWithResume(IMember member, IJobAd jobAd, Guid fileReferenceId, bool useForProfile, string coverLetterText);
    }
}
