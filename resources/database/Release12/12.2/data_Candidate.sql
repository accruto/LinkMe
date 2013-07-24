begin tran

update candidate 
set desiredjobsavedsearchid = null
where desiredjobsavedsearchid is not null
and not exists 
(select 1
from savedjobsearch s (nolock)
where s.id = desiredjobsavedsearchid)

commit