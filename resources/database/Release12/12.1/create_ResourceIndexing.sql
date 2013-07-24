/****** Object:  Table [dbo].[ResourceIndexing]    Script Date: 10/28/2010 10:12:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceIndexing]') AND type in (N'U'))
DROP TABLE [dbo].[ResourceIndexing]

CREATE TABLE dbo.ResourceIndexing
(
	resourceId UNIQUEIDENTIFIER NOT NULL,
	modifiedTime DATETIME NOT NULL
)

ALTER TABLE dbo.ResourceIndexing ADD CONSTRAINT PK_ResourceIndexing
	PRIMARY KEY NONCLUSTERED (resourceId)

CREATE CLUSTERED INDEX IX_ResourceIndexing_modifiedTime ON dbo.ResourceIndexing
(
	modifiedTime
)
