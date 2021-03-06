IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CommunityMemberData') AND type in (N'U'))
DROP TABLE dbo.CommunityMemberData
GO

CREATE TABLE dbo.CommunityMemberData
(
	id UNIQUEIDENTIFIER NOT NULL,
	memberId UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(255) NOT NULL,
	value SQL_VARIANT NULL
)

ALTER TABLE dbo.CommunityMemberData
ADD CONSTRAINT PK_CommunityMemberData PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE UNIQUE CLUSTERED INDEX [IX_CommunityMemberData] ON [dbo].[CommunityMemberData]
(
	[memberId],
	[name]
)

ALTER TABLE dbo.CommunityMemberData
ADD CONSTRAINT FK_CommunityMemberData_CommunityMember FOREIGN KEY (memberId)
REFERENCES dbo.CommunityMember (id)

