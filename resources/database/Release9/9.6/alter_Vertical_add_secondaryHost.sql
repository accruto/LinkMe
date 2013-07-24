IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Vertical') AND NAME = 'secondaryHost')
BEGIN
	ALTER TABLE dbo.Vertical
	ADD secondaryHost NVARCHAR(100) NULL
END
GO

UPDATE
	dbo.Vertical
SET
	secondaryHost = 'monashgsb.linkme.com.au'
WHERE
	name = 'Monash University Graduate School of Business'
GO