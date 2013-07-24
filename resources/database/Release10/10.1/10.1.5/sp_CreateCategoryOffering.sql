IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateCategoryOffering]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateCategoryOffering]
GO

CREATE PROCEDURE CreateCategoryOffering
(
	@categoryId UNIQUEIDENTIFIER,
	@offeringId UNIQUEIDENTIFIER
)
AS

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

GO

