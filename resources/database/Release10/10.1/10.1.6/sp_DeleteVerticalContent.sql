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
