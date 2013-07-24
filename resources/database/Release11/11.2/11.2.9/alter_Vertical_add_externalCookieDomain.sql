ALTER TABLE dbo.Vertical
ADD externalCookieDomain NVARCHAR(100) NULL
GO

UPDATE
	dbo.Vertical
SET
	externalCookieDomain = '.arts.unimelb.edu.au'
WHERE
	name = 'University of Melbourne Arts'


