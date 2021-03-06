IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.MemberViewing') AND type in (N'U'))
DROP TABLE dbo.MemberViewing
GO

CREATE TABLE dbo.MemberViewing
(
	id UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	employerId UNIQUEIDENTIFIER NULL,
	memberId UNIQUEIDENTIFIER NOT NULL,
	jobAdId UNIQUEIDENTIFIER NULL
)

ALTER TABLE dbo.MemberViewing
ADD CONSTRAINT PK_MemberViewing PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_MemberViewing_employerId] ON [dbo].[MemberViewing]
(
	employerId
)
GO
