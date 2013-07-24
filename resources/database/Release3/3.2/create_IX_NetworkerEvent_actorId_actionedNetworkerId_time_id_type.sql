CREATE NONCLUSTERED INDEX [IX_NetworkerEvent_actorId_actionedNetworkerId_time_id_type] ON [dbo].[NetworkerEvent] 
(
	[actorId] ASC,
	[actionedNetworkerId] ASC,
	[time] ASC,
	[id] ASC,
	[type] ASC
)
