-- The numeric values are defined in the JobTypes enum.

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobTypes =
	CASE currentJobType
		WHEN 'Full Time' THEN 1
		WHEN 'Part Time' THEN 2
		WHEN 'Contract' THEN 4
		WHEN 'Temp' THEN 8
		WHEN 'Job Share' THEN 16
		ELSE 0
	END

UPDATE linkme_owner.user_profile_ideal_job
SET idealJobTypes =
	CASE idealJobType
		WHEN 'Full Time' THEN 1
		WHEN 'Part Time' THEN 2
		WHEN 'Contract' THEN 4
		WHEN 'Temp' THEN 8
		WHEN 'Job Share' THEN 16
		ELSE 0
	END

GO