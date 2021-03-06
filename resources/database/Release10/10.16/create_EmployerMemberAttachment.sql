IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.EmployerMemberAttachment') AND type in (N'U'))
DROP TABLE dbo.EmployerMemberAttachment
GO

CREATE TABLE dbo.EmployerMemberAttachment
(
	id UNIQUEIDENTIFIER NOT NULL,
	uploadedTime DATETIME NOT NULL,
	employerId UNIQUEIDENTIFIER NOT NULL,
	fileReferenceId UNIQUEIDENTIFIER NOT NULL
)

ALTER TABLE dbo.EmployerMemberAttachment
ADD CONSTRAINT PK_EmployerMemberAttachment PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_EmployerMemberAttachment_employerId] ON [dbo].[EmployerMemberAttachment]
(
	employerId
)
GO
