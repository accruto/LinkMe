CREATE NONCLUSTERED INDEX [IX_UserEvent_type_actorId_id] ON [dbo].[UserEvent] 
(
	[type] ASC,
	[actorId] ASC,
	[id] ASC
)
