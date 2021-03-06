IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.MemberProfile') AND type in (N'U'))
DROP TABLE dbo.MemberProfile
GO

CREATE TABLE dbo.MemberProfile
(
	memberId UNIQUEIDENTIFIER NOT NULL,
	updateProfileReminderTime DATETIME NULL,
	hideUpdateProfileReminder BIT NOT NULL
)

ALTER TABLE dbo.MemberProfile
ADD CONSTRAINT PK_MemberProfile PRIMARY KEY NONCLUSTERED
(
	memberId
)
GO
