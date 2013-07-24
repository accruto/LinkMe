ALTER TABLE dbo.SavedJobSearchAlert ADD
	searchCreated bit NOT NULL CONSTRAINT DF_SavedJobSearchAlert_searchCreated DEFAULT 0
GO