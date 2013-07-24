update linkme_owner.user_profile_ideal_job
set currentjobcategory = right(currentJobIndustry, len(currentJobIndustry) - charindex('.', currentJobIndustry)),
    currentjobindustry = left(currentJobIndustry, charindex('.', currentJobIndustry) - 1)   
where charindex('.', currentjobindustry) > 0 and 
    ( currentjobcategory is null or currentjobcategory = '' )
go


 