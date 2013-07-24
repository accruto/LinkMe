
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResumeSearch]') AND name = N'IX_ResumeSearch_startTime')
DROP INDEX [IX_ResumeSearch_startTime] ON [dbo].[ResumeSearch] WITH ( ONLINE = OFF )

CREATE CLUSTERED INDEX [IX_ResumeSearch_startTime] ON [dbo].[ResumeSearch] 
(
	[startTime]
)
GO

