IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobAd]') AND name = N'IX_JobAd_integrator')
DROP INDEX [IX_JobAd_integrator] ON [dbo].[JobAd]
GO

CREATE NONCLUSTERED INDEX IX_JobAd_integrator ON dbo.JobAd 
(
	integratorUserId,
	integratorReferenceId ASC
)