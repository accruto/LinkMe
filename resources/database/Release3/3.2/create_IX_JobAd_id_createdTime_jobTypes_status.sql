CREATE NONCLUSTERED INDEX [IX_JobAd_id_createdTime_jobTypes_status] ON [dbo].[JobAd] 
(
	[id] ASC,
	[createdTime] ASC,
	[jobTypes] ASC,
	[status] ASC
)
