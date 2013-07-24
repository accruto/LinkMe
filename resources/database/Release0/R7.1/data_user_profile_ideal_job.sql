UPDATE linkme_owner.user_profile_ideal_job
SET currentJobType = 'Temp'
WHERE currentJobType = 'Casual' OR currentJobType = 'Temporary'

UPDATE linkme_owner.user_profile_ideal_job
SET idealJobType = 'Temp'
WHERE idealJobType = 'Casual' OR idealJobType = 'Temporary'

GO