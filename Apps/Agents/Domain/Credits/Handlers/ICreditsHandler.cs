using System;

namespace LinkMe.Apps.Agents.Domain.Credits.Handlers
{
    public interface ICreditsHandler
    {
        void OnCreditExercised(Guid creditId, Guid ownerId, bool allocationAdjusted);
    }
}