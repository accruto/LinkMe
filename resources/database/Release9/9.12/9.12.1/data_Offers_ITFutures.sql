DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '20D4488A-9852-40ab-ABF8-B5FE1F3A5057'

DECLARE @ciscoName NVARCHAR(500)
SET @ciscoName = 'Cisco Certification (CCNA, CCNP etc)'

DECLARE @microsoftName NVARCHAR(500)
SET @microsoftName = 'Microsoft Certification (MCITP, MTCS etc)'

DECLARE @compTIAName NVARCHAR(500)
SET @compTIAName = 'CompTIA Certification (A+ etc)'

-- Provider

IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, 'IT Futures', 1)
ELSE
	UPDATE
		OfferProvider
	SET
		name = 'IT Futures',
		enabled = 1
	WHERE
		id = @providerId

-- Offerings

DECLARE @ciscoId UNIQUEIDENTIFIER
DECLARE @microsoftId UNIQUEIDENTIFIER
DECLARE @compTIAId UNIQUEIDENTIFIER

SET @ciscoId = '{A9E8F910-F3F2-4eab-9667-74E37E3E4501}'
SET @microsoftId = '{EF21A03E-5323-4a7f-8E19-FAD11BAF3CD8}'
SET @compTIAId = '{933A71DD-32C5-4fc0-80D2-E84BB310E307}'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @ciscoId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@ciscoId, @providerId, @ciscoName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @ciscoName,
		enabled = 1
	WHERE
		id = @ciscoId

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @microsoftId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@microsoftId, @providerId, @microsoftName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @microsoftName,
		enabled = 1
	WHERE
		id = @microsoftId

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @compTIAId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@compTIAId, @providerId, @compTIAName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @compTIAName,
		enabled = 1
	WHERE
		id = @compTIAId

-- Categories

DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @categoryName NVARCHAR(500)
DECLARE @offeringId UNIQUEIDENTIFIER

SET @parentId = 'ae0db911-537d-46de-b971-4b46dc266372'

SET @categoryId = 'AA53C484-70A1-441e-BA2F-5BC68AC92690'
SET @categoryName = @ciscoName
SET @offeringId = @ciscoId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
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

SET @categoryId = '721AF10B-1A1D-4769-BB82-91A7E779FB66'
SET @categoryName = @microsoftName
SET @offeringId = @microsoftid

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
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

SET @categoryId = '18DB0EFB-727D-441d-A316-57740A9AE6E1'
SET @categoryName = @compTIAName
SET @offeringId = @compTIAId

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
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

