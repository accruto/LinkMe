using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds
{
    public interface IJobAdNotesRepository
    {
        void CreateNote(JobAdNote note);
        void UpdateNote(JobAdNote note);
        void DeleteNote(Guid id);

        T GetNote<T>(Guid id) where T : JobAdNote, new();
        IList<T> GetNotes<T>(Guid ownerId, Guid jobAdId) where T : JobAdNote, new();

        bool HasNotes(Guid ownerId, Guid jobAdId);
        IList<Guid> GetHasNotesJobAdIds(Guid ownerId);
    }
}
