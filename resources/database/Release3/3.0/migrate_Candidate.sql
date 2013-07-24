-- CandidateAvailability is set from the old active/passive sub-roles.
-- Active -> ActivelyLooking, Passive -> OpenToOffers

INSERT INTO dbo.Candidate([id], status, currentJobTypes, currentSalaryLower, currentSalaryUpper,
	currentSalaryRateType, desiredJobTitle, desiredJobTypes, desiredSalaryLower, desiredSalaryUpper,
	desiredSalaryRateType, resumeReminderLastSentTime, desiredJobSavedSearchId)
SELECT dbo.GuidFromString(np.[id]),
	CASE up.subRole WHEN 'Active' THEN 3 WHEN 'Passive' THEN 2 END AS status,
	ISNULL(currentJobTypes, 0), currentSalaryMinRate, currentSalaryMaxRate,
	ISNULL(currentSalaryRateType, 0),
	CASE WHEN LEN(idealJob) > 100 THEN NULL ELSE NULLIF(LTRIM(RTRIM(idealJob)), '') END,
	ISNULL(idealJobTypes, 0), idealSalaryMinRate, idealSalaryMaxRate, ISNULL(idealSalaryRateType, 0),
	NULLIF(resumeUpdatedRemindedDate, joinDate), savedJobSearchId
FROM linkme_owner.networker_profile np
INNER JOIN linkme_owner.user_profile up
ON np.[id] = up.[id]
LEFT OUTER JOIN linkme_owner.user_profile_ideal_job ij
ON np.[id] = ij.[id]

GO

UPDATE dbo.Candidate
SET currentSalaryLower = NULL
WHERE currentSalaryLower <= 0

UPDATE dbo.Candidate
SET currentSalaryUpper = NULL
WHERE currentSalaryUpper <= 0

UPDATE dbo.Candidate
SET desiredSalaryLower = NULL
WHERE desiredSalaryLower <= 0

UPDATE dbo.Candidate
SET desiredSalaryUpper = NULL
WHERE desiredSalaryUpper <= 0

GO
