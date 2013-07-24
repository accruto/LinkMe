INSERT INTO dbo.CandidateIndustry(candidateId, industryId)
SELECT dbo.GuidFromString([id]), currentJobIndustryId
FROM linkme_owner.user_profile_ideal_job
WHERE currentJobIndustryId IS NOT NULL
GO
