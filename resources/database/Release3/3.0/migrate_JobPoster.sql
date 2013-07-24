INSERT INTO dbo.JobPoster ([id], flags)
SELECT dbo.GuidFromString([id]), flags
FROM linkme_owner.employer_profile
GO
