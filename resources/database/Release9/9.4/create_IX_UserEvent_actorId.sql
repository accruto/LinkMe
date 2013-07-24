IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserEvent]') AND name = N'IX_UserEvent_actorId')
DROP INDEX [IX_UserEvent_actorId] ON [dbo].[UserEvent] WITH ( ONLINE = OFF )
GO

CREATE CLUSTERED INDEX IX_UserEvent_actorId ON dbo.UserEvent 
(
	actorId
)