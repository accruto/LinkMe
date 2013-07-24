ALTER TABLE linkme_owner.JobAd
ADD integratorUserId UNIQUEIDENTIFIER
GO

ALTER TABLE linkme_owner.JobAd ADD CONSTRAINT FK_JobAd_IntegratorUser
	FOREIGN KEY (integratorUserId) REFERENCES linkme_owner.IntegratorUser ([id])
GO
