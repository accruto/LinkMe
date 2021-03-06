IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Channel') AND type in (N'U'))
DROP TABLE dbo.Channel
GO

CREATE TABLE [dbo].[Channel](
	[id] [uniqueidentifier] NOT NULL,
	[name] NVARCHAR(50) NOT NULL
)
GO

ALTER TABLE dbo.Channel
ADD CONSTRAINT PK_Channel PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE UNIQUE INDEX IX_Channel_name ON dbo.Channel
(
	name
)
GO



