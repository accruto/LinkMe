IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOffering]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOffering]
GO

CREATE PROCEDURE [dbo].[CreateOffering]
(
	@offeringId UNIQUEIDENTIFIER,
	@name NVARCHAR(500),
	@providerId UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER,
	@parentCategoryId UNIQUEIDENTIFIER
)
AS
BEGIN

	SET NOCOUNT ON

	-- Offering

	IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
		INSERT
			Offering (id, providerId, name, enabled)
		VALUES
			(@offeringId, @providerId, @name, 1)
	ELSE
		UPDATE
			Offering
		SET
			providerId = @providerId,
			name = @name,
			enabled = 1
		WHERE
			id = @offeringId

	-- Category

	EXEC dbo.CreateOfferCategory @categoryId, @parentCategoryId, @name

	IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
		INSERT
			OfferCategoryOffering (categoryId, offeringId)
		VALUES
			(@categoryId, @offeringId)

END