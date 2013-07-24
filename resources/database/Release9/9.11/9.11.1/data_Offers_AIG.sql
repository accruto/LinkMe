DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '09DBD244-3912-4f39-A102-F100E44BFE90'

DECLARE @name NVARCHAR(500)
SET @name = '30 days complimentary accident insurance, with cash benefits up to $100,000'

-- Provider

IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, 'AIG', 1)
ELSE
	UPDATE
		OfferProvider
	SET
		name = 'AIG',
		enabled = 1
	WHERE
		id = @providerId

-- Offerings

DECLARE @offeringId UNIQUEIDENTIFIER
SET @offeringId = 'BBDCE6C8-1FF7-4596-98B8-4DBE42DCC36F'

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

-- Categories

DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER

SET @parentId = 'D1CA2974-0500-4DCB-8054-5E9AD8FB6E83'
SET @categoryId = 'E524208B-99BA-4e8f-898B-71DC1C82477C'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @name, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @name,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

