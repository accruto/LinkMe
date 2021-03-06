IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.EmployerMemberMessage') AND type in (N'U'))
DROP TABLE dbo.EmployerMemberMessage
GO

CREATE TABLE dbo.EmployerMemberMessage
(
	id UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	employerId UNIQUEIDENTIFIER NOT NULL,
	memberId UNIQUEIDENTIFIER NOT NULL,
	contactId UNIQUEIDENTIFIER NULL,
	fromEmailAddress NVARCHAR(320) NULL,
	sendcopyToEmployer BIT NOT NULL, 
	subject NVARCHAR(500) NOT NULL,
	body NTEXT NOT NULL
)

ALTER TABLE dbo.EmployerMemberMessage
ADD CONSTRAINT PK_EmployerMemberMessage PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_EmployerMemberMessage_employerId] ON [dbo].[EmployerMemberMessage]
(
	employerId
)
GO
