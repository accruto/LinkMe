using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Services.External.JobG8.Commands
{
    public interface ISendJobG8Command
    {
        string SendApplication(ICommunicationUser user, JobAdEntry jobAd, string resumeFileName, FileContents resumeContents, InternalApplication application, IEnumerable<ApplicationAnswer> answers);
    }
}