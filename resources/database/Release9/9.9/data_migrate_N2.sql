-- Create ids for Item

ALTER TABLE dbo.n2Item
ADD guid UNIQUEIDENTIFIER
GO

ALTER TABLE dbo.n2Item
ADD parentGuid UNIQUEIDENTIFIER
GO

UPDATE dbo.n2Item
SET guid = NEWID()
GO

CREATE FUNCTION dbo.GetGuid (@id INT)
RETURNS UNIQUEIDENTIFIER

AS

BEGIN
	DECLARE @guid UNIQUEIDENTIFIER

	SELECT
		@guid = guid
	FROM
		dbo.n2Item
	WHERE
		ID = @id

	RETURN @guid
END

GO

UPDATE dbo.n2Item
SET parentGuid = dbo.GetGuid(ParentID)
GO

-- Create ids for Detail

ALTER TABLE dbo.n2Detail
ADD guid UNIQUEIDENTIFIER
GO

ALTER TABLE dbo.n2Detail
ADD itemGuid UNIQUEIDENTIFIER
GO

UPDATE dbo.n2Detail
SET guid = NEWID()
GO

UPDATE dbo.n2Detail
SET itemGuid = dbo.GetGuid(ItemID)
GO

DROP FUNCTION dbo.GetGuid
GO

-- Migrate

DELETE
	ContentItem

INSERT
	ContentItem (id, parentId, name, type, deleted, enabled, verticalId)
SELECT
	N.guid, N.parentGuid, N.Name, N.Type, 0, N.Visible, V.id
FROM
	dbo.n2Item AS N
LEFT OUTER JOIN
	Vertical AS V ON V.name = N.Vertical
WHERE
	Type <> 'PageContentItem'
GO

DELETE
	ContentDetail

INSERT
	ContentDetail (id, itemId, name, type, stringValue)
SELECT
	guid, itemGuid, Name, 'String', StringValue
FROM
	n2Detail
GO


