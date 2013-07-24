using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates.Commands
{
    public interface ICandidateListsCommand
    {
        int AddCandidateToBlockList(IEmployer employer, CandidateBlockList blockList, Guid candidateId);
        int AddCandidatesToBlockList(IEmployer employer, CandidateBlockList blockList, IEnumerable<Guid> candidateIds);
        int RemoveCandidateFromBlockList(IEmployer employer, CandidateBlockList blockList, Guid candidateId);
        int RemoveCandidatesFromBlockList(IEmployer employer, CandidateBlockList blockList, IEnumerable<Guid> candidateIds);
        int RemoveAllCandidatesFromBlockList(IEmployer employer, CandidateBlockList blockList);
        int RemoveAllCandidatesFromBlockList(IEmployer employer, BlockListType blockListType);

        int AddCandidateToFlagList(IEmployer employer, CandidateFlagList flagList, Guid candidateId);
        int AddCandidatesToFlagList(IEmployer employer, CandidateFlagList flagList, IEnumerable<Guid> candidateIds);
        int RemoveCandidateFromFlagList(IEmployer employer, CandidateFlagList flagList, Guid candidateId);
        int RemoveCandidatesFromFlagList(IEmployer employer, CandidateFlagList flagList, IEnumerable<Guid> candidateIds);
        int RemoveAllCandidatesFromFlagList(IEmployer employer, CandidateFlagList flagList);

        int AddCandidateToFolder(IEmployer employer, CandidateFolder folder, Guid candidateId);
        int AddCandidatesToFolder(IEmployer employer, CandidateFolder folder, IEnumerable<Guid> candidateIds);
        int RemoveCandidateFromFolder(IEmployer employer, CandidateFolder folder, Guid candidateId);
        int RemoveCandidatesFromFolder(IEmployer employer, CandidateFolder folder, IEnumerable<Guid> candidateIds);
        int RemoveAllCandidatesFromFolder(IEmployer employer, CandidateFolder folder);
    }
}
