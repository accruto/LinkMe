-- DeleteVerticalContent

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteVerticalContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteVerticalContent]
GO

CREATE PROCEDURE DeleteVerticalContent
(
	@verticalId UNIQUEIDENTIFIER
)
AS

DELETE
	ContentDetail
FROM
	ContentDetail AS D
INNER JOIN
	ContentItem AS I ON D.itemId = I.id
INNER JOIN
	ContentItem AS P ON I.parentId = P.id
WHERE
	P.verticalId = @verticalId

DELETE
	ContentDetail
FROM
	ContentDetail AS D
INNER JOIN
	ContentItem AS I ON D.itemId = I.id
WHERE
	I.verticalId = @verticalId

DELETE
	ContentItem
WHERE
	parentId IN (SELECT id FROM ContentItem AS I WHERE I.verticalId = @verticalId)

DELETE
	ContentItem
WHERE
	verticalId = @verticalId

GO

-- CreateVerticalContent

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateVerticalContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateVerticalContent]
GO

CREATE PROCEDURE CreateVerticalContent
(
	@verticalId UNIQUEIDENTIFIER,
	@name NVARCHAR(255),
	@type NVARCHAR(255),
	@enabled BIT,
	@value TEXT
)
AS

DECLARE @id UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @itemId UNIQUEIDENTIFIER

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, @name, @type, @enabled, 0, @verticalId)

SET @parentId = @id
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'Content', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', @value)

GO

-- CreateVerticalHtmlContent

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateVerticalHtmlContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateVerticalHtmlContent]
GO

CREATE PROCEDURE CreateVerticalHtmlContent
(
	@verticalId UNIQUEIDENTIFIER,
	@name NVARCHAR(255),
	@enabled BIT,
	@value TEXT
)
AS

DECLARE @id UNIQUEIDENTIFIER
DECLARE @itemId UNIQUEIDENTIFIER

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, @name, 'HtmlContentItem', @enabled, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', @value)

GO

-- CreateVerticalImageContent

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateVerticalImageContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateVerticalImageContent]
GO

CREATE PROCEDURE CreateVerticalImageContent
(
	@verticalId UNIQUEIDENTIFIER,
	@name NVARCHAR(255),
	@enabled BIT,
	@rootFolder TEXT,
	@relativePath TEXT
)
AS

DECLARE @id UNIQUEIDENTIFIER
DECLARE @itemId UNIQUEIDENTIFIER

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, @name, 'ImageContentItem', @enabled, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'RootFolder', 'String', @rootFolder)

SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'RelativePath', 'String', @relativePath)

GO

-- CreateVerticalSectionContent

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateVerticalSectionContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateVerticalSectionContent]
GO

CREATE PROCEDURE CreateVerticalSectionContent
(
	@verticalId UNIQUEIDENTIFIER,
	@name NVARCHAR(255),
	@enabled BIT,
	@sectionTitle TEXT,
	@sectionContent TEXT
)
AS

DECLARE @id UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @itemId UNIQUEIDENTIFIER

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, @name, 'SectionContentItem', @enabled, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'SectionTitle', 'String', @sectionTitle)

SET @parentId = @itemId
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'SectionContent', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', @sectionContent)

GO