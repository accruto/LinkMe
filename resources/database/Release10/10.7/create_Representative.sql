IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Representative]') AND type in (N'U'))
DROP TABLE [dbo].[Representative]
GO

CREATE TABLE dbo.Representative
(
	representeeId uniqueidentifier NOT NULL,
	representativeId uniqueidentifier NOT NULL,
)
GO

ALTER TABLE dbo.Representative
ADD CONSTRAINT PK_Representative PRIMARY KEY NONCLUSTERED
(
	representeeId
)

CREATE CLUSTERED INDEX [IX_Representative_representativeId] ON [dbo].[Representative]
(
	representativeId
)

GO

