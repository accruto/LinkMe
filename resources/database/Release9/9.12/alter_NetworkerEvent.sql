-- Temporary drop constraints

ALTER TABLE [dbo].[NetworkerEventDelta] DROP CONSTRAINT [FK_NetworkerEventDelta_NetworkerEvent]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NetworkerEvent]') AND name = N'PK_NetworkerEvent')
ALTER TABLE [dbo].[NetworkerEvent] DROP CONSTRAINT [PK_NetworkerEvent]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NetworkerEvent]') AND name = N'IX_NetworkerEvent_actorId_actionedNetworkerId_time_id_type')
DROP INDEX [IX_NetworkerEvent_actorId_actionedNetworkerId_time_id_type] ON [dbo].[NetworkerEvent]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NetworkerEvent]') AND name = N'IX_NetworkerEvent_actorId_actionedNetworkerId')
DROP INDEX [IX_NetworkerEvent_actorId_actionedNetworkerId] ON [dbo].[NetworkerEvent]
GO

-- Move NetworkerEventDelta into NetworkerEvent.

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('[dbo].[NetworkerEvent]') AND NAME = 'from')
BEGIN
	ALTER TABLE dbo.NetworkerEvent
	ADD [from] NVARCHAR(100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('[dbo].[NetworkerEvent]') AND NAME = 'to')
BEGIN
	ALTER TABLE dbo.NetworkerEvent
	ADD [to] NVARCHAR(100) NULL
END
GO

UPDATE
	NetworkerEvent
SET
	[from] = (SELECT [from] FROM NetworkerEventDelta AS d WHERE d.eventId = id),
	[to] = (SELECT [to] FROM NetworkerEventDelta AS d WHERE d.eventId = id)
GO

-- Recreate constraints

ALTER TABLE [dbo].[NetworkerEvent] ADD CONSTRAINT [PK_NetworkerEvent] PRIMARY KEY NONCLUSTERED
(
	[id]
)

CREATE CLUSTERED INDEX IX_NetworkerEvent_actorId_actionedNetworkerId ON dbo.NetworkerEvent 
(
	[actorId] ASC,
	[time] ASC,
	[actionedNetworkerId] ASC,
	[from] ASC,
	[to] ASC,
	[type] ASC,
	[contentId] ASC	
)
GO

ALTER TABLE [dbo].[NetworkerEventDelta] WITH NOCHECK ADD CONSTRAINT [FK_NetworkerEventDelta_NetworkerEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[NetworkerEvent] ([id])
GO

