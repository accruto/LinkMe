IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.MemberContact') AND type in (N'U'))
DROP TABLE dbo.MemberContact
GO

CREATE TABLE dbo.MemberContact
(
	id UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	reason INT NOT NULL,
	employerId UNIQUEIDENTIFIER NOT NULL,
	memberId UNIQUEIDENTIFIER NOT NULL,
	exercisedCreditId UNIQUEIDENTIFIER NULL
)

ALTER TABLE dbo.MemberContact
ADD CONSTRAINT PK_MemberContact PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_MemberContact_employerId] ON [dbo].[MemberContact]
(
	employerId
)
GO
