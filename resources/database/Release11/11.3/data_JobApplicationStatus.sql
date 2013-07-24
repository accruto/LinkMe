-- Set all applications to "shortlisted", since that's the default
update candidateListEntry
set jobApplicationStatus = 2
from candidateList
where candidateList.id = candidateListEntry.candidateListId
and candidateList.listType = 6

-- now update to the status from the actual job app
update candidateListEntry
set jobApplicationStatus = 
(select status
from jobApplication
where candidateListEntry.jobApplicationId = jobApplication.id)
where exists (select 0 from jobApplication
where candidateListEntry.jobApplicationId = jobApplication.id)
