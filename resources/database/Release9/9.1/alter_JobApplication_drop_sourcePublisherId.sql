IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.JobApplication') AND NAME = 'sourcePublisherId')
BEGIN
	ALTER TABLE dbo.JobApplication
	DROP COLUMN sourcePublisherId
END
GO
