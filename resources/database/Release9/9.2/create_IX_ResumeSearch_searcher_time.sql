IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResumeSearch]') AND name = N'IX_ResumeSearch_searcher_time')
DROP INDEX [IX_ResumeSearch_searcher_time] ON [dbo].[ResumeSearch] WITH ( ONLINE = OFF )
GO

CREATE INDEX IX_ResumeSearch_searcher_time ON dbo.ResumeSearch
(
	searcherId,
	startTime
)
GO
