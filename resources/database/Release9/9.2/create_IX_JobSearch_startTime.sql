IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobSearch]') AND name = N'IX_JobSearch_startTime')
DROP INDEX [IX_JobSearch_startTime] ON [dbo].[JobSearch] WITH ( ONLINE = OFF )
GO

CREATE INDEX IX_JobSearch_startTime
ON dbo.JobSearch (startTime)
INCLUDE (criteriaSetId)
GO
