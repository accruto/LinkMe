ALTER TABLE dbo.IndustryAlias
ADD urlName NVARCHAR(100) NULL
GO

UPDATE
	dbo.IndustryAlias
SET
	urlName = 'healthcare-medical'
WHERE
	displayName = 'Healthcare & Medical'