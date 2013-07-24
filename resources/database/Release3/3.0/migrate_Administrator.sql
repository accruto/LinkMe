INSERT INTO dbo.Administrator ([id])
SELECT dbo.GuidFromString([id])
FROM linkme_owner.administrator_profile
GO
