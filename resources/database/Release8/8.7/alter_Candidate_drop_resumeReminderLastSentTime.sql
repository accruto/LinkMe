
IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Candidate') AND NAME = 'resumeReminderLastSentTime')
	ALTER TABLE dbo.Candidate
	DROP COLUMN resumeReminderLastSentTime

GO
