ALTER TABLE dbo.EmployerProfile
ADD hideUpdatedTermsReminder BIT NULL
GO

ALTER TABLE dbo.EmployerProfile
ADD updatedTermsReminderTime DATETIME NULL
GO

UPDATE
	dbo.EmployerProfile
SET
	hideUpdatedTermsReminder = 0
GO

ALTER TABLE dbo.EmployerProfile
ALTER COLUMN hideUpdatedTermsReminder BIT NOT NULL