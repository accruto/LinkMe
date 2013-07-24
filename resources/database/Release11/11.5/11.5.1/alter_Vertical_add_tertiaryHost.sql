ALTER TABLE dbo.Vertical
ADD tertiaryHost NVARCHAR(100) NULL
GO

UPDATE
	dbo.Vertical
SET
	tertiaryHost = 'liacareers.linkme.com.au'
WHERE
	name = 'Live In Australia Careers Community'
