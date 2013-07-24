IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserEventActionedJobAd]') AND name = N'IX_UserEventActionedJobAd_jobAdId')
DROP INDEX [IX_UserEventActionedJobAd_jobAdId] ON [dbo].[UserEventActionedJobAd] WITH ( ONLINE = OFF )
GO

CREATE INDEX IX_UserEventActionedJobAd_jobAdId ON dbo.UserEventActionedJobAd ( jobAdId )
GO
