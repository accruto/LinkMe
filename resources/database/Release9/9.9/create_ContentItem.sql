IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ContentItem') AND type in (N'U'))
DROP TABLE dbo.ContentItem
GO

CREATE TABLE dbo.ContentItem
(
	id UNIQUEIDENTIFIER NOT NULL,
	parentId UNIQUEIDENTIFIER NULL,
	name NVARCHAR(255) NULL,
	type NVARCHAR(255) NULL,
	deleted BIT NOT NULL,
	enabled BIT NOT NULL,
	verticalId UNIQUEIDENTIFIER NULL
)

ALTER TABLE dbo.ContentItem
ADD CONSTRAINT PK_ContentItem PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.ContentItem
ADD CONSTRAINT FK_ContentItem_ContentItem FOREIGN KEY (parentId)
REFERENCES dbo.ContentItem (id)

