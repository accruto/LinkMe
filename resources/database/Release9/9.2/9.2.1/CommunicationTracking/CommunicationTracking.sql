IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TrackingCommunication]') AND name = N'IX_TrackingCommunication_sent')
DROP INDEX [IX_TrackingCommunication_sent] ON [dbo].[TrackingCommunication]
GO

CREATE CLUSTERED INDEX [IX_TrackingCommunication_sent] ON [dbo].[TrackingCommunication]
(
	[sent]
)
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TrackingCommunicationLink]') AND name = N'IX_TrackingCommunicationLink_communicationId')
DROP INDEX [IX_TrackingCommunication_communicationId] ON [dbo].[TrackingCommunicationLink]
GO

CREATE CLUSTERED INDEX [IX_TrackingCommunication_communicationId] ON [dbo].[TrackingCommunicationLink]
(
	[communicationId]
)
GO

