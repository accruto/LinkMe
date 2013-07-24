ALTER TABLE dbo.GroupEvents ADD CONSTRAINT
	PK_GroupEvents PRIMARY KEY CLUSTERED 
	(
	eventId,
	groupId
	) 