INSERT INTO dbo.JobHunter([id])
SELECT dbo.GuidFromString([id])
FROM linkme_owner.networker_profile
GO
