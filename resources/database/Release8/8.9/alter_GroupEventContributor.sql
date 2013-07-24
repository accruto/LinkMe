ALTER TABLE dbo.GroupEventCoordinator ADD CONSTRAINT
	PK_GroupEventCoordinator PRIMARY KEY CLUSTERED 
	(
	eventId,
	contributorId
	)