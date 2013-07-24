
IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('JobAd') AND NAME = 'contactCompanyName')
BEGIN
	ALTER TABLE dbo.JobAd
		ADD contactCompanyName dbo.CompanyName
END

GO
