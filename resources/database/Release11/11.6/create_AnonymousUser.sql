IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.AnonymousUser') AND type in (N'U'))
DROP TABLE dbo.AnonymousUser
GO

CREATE TABLE [dbo].[AnonymousUser]
(
	[id] [uniqueidentifier] NOT NULL
)
GO

ALTER TABLE dbo.AnonymousUser
ADD CONSTRAINT PK_AnonymousUser PRIMARY KEY NONCLUSTERED
(
	id
)
GO

