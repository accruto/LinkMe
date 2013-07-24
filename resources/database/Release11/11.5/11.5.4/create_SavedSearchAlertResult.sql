CREATE TABLE dbo.SavedSearchAlertResult
	(
	id uniqueidentifier NOT NULL,
	savedSearchAlertId uniqueidentifier NOT NULL,
	searchResultId uniqueidentifier NOT NULL,
	viewed bit NOT NULL,
	createdTime datetime NOT NULL
	)
GO

ALTER TABLE dbo.SavedSearchAlertResult ADD CONSTRAINT
	DF_SavedSearchAlertResult_1_viewed DEFAULT 0 FOR viewed
GO

ALTER TABLE dbo.SavedSearchAlertResult ADD CONSTRAINT
	PK_SavedSearchAlertResult_1 PRIMARY KEY CLUSTERED 
	(
	id
	) 

GO
