IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MemberIndexing_Member]') AND parent_object_id = OBJECT_ID(N'[dbo].[MemberIndexing]'))
ALTER TABLE [dbo].[MemberIndexing] DROP CONSTRAINT [FK_MemberIndexing_Member]

/****** Object:  Table [dbo].[MemberIndexing]    Script Date: 10/28/2010 10:12:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberIndexing]') AND type in (N'U'))
DROP TABLE [dbo].[MemberIndexing]

CREATE TABLE dbo.MemberIndexing
(
	memberId UNIQUEIDENTIFIER NOT NULL,
	modifiedTime DATETIME NOT NULL
)

ALTER TABLE dbo.MemberIndexing ADD CONSTRAINT PK_MemberIndexing
	PRIMARY KEY NONCLUSTERED (memberId)

ALTER TABLE dbo.MemberIndexing ADD CONSTRAINT FK_MemberIndexing_Member 
	FOREIGN KEY(memberId) REFERENCES dbo.Member (id)

CREATE CLUSTERED INDEX IX_MemberIndexing_modifiedTime ON dbo.MemberIndexing
(
	modifiedTime
)
