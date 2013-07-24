DECLARE @bankingCategoryId UNIQUEIDENTIFIER
SET @bankingCategoryId = '7C0F0A30-292F-4465-8D28-C6808AA81BF5'

DECLARE @accountingCategoryId UNIQUEIDENTIFIER
SET @accountingCategoryId = 'A03E77C9-F9D9-40F5-AB88-03BA431D71C0'

DECLARE @financialCategoryId UNIQUEIDENTIFIER
SET @financialCategoryId = '79994AD1-E148-4337-BC82-32C477627153'

DECLARE @offeringId UNIQUEIDENTIFIER
SET @offeringId = '5FDF60B6-E588-4D7A-9814-61385D68B697'

DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @parentCategoryId UNIQUEIDENTIFIER
DECLARE @categoryName NVARCHAR(500)
SET @categoryName = 'Financial Services Institute of Australasia (FINSIA)'

-- Banking

SET @categoryId = '0A822A26-2F40-4c5a-A4D6-E76AFDC7BB3C'
SET @parentCategoryId = @bankingCategoryId

IF EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @parentCategoryId AND offeringId = @offeringId)
	DELETE
		OfferCategoryOffering
	WHERE
		categoryId = @parentCategoryId AND offeringId = @offeringId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentCategoryId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentCategoryId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

-- Accounting

SET @categoryId = '8109A31F-828D-4f1c-AB47-FA1411166A2C'
SET @parentCategoryId = @accountingCategoryId

IF EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @parentCategoryId AND offeringId = @offeringId)
	DELETE
		OfferCategoryOffering
	WHERE
		categoryId = @parentCategoryId AND offeringId = @offeringId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentCategoryId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentCategoryId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

-- Financial

SET @categoryId = 'DECF203C-4652-4d0f-B98A-E7893BBA018A'
SET @parentCategoryId = @financialCategoryId

IF EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @parentCategoryId AND offeringId = @offeringId)
	DELETE
		OfferCategoryOffering
	WHERE
		categoryId = @parentCategoryId AND offeringId = @offeringId


IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentCategoryId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentCategoryId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

