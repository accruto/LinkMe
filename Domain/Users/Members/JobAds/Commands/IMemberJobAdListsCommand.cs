using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public interface IMemberJobAdListsCommand
    {
        int AddJobAdToFlagList(IMember member, JobAdFlagList flagList, Guid jobAdId);
        int AddJobAdsToFlagList(IMember member, JobAdFlagList flagList, IEnumerable<Guid> jobAdIds);
        int RemoveJobAdFromFlagList(IMember member, JobAdFlagList flagList, Guid jobAdId);
        int RemoveJobAdsFromFlagList(IMember member, JobAdFlagList flagList, IEnumerable<Guid> jobAdIds);
        int RemoveAllJobAdsFromFlagList(IMember member, JobAdFlagList flagList);
       
        int AddJobAdToBlockList(IMember member, JobAdBlockList blockList, Guid jobAdId);
        int AddJobAdsToBlockList(IMember member, JobAdBlockList blockList, IEnumerable<Guid> jobAdIds);
        int RemoveJobAdFromBlockList(IMember member, JobAdBlockList blockList, Guid jobAdId);
        int RemoveJobAdsFromBlockList(IMember member, JobAdBlockList blockList, IEnumerable<Guid> jobAdIds);
        int RemoveAllJobAdsFromBlockList(IMember member, JobAdBlockList blockList);

        int AddJobAdToFolder(IMember member, JobAdFolder folder, Guid jobAdId);
        int AddJobAdsToFolder(IMember member, JobAdFolder folder, IEnumerable<Guid> jobAdIds);
        int RemoveJobAdFromFolder(IMember member, JobAdFolder folder, Guid jobAdId);
        int RemoveJobAdsFromFolder(IMember member, JobAdFolder folder, IEnumerable<Guid> jobAdIds);
        int RemoveAllJobAdsFromFolder(IMember member, JobAdFolder folder);
    }
}