IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('UserEventActionedJobAd') AND NAME = 'integratorUserId')
BEGIN
	ALTER TABLE dbo.UserEventActionedJobAd
	ADD integratorUserId UNIQUEIDENTIFIER NULL
END
GO

UPDATE dbo.UserEventActionedJobAd
SET integratorUserId = ja.integratorUserId
FROM dbo.UserEventActionedJobAd uea
INNER JOIN dbo.JobAd ja
ON uea.jobAdId = ja.[id]
GO
