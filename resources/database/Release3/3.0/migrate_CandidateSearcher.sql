INSERT INTO dbo.CandidateSearcher ([id])
SELECT dbo.GuidFromString([id])
FROM linkme_owner.employer_profile
GO
