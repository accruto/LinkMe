using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Services.External.JobG8.Commands
{
    public class StubSendJobG8Command
        : ISendJobG8Command
    {
        string ISendJobG8Command.SendApplication(ICommunicationUser user, JobAdEntry jobAd, string resumeFileName, FileContents resumeContents, InternalApplication application, IEnumerable<ApplicationAnswer> answers)
        {
            return "SUCCESS";
        }
    }
}
