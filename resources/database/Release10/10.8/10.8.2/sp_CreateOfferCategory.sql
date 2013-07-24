IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOfferCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOfferCategory]
GO

CREATE PROCEDURE [dbo].[CreateOfferCategory](@categoryId UNIQUEIDENTIFIER, @parentId UNIQUEIDENTIFIER, @categoryName NVARCHAR(500))
AS
BEGIN

	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
		INSERT
			OfferCategory (id, parentId, name, enabled)
		VALUES
			(@categoryId, @parentId, @categoryName, 1)
	ELSE
		UPDATE
			OfferCategory
		SET
			parentId = @parentId,
			name = @categoryName,
			enabled = 1
		WHERE
			id = @categoryId

END