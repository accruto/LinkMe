IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobAd]') AND name = N'IX_JobAd_dupecheck')
DROP INDEX [IX_JobAd_dupecheck] ON [dbo].[JobAd] WITH ( ONLINE = OFF )
GO

CREATE INDEX IX_JobAd_dupecheck ON dbo.JobAd
(
	[status],
	expiryTime,
	jobPosterId,
	integratorUserId,
	externalReferenceId
)
INCLUDE ([id], title)
GO
