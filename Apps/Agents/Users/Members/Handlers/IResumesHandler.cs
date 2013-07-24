using System;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public interface IResumesHandler
    {
        void OnResumeUploaded(Guid candidateId, Guid resumeId);
        void OnResumeReloaded(Guid candidateId, Guid resumeId);
        void OnResumeEdited(Guid candidateId, Guid resumeId, bool resumeCreated);
    }
}
