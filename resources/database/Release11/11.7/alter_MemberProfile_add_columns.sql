ALTER TABLE dbo.MemberProfile
ADD hideUpdatedTermsReminder BIT NULL
GO

ALTER TABLE dbo.MemberProfile
ADD updatedTermsReminderTime DATETIME NULL
GO

UPDATE
	dbo.MemberProfile
SET
	hideUpdatedTermsReminder = 0
GO

ALTER TABLE dbo.MemberProfile
ALTER COLUMN hideUpdatedTermsReminder BIT NOT NULL