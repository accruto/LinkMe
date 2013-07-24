ALTER TABLE dbo.SavedResumeSearchAlert ADD
	alertType tinyint NOT NULL DEFAULT 0,
	savedResumeSearchId uniqueidentifier NULL
GO

ALTER TABLE dbo.SavedResumeSearchAlert ADD CONSTRAINT
	FK_SavedResumeSearchAlert_SavedResumeSearch FOREIGN KEY
	(
	savedResumeSearchId
	) REFERENCES dbo.SavedResumeSearch
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
