-- Temporary drop constarints

ALTER TABLE [dbo].[GroupEventCoordinator] DROP CONSTRAINT [FK_EventCoordinator_GroupEvent]
GO

--ALTER TABLE [dbo].[GroupEventCoordinator] DROP CONSTRAINT [FK_GroupEventCoordinator_GroupEvent]
--GO

ALTER TABLE [dbo].[GroupEvents] DROP CONSTRAINT [FK_GroupEvents_GroupEvent]
GO

ALTER TABLE [dbo].[GroupEventInvitation] DROP CONSTRAINT [FK_GroupEventInvitation_GroupEvent]
GO

ALTER TABLE [dbo].[GroupEventAttendee] DROP CONSTRAINT [FK_GroupEventAttendee_GroupEvent]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupEvent]') AND name = N'PK_GroupEvent')
ALTER TABLE [dbo].[GroupEvent] DROP CONSTRAINT [PK_GroupEvent]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupEvent]') AND name = N'IX_GroupEvent_startTime_endTime')
DROP INDEX [IX_GroupEvent_startTime_endTime] ON [dbo].[GroupEvent]
GO

-- Recreate constraints

ALTER TABLE [dbo].[GroupEvent] ADD CONSTRAINT [PK_GroupEvent] PRIMARY KEY NONCLUSTERED
(
	[id]
)

CREATE CLUSTERED INDEX IX_GroupEvent_startTime_endTime ON dbo.GroupEvent 
(
	startTime,
	endTime
)
GO

ALTER TABLE [dbo].[GroupEventAttendee] WITH NOCHECK ADD CONSTRAINT [FK_GroupEventAttendee_GroupEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[GroupEvent] ([id])
GO

ALTER TABLE [dbo].[GroupEventInvitation] WITH NOCHECK ADD CONSTRAINT [FK_GroupEventInvitation_GroupEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[GroupEvent] ([id])
GO

ALTER TABLE [dbo].[GroupEvents] WITH NOCHECK ADD CONSTRAINT [FK_GroupEvents_GroupEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[GroupEvent] ([id])
GO

ALTER TABLE [dbo].[GroupEventCoordinator] WITH NOCHECK ADD CONSTRAINT [FK_GroupEventCoordinator_GroupEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[GroupEvent] ([id])
GO

